// Історичні варіанти Program з попередніх навчальних етапів залишені нижче
// закоментованими, щоб зберегти попередні приклади. Активна версія програми
// починається після цих прикладів і об'єднує старі функції з новим завданням.
﻿
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
//             // Console.WriteLine("\nAuthor tampering test:");

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

//             // Console.WriteLine("========== NORMAL MINING ==========");

//             // var stopwatch = Stopwatch.StartNew();

//             // // Виконуємо звичайний асинхронний майнінг
//             // await normalBlockchain.AddBlockAsync(
//             //     "Artem",
//             //     "Artem bought 1 BTC"
//             // );

//             // stopwatch.Stop();
//             // double normalTime = stopwatch.Elapsed.TotalSeconds;

//             // Console.WriteLine($"Normal mining time: {normalTime:F3} seconds");
//             // Console.WriteLine($"Blockchain validation: {normalBlockchain.IsChainValid()}");

//             // Console.WriteLine("\n========== PARALLEL MINING ==========");

//             // stopwatch.Restart();

//             // // Виконуємо багатопотоковий майнінг
//             // await parallelBlockchain.AddBlockParallelAsync(
//             //     "Artem",
//             //     "Artem bought 1 BTC"
//             // );

//             // stopwatch.Stop();
//             // double parallelTime = stopwatch.Elapsed.TotalSeconds;

//             // Console.WriteLine($"Thread count: {Environment.ProcessorCount}");
//             // Console.WriteLine($"Parallel mining time: {parallelTime:F3} seconds");
//             // Console.WriteLine($"Blockchain validation: {parallelBlockchain.IsChainValid()}");

//             // // Порівнюємо результати
//             // Console.WriteLine("\n========== RESULTS COMPARISON ==========");

//             // Console.WriteLine($"Normal mining: {normalTime:F3} seconds");
//             // Console.WriteLine($"Parallel mining: {parallelTime:F3} seconds");

//             // if (parallelTime < normalTime)
//             // {
//             //     Console.WriteLine(
//             //         $"Speedup: {normalTime / parallelTime:F2}x"
//             //     );
//             // }
//             // else
//             // {
//             //     Console.WriteLine(
//             //         $"Parallel mining is slower by {parallelTime / normalTime:F2}x times."
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




// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace BlockChainP33
// {
//     class Program
//     {
//         static async Task Main(string[] args)
//         {
//             var displayService = new DisplayService();
//             var blockChainService4 = new BlockChain();

//             var tempListTransaction = new List<Transaction>();
//             var transactionService = new TransactionService();
//             var walletService = new WalletService();

//             // Створення гаманців
//             var bobwallet = walletService.CreateWallet("Bob");
//             var alicewallet = walletService.CreateWallet("Alice");
//             var charliewallet = walletService.CreateWallet("Charlie");

//             Console.WriteLine("Wallets created:");
//             Console.WriteLine($"Alice: {alicewallet.Address}");
//             Console.WriteLine($"Bob: {bobwallet.Address}");
//             Console.WriteLine($"Charlie: {charliewallet.Address}");

//             while (true)
//             {
//                 Console.WriteLine("\nSelect an option:");
//                 Console.WriteLine("1. Mine Block");
//                 Console.WriteLine("2. Add Transaction");
//                 Console.WriteLine("3. Display Chain");
//                 Console.WriteLine("4. Display Transactions");
//                 Console.WriteLine("5. Validate Chain");
//                 Console.WriteLine("6. Add 20 Blocks");
//                 Console.WriteLine("7. Compare Mining");
//                 Console.WriteLine("8. Test Transaction Signature");
//                 Console.WriteLine("9. Exit");

//                 var enter = Console.ReadLine();

//                 switch (enter)
//                 {
//                     case "1":
//                         try
//                         {
//                             await blockChainService4.AddBlockParallelAsync(
//                                 tempListTransaction,
//                                 "Bob"
//                             );

//                             tempListTransaction.Clear();
//                         }
//                         catch (ArgumentException ex)
//                         {
//                             Console.WriteLine(ex.Message);
//                         }
//                         break;

//                     case "2":
//                         try
//                         {
//                             var transaction = transactionService.CreateTransaction(
//                                 alicewallet.Address,
//                                 bobwallet.Address,
//                                 10,
//                                 alicewallet
//                             );

//                             tempListTransaction.Add(transaction);

