
using System;
using System.Collections.Generic;

namespace BlockChainP33
{
    // Модель блоку
    public class Block
    {
        public int Index { get; set; }

        public string Hash { get; set; } = "";

        public string Data { get; set; } = "";

        public string PrevHash { get; set; } = "";

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public DateTime TimeStamp { get; set; }

        public string Author { get; set; } = "";

        public long Nonce { get; set; }

        public decimal MiningDuration { get; set; } = 0;

        // Складність, з якою був видобутий блок
        public int Difficulty { get; set; }
    }
}
