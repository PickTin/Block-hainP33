
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
            // Формуємо рядок транзакцій разом із публічними ключами та підписами.
            // Роздільник запобігає неоднозначному склеюванню сусідніх значень.
            var stringTransactionRaw = string.Join(
                "||",
                block.Transactions.Select(t => t.ToRawString())
            );

            // Data входить до хешу, щоб будь-яка зміна події в блоці
            // автоматично порушувала перевірку цілісності блокчейну.
            var blockData =
                $"{block.Index}|{block.Data}|{block.PrevHash}|{block.TimeStamp:O}|" +
                $"{stringTransactionRaw}|{block.Author}|{block.Nonce}|{block.Difficulty}";

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