//                             Console.WriteLine("Transaction added successfully.");
//                         }
//                         catch (ArgumentException ex)
//                         {
//                             Console.WriteLine(ex.Message);
//                         }
//                         break;

//                     case "3":
//                         displayService.DisplayChain(blockChainService4);
//                         break;

//                     case "4":
//                         displayService.DisplayTransactions(tempListTransaction);
//                         break;

//                     case "5":
//                         var isValid = blockChainService4.IsChainValid();
//                         Console.WriteLine($"Is blockchain valid? {isValid}");
//                         break;

//                     case "6":
//                         for (int i = 0; i < 20; i++)
//                         {
//                             await blockChainService4.AddBlockParallelAsync(
//                                 tempListTransaction,
//                                 "Bob"
//                             );

//                             tempListTransaction.Clear();
//                         }
//                         break;

//                     case "7":
//                         await CompareMining();
//                         break;

//                     case "8":
//                         TestTransactionSignature(
//                             transactionService,
//                             alicewallet,
//                             bobwallet
//                         );
//                         break;

//                     case "9":
//                         return;

//                     default:
//                         Console.WriteLine("Invalid option.");
//                         break;
//                 }
//             }
//         }

//         // Порівняння звичайного та багатопотокового майнінгу
//         static async Task CompareMining()
//         {
//             int difficulty = 5;

//             var normalBlockchain = new BlockChain(difficulty);
//             var parallelBlockchain = new BlockChain(difficulty);

//             var emptyTransactions = new List<Transaction>();

//             Console.WriteLine("\n========== NORMAL ASYNC MINING ==========");

//             await normalBlockchain.AddBlockAsync(
//                 emptyTransactions,
//                 "Benchmark"
//             );

//             var normalBlock = normalBlockchain.Chain[
//                 normalBlockchain.Chain.Count - 1
//             ];

//             decimal normalTime = normalBlock.MiningDuration;

//             Console.WriteLine($"Difficulty: {difficulty}");
//             Console.WriteLine($"Mining time: {normalTime} seconds");
//             Console.WriteLine($"Nonce: {normalBlock.Nonce}");
//             Console.WriteLine($"Hash: {normalBlock.Hash}");

//             Console.WriteLine("\n========== PARALLEL MINING ==========");

//             await parallelBlockchain.AddBlockParallelAsync(
//                 emptyTransactions,
//                 "Benchmark"
//             );

//             var parallelBlock = parallelBlockchain.Chain[
//                 parallelBlockchain.Chain.Count - 1
//             ];

//             decimal parallelTime = parallelBlock.MiningDuration;

//             Console.WriteLine($"Difficulty: {difficulty}");
//             Console.WriteLine($"Threads: {Environment.ProcessorCount}");
//             Console.WriteLine($"Mining time: {parallelTime} seconds");
//             Console.WriteLine($"Nonce: {parallelBlock.Nonce}");
//             Console.WriteLine($"Hash: {parallelBlock.Hash}");

//             Console.WriteLine("\n========== COMPARISON ==========");

//             if (parallelTime < normalTime)
//             {
//                 Console.WriteLine(
//                     $"Parallel mining is faster. Speedup: {normalTime / parallelTime:F2}x"
//                 );
//             }
//             else if (parallelTime > normalTime)
//             {
//                 Console.WriteLine("Parallel mining is slower.");
//             }
//             else
//             {
//                 Console.WriteLine("Both methods have the same execution time.");
//             }
//         }

//         // Перевірка цифрового підпису транзакції
//         static void TestTransactionSignature(
//             TransactionService transactionService,
//             Wallet alicewallet,
//             Wallet bobwallet)
//         {
//             Console.WriteLine("\n========== SIGNATURE VALIDATION TEST ==========");

//             var transaction = transactionService.CreateTransaction(
//                 alicewallet.Address,
//                 bobwallet.Address,
//                 10,
//                 alicewallet
//             );

//             Console.WriteLine("\n1. Original transaction:");

//             var result = transactionService.ValidateTransaction(transaction);

//             Console.WriteLine($"Valid: {result.isValid}");
//             Console.WriteLine($"Message: {result.errorMessage}");

//             Console.WriteLine("\n2. Change transaction amount:");

//             transaction.Amount = 100;

//             result = transactionService.ValidateTransaction(transaction);

