
using System;
using System.Collections.Generic;

namespace BlockChainP33
{
    public class TransactionService
    {
        private readonly WalletService _walletService = new WalletService();
        private readonly BalanceService _balanceService = new BalanceService();

        // Створення та підпис транзакції
        public Transaction CreateTransaction(
            string from,
            string to,
            decimal amount,
            Wallet wallet)
        {
            var transaction = new Transaction(
                from,
                to,
                amount,
                wallet.PublicKey
            );

            byte[] signatureBytes = wallet.Sign(transaction.GetDataToSign());

            transaction.Signature = Convert.ToBase64String(signatureBytes);

            var validationResult = ValidateTransaction(transaction);

            if (!validationResult.isValid)
                throw new ArgumentException(validationResult.errorMessage);

            return transaction;
        }

        // Перевірка транзакції та цифрового підпису
        public (bool isValid, string errorMessage) ValidateTransaction(
            Transaction transaction)
        {
            if (transaction == null)
                return (false, "Transaction cannot be null.");

            if (transaction.IsCoinbase)
            {
                if (string.IsNullOrWhiteSpace(transaction.To))
                    return (false, "Miner address is required.");

                if (transaction.Amount <= 0)
                    return (false, "Mining reward must be greater than zero.");

                return (true, string.Empty);
            }

            if (string.IsNullOrWhiteSpace(transaction.From))
                return (false, "Sender address is required.");

            if (string.IsNullOrWhiteSpace(transaction.To))
                return (false, "Recipient address is required.");

            if (transaction.Amount <= 0)
                return (false, "Transaction amount must be greater than zero.");

            if (transaction.PublicKey == null || transaction.PublicKey.Length == 0)
                return (false, "Public key is required.");

            if (string.IsNullOrWhiteSpace(transaction.Signature))
                return (false, "Signature is required.");

            string expectedAddress = _walletService.GetAddress(transaction.PublicKey);

            if (transaction.From != expectedAddress)
                return (false, "Sender address does not match public key.");

            bool signatureIsValid = _walletService.VerifySignature(
                transaction.GetDataToSign(),
                transaction.Signature,
                transaction.PublicKey
            );

            if (!signatureIsValid)
                return (false, "Invalid signature.");

            return (true, string.Empty);
        }

        // Перевірка балансу транзакції
        public (bool isValid, string errorMessage) ValidateBalance(
            Transaction transaction,
            List<Block> chain,
            List<Transaction> mempool)
        {
            if (transaction.IsCoinbase)
                return (true, string.Empty);

            var validationResult = ValidateTransaction(transaction);

            if (!validationResult.isValid)
                return validationResult;

            if (!_balanceService.HasSufficientBalance(
                transaction.From,
                transaction.Amount,
                chain,
                mempool))
            {
                return (false, "Insufficient balance for the transaction.");
            }

            return (true, string.Empty);
        }
    }
}
