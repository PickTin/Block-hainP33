
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

        private readonly decimal _miningReward = 10;

        
        public List<Block> Chain { get; set; }

        private readonly HashingService _hashingService;
        private readonly MiningService _miningService;
        private readonly TransactionService _transactionService = new TransactionService();

        // Поле з попереднього етапу залишено як частину навчальної структури.
        // Поточну перевірку балансу виконує TransactionService.ValidateBalance().
        private readonly BalanceService _balanceService = new BalanceService();
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

        /*
        // Окрема перевірка транзакцій з попереднього етапу.
        // Закоментована, оскільки CreateNewBlock() використовує ValidateBalance(),
        // а цей метод уже викликає ValidateTransaction() і перевіряє підпис.
        // Залишена в коді як навчальний приклад окремої перевірки транзакцій.
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
        */

        // Створення нового блоку
        // Створення блоку з перевіркою транзакцій та винагородою майнеру
            private Block CreateNewBlock(
                List<Transaction> transactions,
                string author,
                string minerAddress,
                List<Transaction> mempool)
            {
                foreach (var tx in transactions)
                {
                    var balanceCheck = _transactionService.ValidateBalance(
                        tx, Chain, mempool);

                    if (!balanceCheck.isValid)
                        throw new ArgumentException(balanceCheck.errorMessage);
                }

                var transactionCopy = new List<Transaction>(transactions);

                // Додавання винагороди майнеру
                var coinbaseTransaction = Transaction.CreateCoinbaseTransaction(minerAddress, _miningReward);
                transactionCopy.Insert(0, coinbaseTransaction);

                var lastBlock = Chain[Chain.Count - 1];

                return new Block
                {
                    Index = lastBlock.Index + 1,
                    TimeStamp = DateTime.UtcNow,
                    Data = "Block with transactions",
                    Transactions = transactionCopy,
                    Author = author,
                    PrevHash = lastBlock.Hash,
                    Difficulty = _difficulty
                };
            }


                    // Синхронне додавання блоку
            // Синхронне додавання блоку
            // Синхронне додавання блоку
            public void AddBlock(
                List<Transaction> transactions,
                string author,
                string minerAddress,
                List<Transaction> mempool)
            {
                var newBlock = CreateNewBlock(
                    transactions, author, minerAddress, mempool);

                _miningService.MineBlockAsync(newBlock, _difficulty)
                    .GetAwaiter().GetResult();

                Chain.Add(newBlock);
                AdjustDifficulty();
            }

            // Асинхронне додавання блоку з однопотоковим майнінгом
            public async Task AddBlockAsync(
                List<Transaction> transactions,
                string author,
                string minerAddress,
                List<Transaction> mempool)
            {
                var newBlock = CreateNewBlock(
                    transactions, author, minerAddress, mempool);

                await _miningService.MineBlockAsync(newBlock, _difficulty);

                Chain.Add(newBlock);
                AdjustDifficulty();
            }

            // Асинхронне додавання блоку з багатопотоковим майнінгом
            public async Task AddBlockParallelAsync(
                List<Transaction> transactions,
                string author,
                string minerAddress,
                List<Transaction> mempool)
            {
                var newBlock = CreateNewBlock(
                    transactions, author, minerAddress, mempool);

                await _miningService.MineBlockParallelAsync(newBlock, _difficulty);

                Chain.Add(newBlock);
                AdjustDifficulty();
}

        // Створення блоку історії подій. Цей метод спільний для трьох
        // варіантів майнінгу, щоб не дублювати формування самого блоку.
        private Block CreateEventBlock(string author, string eventData)
        {
            if (string.IsNullOrWhiteSpace(eventData))
                throw new ArgumentException("Event data cannot be empty.", nameof(eventData));

            var lastBlock = Chain[Chain.Count - 1];

            return new Block
            {
                Index = lastBlock.Index + 1,
                TimeStamp = DateTime.UtcNow,
                Data = eventData,
                Author = author,
                PrevHash = lastBlock.Hash,
                Transactions = new List<Transaction>(),
                Difficulty = _difficulty
            };
        }

        // Синхронне додавання блоку-події. Залишаємо окрему реалізацію,
        // щоб можна було порівнювати її з асинхронними варіантами.
        public void AddBlock(string author, string eventData)
        {
            var newBlock = CreateEventBlock(author, eventData);
            _miningService.MineBlockAsync(newBlock, _difficulty).GetAwaiter().GetResult();
            Chain.Add(newBlock);
            AdjustDifficulty();
        }

        // Асинхронне додавання блоку-події з однопотоковим майнінгом.
        public async Task AddBlockAsync(string author, string eventData)
        {
            var newBlock = CreateEventBlock(author, eventData);
            await _miningService.MineBlockAsync(newBlock, _difficulty);
            Chain.Add(newBlock);
            AdjustDifficulty();
        }

        // Асинхронне додавання блоку-події з багатопотоковим майнінгом.
        public async Task AddBlockParallelAsync(string author, string eventData)
        {
            var newBlock = CreateEventBlock(author, eventData);
            await _miningService.MineBlockParallelAsync(newBlock, _difficulty);
            Chain.Add(newBlock);
            AdjustDifficulty();
        }

        // Детальна перевірка потрібна для визначення першого пошкодженого блоку.
        // Наявний IsChainValid() нижче залишається як сумісний bool-метод.
        public (bool IsValid, int? InvalidBlockIndex, string ErrorMessage) ValidateChainDetailed()
        {
            for (int i = 0; i < Chain.Count; i++)
            {
                var currentBlock = Chain[i];

                if (currentBlock.Hash != _hashingService.ComputeBlockHash(currentBlock))
                    return (false, currentBlock.Index, "Block hash does not match its data.");

                if (!currentBlock.Hash.StartsWith(
                    new string('0', currentBlock.Difficulty), StringComparison.Ordinal))
                    return (false, currentBlock.Index, "Block does not meet the mining difficulty.");

                if (i == 0)
                {
                    if (currentBlock.Index != 0 || currentBlock.PrevHash != "0")
                        return (false, currentBlock.Index, "Invalid genesis block.");
                }
                else
                {
                    var previousBlock = Chain[i - 1];

                    if (currentBlock.PrevHash != previousBlock.Hash)
                        return (false, currentBlock.Index, "PreviousHash does not match the previous block.");

                    if (currentBlock.Index != previousBlock.Index + 1)
                        return (false, currentBlock.Index, "Block index sequence is invalid.");
                }

                foreach (var tx in currentBlock.Transactions)
                {
                    var validationResult = _transactionService.ValidateTransaction(tx);
                    if (!validationResult.isValid)
                        return (false, currentBlock.Index,
                            $"Invalid transaction: {validationResult.errorMessage}");
                }
            }

            return (true, null, "Blockchain integrity check passed.");
        }

        // Зміна Data без перерахунку Hash — навмисна демонстрація атаки.
        // Повертає false, якщо другого блоку ще не існує.
        public bool TamperWithSecondBlock()
        {
            if (Chain.Count < 2)
                return false;

            Chain[1].Data += " [DATA MODIFIED]";
            return true;
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

        // Перевірка цілісності блокчейну. Старий bool-метод збережено,
        // а деталі помилки доступні через ValidateChainDetailed().
        public bool IsChainValid()
        {
            return ValidateChainDetailed().IsValid;
        }

        // Назва IsValid() відповідає формулюванню нового завдання.
        // IsChainValid() збережено для сумісності з попередніми заняттями.
        public bool IsValid()
        {
            return IsChainValid();
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
