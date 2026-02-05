using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using TodoListManagementSystem.Application.Abstractions.Utilities;
using TodoListManagementSystem.Shared.Settings;

namespace TodoListManagementSystem.Application.Universal.Utilities
{
    public sealed class HashManager(IOptionsMonitor<AppSettings> options) : IHashManager
    {
        private readonly HashSettings _settings = options.CurrentValue.Hash;

        public string Create(string value)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_settings.SaltSize);

            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(value))
            {
                Salt = salt,
                DegreeOfParallelism = _settings.DegreeOfParallelism,
                Iterations = _settings.Iterations,
                MemorySize = _settings.MemorySizeKB
            };

            byte[] hashBytes = argon2.GetBytes(_settings.HashLength);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hashBytes)}";
        }

        public bool Verify(string hashedValue, string value)
        {
            try
            {
                var parts = hashedValue.Split('.');
                if (parts.Length != 2)
                    return false;

                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] expectedHash = Convert.FromBase64String(parts[1]);

                using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(value))
                {
                    Salt = salt,
                    DegreeOfParallelism = _settings.DegreeOfParallelism,
                    Iterations = _settings.Iterations,
                    MemorySize = _settings.MemorySizeKB
                };

                byte[] actualHash = argon2.GetBytes(expectedHash.Length);

                return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
            }
            catch
            {
                return false;
            }
        }
    }
}
