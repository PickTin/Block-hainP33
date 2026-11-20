
// using System;
// using System.Diagnostics;
// using System.Threading.Tasks;

// namespace BlockChainP33
// {
//     class Program
//     {
//         static async Task Main(string[] args)
//         {
            

            
//             // // ДЗ 1 — Простий блокчейн та поле Author

//             // var blockChainService = new BlockChain();
//             // var displayService = new DisplayService();

//             // // Додаємо блоки з інформацією про транзакції
//             // blockChainService.AddBlock("Artem", "Artem registered");
//             // blockChainService.AddBlock("Artem", "Artem bought 1 BTC");
//             // blockChainService.AddBlock("Artem", "Artem shorted 0.5 BTC");
//             // blockChainService.AddBlock("Artem", "Artem got 0.5 BTC");
//             // blockChainService.AddBlock("Artem", "Artem send Alex 1.5 BTC");

//             // // Відображаємо весь блокчейн
//             // displayService.DisplayChain(blockChainService);

//             // // Перевіряємо цілісність блокчейну
//             // bool isValid = blockChainService.IsChainValid();

//             // if (isValid)
//             //     Console.WriteLine("Blockchain is valid.");
//             // else
//             //     Console.WriteLine("Blockchain is not valid.");

//             // // Перевіряємо зміну автора
//             // Console.WriteLine("\nПеревірка зміни автора:");

//             // blockChainService.Chain[1].Author = "Alex";

//             // Console.WriteLine(
//             //     blockChainService.IsChainValid()
//             //         ? "Blockchain is valid."
//             //         : "Blockchain is not valid."
//             // );


//             // // Попереднє завдання — Proof of Work та майнінг

//             // var miningBlockchain = new BlockChain(5);
//             // var miningDisplay = new DisplayService();

//             // miningBlockchain.AddBlock("Artem", "Artem registered");
//             // miningBlockchain.AddBlock("Artem", "Artem bought 1 BTC");
//             // miningBlockchain.AddBlock("Artem", "Artem got 0.5 BTC");

//             // miningDisplay.DisplayChain(miningBlockchain);

//             // bool miningIsValid = miningBlockchain.IsChainValid();

//             // if (miningIsValid)
//             //     Console.WriteLine("Blockchain is valid.");
//             // else
//             //     Console.WriteLine("Blockchain is not valid.");
            
//             // // ДЗ 2 — Асинхронний та багатопотоковий майнінг

//             // int difficulty = 5;

//             // // Створюємо два блокчейни з однаковою складністю
//             // var normalBlockchain = new BlockChain(difficulty);
//             // var parallelBlockchain = new BlockChain(difficulty);

//             // Console.WriteLine("========== ЗВИЧАЙНИЙ МАЙНІНГ ==========");

//             // var stopwatch = Stopwatch.StartNew();

//             // // Виконуємо звичайний асинхронний майнінг
//             // await normalBlockchain.AddBlockAsync(
//             //     "Artem",
//             //     "Artem bought 1 BTC"
//             // );

//             // stopwatch.Stop();
//             // double normalTime = stopwatch.Elapsed.TotalSeconds;

//             // Console.WriteLine($"Час звичайного майнінгу: {normalTime:F3} сек");
//             // Console.WriteLine($"Перевірка блокчейну: {normalBlockchain.IsChainValid()}");

//             // Console.WriteLine("\n========== БАГАТОПОТОКОВИЙ МАЙНІНГ ==========");

//             // stopwatch.Restart();

//             // // Виконуємо багатопотоковий майнінг
//             // await parallelBlockchain.AddBlockParallelAsync(
//             //     "Artem",
//             //     "Artem bought 1 BTC"
//             // );

//             // stopwatch.Stop();
//             // double parallelTime = stopwatch.Elapsed.TotalSeconds;

//             // Console.WriteLine($"Кількість потоків: {Environment.ProcessorCount}");
//             // Console.WriteLine($"Час багатопотокового майнінгу: {parallelTime:F3} сек");
//             // Console.WriteLine($"Перевірка блокчейну: {parallelBlockchain.IsChainValid()}");

//             // // Порівнюємо результати
//             // Console.WriteLine("\n========== ПОРІВНЯННЯ РЕЗУЛЬТАТІВ ==========");

//             // Console.WriteLine($"Звичайний майнінг: {normalTime:F3} сек");
//             // Console.WriteLine($"Багатопотоковий майнінг: {parallelTime:F3} сек");

//             // if (parallelTime < normalTime)
//             // {
//             //     Console.WriteLine(
//             //         $"Прискорення: {normalTime / parallelTime:F2}x"
//             //     );
//             // }
//             // else
//             // {
//             //     Console.WriteLine(
//             //         $"Багатопотоковий майнінг повільніший у {parallelTime / normalTime:F2}x разів."
//             //     );
//             // }

//             // Console.WriteLine("============================================");

//             var displayService = new DisplayService();
//             var blockChainService4 = new BlockChain();
//             while (true)
//             {
//                 Console.WriteLine("\nSelect an option:");
//                 Console.WriteLine("1. Add Block");
//                 Console.WriteLine("2. Display Chain");
//                 Console.WriteLine("3. Validate Chain");
//                 Console.WriteLine("4. Add 20 Blocks");
//                 Console.WriteLine("5. Exit");

