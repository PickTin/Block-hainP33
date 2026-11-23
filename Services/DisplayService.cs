
using System;
using System.Collections.Generic;

namespace BlockChainP33
{
    // Сервіс виведення інформації в консоль
    public class DisplayService
    {
        public void DisplayChain(BlockChain blockChain)
        {
            foreach (var block in blockChain.Chain)
            {
                DisplayBlock(block);
            }
        }

        public void DisplayBlock(Block block)
        {
            if (block == null)
            {
                Console.WriteLine("Block not found.");
                Console.WriteLine(new string('-', 48));
                return;
            }

            Console.WriteLine($"Index: {block.Index}");
            Console.WriteLine($"Data: {block.Data}");
            Console.WriteLine($"Hash: {block.Hash}");
            Console.WriteLine($"Author: {block.Author}");
            Console.WriteLine("Transaction:");

            if (block.Transactions == null || block.Transactions.Count == 0)
            {
                // Genesis-блок не містить транзакцій
            }
            else
            {
                foreach (var transaction in block.Transactions)
                {
                    Console.WriteLine(
                        $"\tTransaction Id: {transaction.Id}, " +
                        $"From: {transaction.From}, " +
                        $"To: {transaction.To}, " +
                        $"Amount: {transaction.Amount}, " +
                        $"Timestamp: {transaction.Timestamp}"
                    );
                }
            }

            Console.WriteLine($"Previous Hash: {block.PrevHash}");
            Console.WriteLine($"Timestamp: {block.TimeStamp}");
            Console.WriteLine($"DifficultyAtMining: {block.Difficulty}");
            Console.WriteLine($"Nonce: {block.Nonce}");
            Console.WriteLine($"Mining Duration: {block.MiningDuration} seconds");
            Console.WriteLine(new string('-', 48));
        }

        // Виведення транзакцій, які ще очікують додавання до блоку
        public void DisplayTransactions(List<Transaction> transactions)
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("No pending transactions.");
                return;
            }

            foreach (var transaction in transactions)
            {
                Console.WriteLine($"Transaction Id: {transaction.Id}");
                Console.WriteLine($"From: {transaction.From}");
                Console.WriteLine($"To: {transaction.To}");
                Console.WriteLine($"Amount: {transaction.Amount}");
                Console.WriteLine($"Timestamp: {transaction.Timestamp}");
                Console.WriteLine(new string('-', 48));
            }
        }
    }
}
