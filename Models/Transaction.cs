
using System;
using System.Text;

namespace BlockChainP33
{
    public class Transaction
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }

        // Публічний ключ відправника
        public byte[] PublicKey { get; set; }

        // Цифровий підпис транзакції
        public string Signature { get; set; }

        public bool IsCoinbase { get; set; }

        public Transaction(string from, string to, decimal amount, byte[] publicKey)
        {
            Id = Guid.NewGuid().ToString();
            From = from;
            To = to;
            Amount = amount;
            PublicKey = publicKey;
            Timestamp = DateTime.Now;
            Signature = "";
        }

        // Створення винагороди майнеру
        public static Transaction CreateCoinbaseTransaction(string minerAddress, decimal reward)
        {
            var tx = new Transaction("COINBASE", minerAddress, reward, Array.Empty<byte>());
            tx.IsCoinbase = true;
            return tx;
        }

        // Отримання даних транзакції для підпису
        public byte[] GetDataToSign()
        {
            string dataToSign = $"{From}|{To}|{Amount}|{Timestamp}";
            return Encoding.UTF8.GetBytes(dataToSign);
        }

        // Отримання повних даних транзакції для хешування блоку
        public string ToRawString()
        {
            string publicKey = PublicKey == null
                ? string.Empty
                : Convert.ToBase64String(PublicKey);

            return $"{Id}|{From}|{To}|{Amount}|{Timestamp}|{publicKey}|{Signature}|{IsCoinbase}";
        }

        public override string ToString()
        {
            return $"Transaction ID: {Id}, From: {From}, To: {To}, " +
                   $"Amount: {Amount}, Timestamp: {Timestamp}, " +
                   $"Signature: {Signature}";
        }
    }
}