//                 var enter = Console.ReadLine();

//                 switch (enter)
//                 {
//                     case "1":
//                         await blockChainService4.AddBlockParallelAsync(
//                             "Bob",
//                             "Bob send 100 coins to Alice"
//                         );
//                         break;

//                     case "2":
//                         displayService.DisplayChain(blockChainService4);
//                         break;

//                     case "3":
//                         var isValid = blockChainService4.IsChainValid();
//                         Console.WriteLine($"Is blockchain valid? {isValid}");
//                         break;

//                     case "4":
//                         for (int i = 0; i < 20; i++)
//                         {
//                             await blockChainService4.AddBlockParallelAsync(
//                                 "Bob",
//                                 $"Block {i + 1} Data"
//                             );
//                         }
//                         break;

//                     case "5":
//                         return;

//                     default:
//                         Console.WriteLine("Invalid option.");
//                         break;
//                 }
//             }



        

        
//         }
//     }
// }



using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockChainP33
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var displayService = new DisplayService();
            var blockChainService4 = new BlockChain();

            var tempListTransaction = new List<Transaction>();
            var transactionService = new TransactionService();

            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Mine Block");
                Console.WriteLine("2. Add Transaction");
                Console.WriteLine("3. Display Chain");
                Console.WriteLine("4. Display Transactions");
                Console.WriteLine("5. Validate Chain");
                Console.WriteLine("6. Add 20 Blocks");
                Console.WriteLine("7. Compare Mining");
                Console.WriteLine("8. Exit");

                var enter = Console.ReadLine();

                switch (enter)
                {
                    case "1":
                        try
                        {
                            await blockChainService4.AddBlockParallelAsync(
                                tempListTransaction,
                                "Bob"
                            );

                            tempListTransaction.Clear();
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "2":
                        try
                        {
                            var transaction = transactionService.CreateTransaction(
                                "Alice",
                                "Bob",
                                10
                            );

                            tempListTransaction.Add(transaction);

                            Console.WriteLine("Transaction added successfully.");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "3":
                        displayService.DisplayChain(blockChainService4);
                        break;

                    case "4":
                        displayService.DisplayTransactions(tempListTransaction);
                        break;

                    case "5":
                        var isValid = blockChainService4.IsChainValid();
                        Console.WriteLine($"Is blockchain valid? {isValid}");
                        break;

                    case "6":
                        for (int i = 0; i < 20; i++)
                        {
                            await blockChainService4.AddBlockParallelAsync(
                                tempListTransaction,
                                "Bob"
                            );

                            tempListTransaction.Clear();
                        }
                        break;

                    case "7":
                        await CompareMining();
                        break;

                    case "8":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        // Порівняння однопотокового та багатопотокового майнінгу
        static async Task CompareMining()
        {
            int difficulty = 7;

            // Створюємо окремі блокчейни з однаковою складністю
            var normalBlockchain = new BlockChain(difficulty);
            var parallelBlockchain = new BlockChain(difficulty);

            var emptyTransactions = new List<Transaction>();

            Console.WriteLine("\n========== ЗВИЧАЙНИЙ АСИНХРОННИЙ МАЙНІНГ ==========");

            // Однопотоковий майнінг
            await normalBlockchain.AddBlockAsync(
                emptyTransactions,
                "Benchmark"
            );

            var normalBlock = normalBlockchain.Chain[
                normalBlockchain.Chain.Count - 1
            ];

            decimal normalTime = normalBlock.MiningDuration;

            Console.WriteLine($"Difficulty: {difficulty}");
            Console.WriteLine($"Mining time: {normalTime:F3} seconds");
            Console.WriteLine($"Nonce: {normalBlock.Nonce}");
            Console.WriteLine($"Hash: {normalBlock.Hash}");

            Console.WriteLine("\n========== БАГАТОПОТОКОВИЙ МАЙНІНГ ==========");

            // Багатопотоковий майнінг
            await parallelBlockchain.AddBlockParallelAsync(
                emptyTransactions,
                "Benchmark"
            );

            var parallelBlock = parallelBlockchain.Chain[
                parallelBlockchain.Chain.Count - 1
            ];

            decimal parallelTime = parallelBlock.MiningDuration;

            Console.WriteLine($"Difficulty: {difficulty}");
            Console.WriteLine($"Threads: {Environment.ProcessorCount}");
            Console.WriteLine($"Mining time: {parallelTime:F3} seconds");
            Console.WriteLine($"Nonce: {parallelBlock.Nonce}");
            Console.WriteLine($"Hash: {parallelBlock.Hash}");

            Console.WriteLine("\n========== ПОРІВНЯННЯ ==========");

            Console.WriteLine($"Single-thread: {normalTime:F3} seconds");
            Console.WriteLine($"Multi-thread: {parallelTime:F3} seconds");

            if (parallelTime < normalTime)
            {
                Console.WriteLine(
                    $"Speedup: {normalTime / parallelTime:F2}x"
                );
            }
            else if (parallelTime > normalTime)
            {
                Console.WriteLine(
                    $"Multi-thread mining is slower by {parallelTime / normalTime:F2}x"
                );
            }
            else
            {
                Console.WriteLine("Both methods took approximately the same time.");
            }

            Console.WriteLine("============================================");
        }
    }
}
