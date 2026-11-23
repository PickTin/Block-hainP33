
using System;
using System.Security.Cryptography;

namespace BlockChainP33
{
    public class WalletService
    {
        // Створення нового гаманця
        public Wallet CreateWallet(string name)
        {
            using var ecdsa = ECDsa.Create();

            // Генерація приватного та публічного ключів
            byte[] privateKey = ecdsa.ExportECPrivateKey();
            byte[] publicKey = ecdsa.ExportSubjectPublicKeyInfo();

            // Створення адреси на основі публічного ключа
            string address = GetAddress(publicKey);

            return new Wallet(name, address, publicKey, privateKey);
        }

        // Генерація адреси гаманця
        public string GetAddress(byte[] publicKey)
        {
            using var sha256 = SHA256.Create();

            byte[] hash = sha256.ComputeHash(publicKey);

            return Convert.ToHexString(hash).ToLowerInvariant()[..10];
        }

        // Перевірка цифрового підпису
        public bool VerifySignature(byte[] data, string signature, byte[] publicKey)
        {
            try
            {
                using var ecdsa = ECDsa.Create();

                // Завантаження публічного ключа
                ecdsa.ImportSubjectPublicKeyInfo(publicKey, out _);

                // Перетворення підпису з Base64 у масив байтів
                byte[] signatureBytes = Convert.FromBase64String(signature);

                return ecdsa.VerifyData(
                    data,
                    signatureBytes,
                    HashAlgorithmName.SHA256
                );
            }
            catch (CryptographicException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