//             Console.WriteLine($"Valid: {result.isValid}");
//             Console.WriteLine($"Message: {result.errorMessage}");
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
            var walletService = new WalletService();
            var balanceService = new BalanceService();

            // Створення гаманців
            var bobwallet = walletService.CreateWallet("Bob");
            var alicewallet = walletService.CreateWallet("Alice");
            var charliewallet = walletService.CreateWallet("Charlie");

            Console.WriteLine("Wallets created:");
            Console.WriteLine($"Alice: {alicewallet.Address}");
            Console.WriteLine($"Bob: {bobwallet.Address}");
            Console.WriteLine($"Charlie: {charliewallet.Address}");

            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Mine Block");
                Console.WriteLine("2. Add Transaction");
                Console.WriteLine("3. Display Chain");
                Console.WriteLine("4. Display Transactions");
                Console.WriteLine("5. Validate Chain");
                Console.WriteLine("6. Add 20 Blocks");
                Console.WriteLine("7. Display Wallet Balances");
                Console.WriteLine("8. Compare Mining");
                Console.WriteLine("9. Test Transaction Signature");
                Console.WriteLine("10. Create 5 Event Blocks");
                Console.WriteLine("11. Find Block by Index");
                Console.WriteLine("12. Search Events by Text");
                Console.WriteLine("13. Modify Second Block (Security Test)");
                Console.WriteLine("14. Exit");

                var enter = Console.ReadLine();

                switch (enter)
                {
                    case "1":
                        try
                        {
                            blockChainService4.AddBlock(
                                new List<Transaction>(tempListTransaction),
                                "Bob",
                                bobwallet.Address,
                                tempListTransaction
                            );

                            tempListTransaction.Clear();
                            Console.WriteLine("Block mined successfully.");
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
                                alicewallet.Address,
                                bobwallet.Address,
                                10,
                                alicewallet
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
                        var validation = blockChainService4.ValidateChainDetailed();
                        if (validation.IsValid)
                        {
                            Console.WriteLine("Blockchain is valid.");
                        }
                        else
                        {
                            Console.WriteLine($"Blockchain is corrupted at block #{validation.InvalidBlockIndex}.");
                            Console.WriteLine($"Reason: {validation.ErrorMessage}");
                        }
                        break;

                    case "6":
                        try
                        {
                            for (int i = 0; i < 20; i++)
                            {
                                blockChainService4.AddBlock(
                                    new List<Transaction>(tempListTransaction),
                                    "Bob",
                                    bobwallet.Address,
                                    tempListTransaction
                                );

                                tempListTransaction.Clear();
                            }

                            Console.WriteLine("20 blocks mined successfully.");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "7":
                        Console.WriteLine("\n========== WALLET BALANCES ==========");
                        Console.WriteLine($"Bob: {balanceService.GetBalance(bobwallet.Address, blockChainService4.Chain)}");
                        Console.WriteLine($"Alice: {balanceService.GetBalance(alicewallet.Address, blockChainService4.Chain)}");
                        Console.WriteLine($"Charlie: {balanceService.GetBalance(charliewallet.Address, blockChainService4.Chain)}");
                        break;

                    case "8":
                        await CompareMining();
                        break;

                    case "9":
                        TestTransactionSignature(
                            transactionService,
                            alicewallet,
                            bobwallet
                        );
                        break;

                    case "10":
                        try
                        {
                            string[] events =
                            {
                                "User registered: Alice created a new account.",
                                "Purchase completed: Bob bought a laptop for 1200 UAH.",
                                "Funds transferred: Alice sent 50 coins to Charlie.",
                                "Profile updated: Charlie changed his account details.",
                                "Order delivered: Order #1001 was delivered to Bob."
                            };

                            // Демонструємо всі три наявні способи додавання блоку.
                            // Методи не замінюють один одного: кожен виконує власний тип майнінгу.
                            for (int i = 0; i < events.Length; i++)
                            {
                                if (i % 3 == 0)
                                {
                                    blockChainService4.AddBlock("Event System", events[i]);
                                }
                                else if (i % 3 == 1)
                                {
                                    await blockChainService4.AddBlockAsync("Event System", events[i]);
                                }
                                else
                                {
                                    await blockChainService4.AddBlockParallelAsync("Event System", events[i]);
                                }
                            }

                            Console.WriteLine("Five event blocks were added using all three mining methods.");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "11":
                        Console.Write("Enter block index: ");
                        if (int.TryParse(Console.ReadLine(), out int blockIndex))
                        {
                            var foundBlock = blockChainService4.GetBlockByIndex(blockIndex);
                            displayService.DisplayBlock(foundBlock!);
                        }
                        else
                        {
                            Console.WriteLine("Invalid block index.");
                        }
                        break;

                    case "12":
                        Console.Write("Enter text to search in the Data field: ");
                        string keyword = Console.ReadLine() ?? string.Empty;
                        if (string.IsNullOrWhiteSpace(keyword))
                        {
                            Console.WriteLine("Search text cannot be empty.");
                            break;
                        }

                        var foundBlocks = blockChainService4.SearchByData(keyword);
                        if (foundBlocks.Count == 0)
                        {
                            Console.WriteLine("No events matching this text were found.");
                        }
                        else
                        {
                            foreach (var foundBlock in foundBlocks)
                                displayService.DisplayBlock(foundBlock);
                        }
                        break;

                    case "13":
                        if (!blockChainService4.TamperWithSecondBlock())
                        {
                            Console.WriteLine("The second block does not exist yet. Add a block first.");
                            break;
                        }

                        Console.WriteLine("The second block data was modified without recalculating its hash.");
                        var tamperValidation = blockChainService4.ValidateChainDetailed();
                        Console.WriteLine($"Blockchain is valid: {tamperValidation.IsValid}");
                        Console.WriteLine($"First invalid block: {tamperValidation.InvalidBlockIndex}");
                        Console.WriteLine($"Reason: {tamperValidation.ErrorMessage}");
                        break;

                    case "14":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        // Порівняння звичайного та багатопотокового майнінгу
        static async Task CompareMining()
        {
            int difficulty = 5;

            var normalBlockchain = new BlockChain(difficulty);
            var parallelBlockchain = new BlockChain(difficulty);

            var emptyTransactions = new List<Transaction>();

            Console.WriteLine("\n========== NORMAL ASYNC MINING ==========");

            await normalBlockchain.AddBlockAsync(
            emptyTransactions,
            "Benchmark",
            "BenchmarkWallet",
            emptyTransactions
        );

            var normalBlock = normalBlockchain.Chain[
                normalBlockchain.Chain.Count - 1
            ];

            decimal normalTime = normalBlock.MiningDuration;

            Console.WriteLine($"Difficulty: {difficulty}");
            Console.WriteLine($"Mining time: {normalTime} seconds");
            Console.WriteLine($"Nonce: {normalBlock.Nonce}");
            Console.WriteLine($"Hash: {normalBlock.Hash}");

            Console.WriteLine("\n========== PARALLEL MINING ==========");

            await parallelBlockchain.AddBlockParallelAsync(
                emptyTransactions,
                "Benchmark",
                "BenchmarkWallet",
                emptyTransactions
            );

            var parallelBlock = parallelBlockchain.Chain[
                parallelBlockchain.Chain.Count - 1
            ];

            decimal parallelTime = parallelBlock.MiningDuration;

            Console.WriteLine($"Difficulty: {difficulty}");
            Console.WriteLine($"Threads: {Environment.ProcessorCount}");
            Console.WriteLine($"Mining time: {parallelTime} seconds");
            Console.WriteLine($"Nonce: {parallelBlock.Nonce}");
            Console.WriteLine($"Hash: {parallelBlock.Hash}");

            Console.WriteLine("\n========== COMPARISON ==========");

            if (parallelTime < normalTime)
            {
                Console.WriteLine(
                    $"Parallel mining is faster. Speedup: {normalTime / parallelTime:F2}x"
                );
            }
            else if (parallelTime > normalTime)
            {
                Console.WriteLine("Parallel mining is slower.");
            }
            else
            {
                Console.WriteLine("Both methods have the same execution time.");
            }
        }

        // Перевірка цифрового підпису транзакції
        static void TestTransactionSignature(
            TransactionService transactionService,
            Wallet alicewallet,
            Wallet bobwallet)
        {
            Console.WriteLine("\n========== SIGNATURE VALIDATION TEST ==========");

            var transaction = transactionService.CreateTransaction(
                alicewallet.Address,
                bobwallet.Address,
                10,
                alicewallet
            );

            Console.WriteLine("\n1. Original transaction:");

            var result = transactionService.ValidateTransaction(transaction);

            Console.WriteLine($"Valid: {result.isValid}");
            Console.WriteLine($"Message: {result.errorMessage}");

            Console.WriteLine("\n2. Change transaction amount:");

            transaction.Amount = 100;

            result = transactionService.ValidateTransaction(transaction);

            Console.WriteLine($"Valid: {result.isValid}");
            Console.WriteLine($"Message: {result.errorMessage}");
        }
    }
}
