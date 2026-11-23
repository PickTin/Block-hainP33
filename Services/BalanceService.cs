using BlockChainP33;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockChainP33
{
    public class BalanceService
    {

        // Обчислення балансу для конкретної адреси
        public decimal GetBalance(string address, List<Block> chain)
        {
            decimal balance = 0;

                foreach (var block in chain)
                {
                    foreach (var transaction in block.Transactions)
                    {
                        if (transaction.To == address)
                        {
                            balance += transaction.Amount;
                        }
                        else if (transaction.From == address)
                        {
                            balance -= transaction.Amount;
                        }
                    }  
                }

            return balance;
        }
        public bool HasSufficientBalance(string address, decimal amount, List<Block> chain)
        {
            decimal balance = GetBalance(address, chain);
            return balance >= amount;
        }
                // Перевірка достатності балансу з урахуванням очікуваних транзакцій
        public bool HasSufficientBalance(
            string address,
            decimal amount,
            List<Block> chain,
            List<Transaction> mempool)
        {
            var balance = GetBalance(address, chain);

            foreach (var transaction in mempool)
            {
                if (transaction.From == address && !transaction.IsCoinbase)
                {
                    balance -= transaction.Amount;
                }
            }

            return balance >= amount;
}
    }
}