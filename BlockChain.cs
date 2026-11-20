
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockChainP33
{
    public class BlockChain
    {
        private readonly decimal _targetBlockTime = 1;
        private readonly int _adjustEveryNBlocks = 1;

        public List<Block> Chain { get; set; }

        private readonly HashingService _hashingService;
        private readonly MiningService _miningService;
        private readonly TransactionService _transactionService = new TransactionService();

        // Складність майнінгу
        public int _difficulty { get; private set; } = 5;

        public BlockChain() : this(5)
        {
        }

        public BlockChain(int difficulty)
        {
            Chain = new List<Block>();

            _difficulty = Math.Max(1, difficulty);

            _hashingService = new HashingService();
            _miningService = new MiningService(_hashingService);

            AddGenesisBlock();
        }

        // Створення генезис-блоку
        private void AddGenesisBlock()
        {
            var genesisBlock = new Block
            {
                Index = 0,
                TimeStamp = DateTime.UtcNow,
                Data = "Genesis Block",
                Author = "System",
                PrevHash = "0",
                Transactions = new List<Transaction>(),
                Difficulty = _difficulty
            };

            _miningService.MineBlockAsync(genesisBlock, _difficulty)
                .GetAwaiter().GetResult();

            Chain.Add(genesisBlock);
        }

        // Перевірка транзакцій
        private void ValidateTransactions(List<Transaction> transactions)
        {
            foreach (var tx in transactions)
            {
                var validationResult = _transactionService.ValidateTransaction(tx);

                if (!validationResult.isValid)
                {
                    throw new ArgumentException(
                        $"Invalid transaction: {validationResult.errorMessage}");
                }
            }
        }

        // Створення нового блоку
        private Block CreateNewBlock(List<Transaction> transactions, string author)
        {
            ValidateTransactions(transactions);

            var lastBlock = Chain[Chain.Count - 1];

            return new Block
            {
                Index = lastBlock.Index + 1,
                TimeStamp = DateTime.UtcNow,
                Transactions = new List<Transaction>(transactions),
                Author = author,
                PrevHash = lastBlock.Hash,
                Difficulty = _difficulty
            };
        }

        // Синхронне додавання блоку
        public void AddBlock(List<Transaction> transactions, string author)
        {
            var newBlock = CreateNewBlock(transactions, author);

            _miningService.MineBlockAsync(newBlock, _difficulty)
                .GetAwaiter().GetResult();

            Chain.Add(newBlock);

            AdjustDifficulty();
        }

        // Асинхронне додавання блоку з однопотоковим майнінгом
        public async Task AddBlockAsync(List<Transaction> transactions, string author)
        {
            var newBlock = CreateNewBlock(transactions, author);

            await _miningService.MineBlockAsync(newBlock, _difficulty);

            Chain.Add(newBlock);

            AdjustDifficulty();
        }

        // Асинхронне додавання блоку з багатопотоковим майнінгом
        public async Task AddBlockParallelAsync(List<Transaction> transactions, string author)
        {
            var newBlock = CreateNewBlock(transactions, author);

            await _miningService.MineBlockParallelAsync(newBlock, _difficulty);

            Chain.Add(newBlock);

            AdjustDifficulty();
        }

        // Регулювання складності майнінгу
        private void AdjustDifficulty()
        {
            var recentBlocks = Chain
                .Where(b => b.Index > 0)
                .TakeLast(_adjustEveryNBlocks)
                .ToList();

            if (recentBlocks.Count < _adjustEveryNBlocks)
                return;

            var avgTime = recentBlocks.Average(b => b.MiningDuration);

            if (avgTime < _targetBlockTime)
            {
                _difficulty++;
                Console.WriteLine($"Difficulty increased to {_difficulty}");
            }
            else if (avgTime > _targetBlockTime)
            {
                _difficulty = Math.Max(1, _difficulty - 1);
                Console.WriteLine($"Difficulty decreased to {_difficulty}");
            }
        }

        // Перевірка цілісності блокчейну
        public bool IsChainValid()
        {
            for (int i = 0; i < Chain.Count; i++)
            {
                var currentBlock = Chain[i];

                if (currentBlock.Hash != _hashingService.ComputeBlockHash(currentBlock))
                    return false;

                if (!currentBlock.Hash.StartsWith(
                    new string('0', currentBlock.Difficulty),
                    StringComparison.Ordinal))
                    return false;

                foreach (var tx in currentBlock.Transactions)
                {
                    var validationResult = _transactionService.ValidateTransaction(tx);

                    if (!validationResult.isValid)
                        return false;
                }

                if (i > 0)
                {
                    var previousBlock = Chain[i - 1];

                    if (currentBlock.PrevHash != previousBlock.Hash)
                        return false;

                    if (currentBlock.Index != previousBlock.Index + 1)
                        return false;
                }
                else
                {
                    if (currentBlock.Index != 0 || currentBlock.PrevHash != "0")
                        return false;
                }
            }

            return true;
        }

        // Пошук блоку за індексом
        public Block? GetBlockByIndex(int index)
        {
            foreach (var block in Chain)
            {
                if (block.Index == index)
                    return block;
            }

            return null;
        }

        // Пошук блоків за ключовим словом
        public List<Block> SearchByData(string keyword)
        {
            List<Block> results = new List<Block>();

            foreach (var block in Chain)
            {
                if (block.Data.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    results.Add(block);
            }

            return results;
        }
    }
}
