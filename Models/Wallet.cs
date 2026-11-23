
using System;
using System.Security.Cryptography;

namespace BlockChainP33
{
    public class Wallet
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public byte[] PublicKey { get; set; }

        private byte[] PrivateKey { get; }

        public Wallet(string name, string address, byte[] publicKey, byte[] privateKey)
        {
            Name = name;
            Address = address;
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }

        // Підпис даних за допомогою приватного ключа
        public byte[] Sign(byte[] data)
        {
            using var ecdsa = ECDsa.Create();

            ecdsa.ImportECPrivateKey(PrivateKey, out _);

            return ecdsa.SignData(data, HashAlgorithmName.SHA256);
        }
    }
}
