using System;
using System.Security.Cryptography;

namespace NeedyNest
{
    /// <summary>
    /// Salted PBKDF2 (SHA-256) password hashing. Stored format:
    ///     PBKDF2$&lt;iterations&gt;$&lt;saltBase64&gt;$&lt;hashBase64&gt;
    /// <para>
    /// <see cref="Verify"/> also accepts legacy plaintext values (rows created
    /// before hashing existed), so the app keeps working while old accounts are
    /// upgraded to a hash on their next successful login.
    /// </para>
    /// </summary>
    internal static class PasswordHelper
    {
        private const string Prefix     = "PBKDF2";
        private const int    SaltSize   = 16;   // bytes
        private const int    HashSize   = 32;   // bytes (256-bit)
        private const int    Iterations = 100000;

        public static string Hash(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            byte[] hash = Pbkdf2(password, salt, Iterations, HashSize);
            return string.Join("$", Prefix, Iterations,
                Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public static bool IsHashed(string stored) =>
            !string.IsNullOrEmpty(stored) && stored.StartsWith(Prefix + "$", StringComparison.Ordinal);

        public static bool Verify(string password, string stored)
        {
            if (string.IsNullOrEmpty(stored)) return false;

            // Legacy plaintext row — direct compare (will be upgraded on login).
            if (!IsHashed(stored))
                return stored == password;

            string[] parts = stored.Split('$');
            if (parts.Length != 4) return false;

            if (!int.TryParse(parts[1], out int iterations)) return false;
            byte[] salt     = Convert.FromBase64String(parts[2]);
            byte[] expected = Convert.FromBase64String(parts[3]);
            byte[] actual   = Pbkdf2(password, salt, iterations, expected.Length);

            return FixedTimeEquals(expected, actual);
        }

        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int size)
        {
            using (var kdf = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                return kdf.GetBytes(size);
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}
