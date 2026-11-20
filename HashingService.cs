
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BlockChainP33
{
    // Сервіс хешування
    public class HashingService
    {
        public string ComputeBlockHash(Block block)
        {
            // Формуємо рядок транзакцій
            var stringTransactionRaw = string.Concat(
                block.Transactions.Select(t => t.ToRawString())
            );

            // Формуємо дані блоку для хешування
            var blockData =
                $"{block.Index}{block.PrevHash}{block.TimeStamp}" +
                $"{stringTransactionRaw}{block.Author}{block.Nonce}{block.Difficulty}";

            return ComputeHash(blockData);
        }

        private string ComputeHash(string data)
        {
            // Використовуємо SHA256 для обчислення хешу
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                var hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();

                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
