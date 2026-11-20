using System;

namespace BlockChainP33
{
    public class Transaction
    {
        public string Id { get; set; }
        //Відправник транзакції
        public string From { get; set; }
        //Отримувач транзакції
        public string To { get; set; }
        //Сума транзакції
        public decimal Amount { get; set; }
        //Дата та час створення транзакції
        public DateTime Timestamp { get; set; }

        //Конструктор для створення транзакції
        public Transaction(string from, string to, decimal amount)
        {
            Id = Guid.NewGuid().ToString();
            From = from;
            To = to;
            Amount = amount;
            Timestamp = DateTime.Now;
        }

        //Метод для отримання рядкового представлення транзакції у вигляді "Id|From|To|Amount|Timestamp"
        public string ToRawString()
        {
            return $"{Id}|{From}|{To}|{Amount}|{Timestamp}";
        }

        //Метод для отримання рядкового представлення транзакції у вигляді "Transaction ID: {Id}, From: {From}, To: {To}, Amount: {Amount}, Timestamp: {Timestamp}"
        public override string ToString()
        {
            return $"Transaction ID: {Id}, From: {From}, To: {To}, Amount: {Amount}, Timestamp: {Timestamp}";
        }
    }
}