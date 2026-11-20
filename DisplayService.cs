
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
                Console.WriteLine(new string('-', 30));
                return;
            }

            Console.WriteLine($"Index: {block.Index}");
            Console.WriteLine($"Author: {block.Author}");
            Console.WriteLine($"Hash: {block.Hash}");
            Console.WriteLine($"Data: {block.Data}");
            Console.WriteLine($"PrevHash: {block.PrevHash}");
            Console.WriteLine($"TimeStamp: {block.TimeStamp}");
            Console.WriteLine($"Nonce: {block.Nonce}");
            Console.WriteLine($"Difficulty: {block.Difficulty}");
            Console.WriteLine($"Mining Duration: {block.MiningDuration} seconds");

            // ПОПЕРЕДНЄ ЗАВДАННЯ:
            // Виведення транзакцій кожного блоку.
            // Тимчасово закоментовано для поточного завдання з майнінгу.
            // Після завершення завдання розкоментувати цей блок.

            /*
            Console.WriteLine("Transactions:");

            if (block.Transactions.Count == 0)
            {
                Console.WriteLine("No transactions.");
            }
            else
            {
                foreach (var transaction in block.Transactions)
                {
                    Console.WriteLine($"Transaction ID: {transaction.Id}");
                    Console.WriteLine($"From: {transaction.From}");
                    Console.WriteLine($"To: {transaction.To}");
                    Console.WriteLine($"Amount: {transaction.Amount}");
                    Console.WriteLine($"Timestamp: {transaction.Timestamp}");
                    Console.WriteLine();
                }
            }
            */

            Console.WriteLine(new string('-', 30));
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
                Console.WriteLine($"Transaction ID: {transaction.Id}");
                Console.WriteLine($"From: {transaction.From}");
                Console.WriteLine($"To: {transaction.To}");
                Console.WriteLine($"Amount: {transaction.Amount}");
                Console.WriteLine($"Timestamp: {transaction.Timestamp}");
                Console.WriteLine(new string('-', 30));
            }
        }
    }
}
