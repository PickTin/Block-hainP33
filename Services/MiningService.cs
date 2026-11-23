
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

namespace BlockChainP33
{
    // Сервіс майнінгу
    public class MiningService
    {
        private readonly HashingService _hashingService;

        public MiningService()
        {
            _hashingService = new HashingService();
        }

        public MiningService(HashingService hashingService)
        {
            _hashingService = hashingService;
        }

        /*
        // Старий синхронний однопотоковий майнінг.
        // Залишено для попереднього завдання.
        // Не використовується в поточному потоці, оскільки синхронний AddBlock()
        // очікує завершення MineBlockAsync(), а CompareMining() окремо порівнює
        // однопотоковий асинхронний та багатопотоковий варіанти.

        public void MineBlock(Block block, int difficulty)
        {
            var target = new string('0', difficulty);

            while (true)
            {
                block.Hash = _hashingService.ComputeBlockHash(block);

                if (block.Hash.StartsWith(target))
                {
                    Console.WriteLine($"Hash found for block {block.Index}: {block.Hash}");
                    break;
                }

                block.Nonce++;
            }
        }
        */

        // Асинхронний однопотоковий майнінг
        public Task MineBlockAsync(Block block, int difficulty)
        {
            return Task.Run(() =>
            {
                var target = new string('0', difficulty);
                var startTime = Stopwatch.StartNew();

                while (true)
                {
                    block.Hash = _hashingService.ComputeBlockHash(block);

                    if (block.Hash.StartsWith(target, StringComparison.Ordinal))
                    {
                        break;
                    }

                    block.Nonce++;
                }

                startTime.Stop();

                block.MiningDuration = (decimal)startTime.Elapsed.TotalSeconds;

                Console.WriteLine($"Hash found for block {block.Index}: {block.Hash}");
                Console.WriteLine($"Mining duration: {block.MiningDuration} seconds");
            });
        }

        // Асинхронний багатопотоковий майнінг
        public Task MineBlockParallelAsync(Block block, int difficulty)
        {
            return Task.Run(() =>
            {
                var target = new string('0', difficulty);
                int threadCount = Environment.ProcessorCount;
                int found = 0;

                var startTime = Stopwatch.StartNew();

                using (CancellationTokenSource cancellation = new CancellationTokenSource())
                {
                    Parallel.For(0, threadCount, workerIndex =>
                    {
                        long nonce = workerIndex;
                        int attempts = 0;

                        while (!cancellation.IsCancellationRequested)
                        {
                            var candidate = new Block
                            {
                                Index = block.Index,
                                TimeStamp = block.TimeStamp,
                                Data = block.Data,
                                Author = block.Author,
                                PrevHash = block.PrevHash,
                                Transactions = new List<Transaction>(block.Transactions),
                                Difficulty = block.Difficulty,
                                Nonce = nonce
                            };

                            string hash = _hashingService.ComputeBlockHash(candidate);

                            if (hash.StartsWith(target, StringComparison.Ordinal))
                            {
                                if (Interlocked.CompareExchange(ref found, 1, 0) == 0)
                                {
                                    block.Nonce = nonce;
                                    block.Hash = hash;

                                    cancellation.Cancel();
                                }

                                break;
                            }

                            nonce += threadCount;
                            attempts++;

                            // Діагностика прогресу майнінгу.
                            // Тимчасово вимкнена, щоб Console.WriteLine
                            // не впливав на точність вимірювання часу.
                            // За потреби можна розкоментувати.

                            /*
                            if (attempts % 100000 == 0)
                            {
                                Console.WriteLine(
                                    $"Mining... Thread: {workerIndex}, Nonce: {nonce}, Hash: {hash}"
                                );
                            }
                            */
                        }
                    });
                }

                startTime.Stop();

                block.MiningDuration = (decimal)startTime.Elapsed.TotalSeconds;

                Console.WriteLine($"Hash found for block {block.Index}: {block.Hash}");
                Console.WriteLine($"Mining duration: {block.MiningDuration} seconds");
            });
        }
    }
}
