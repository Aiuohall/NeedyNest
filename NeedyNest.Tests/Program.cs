using System;
using NeedyNest;

namespace NeedyNest.Tests
{
    /// <summary>
    /// Dependency-free unit test runner for PasswordHelper. Run the produced
    /// NeedyNest.Tests.exe (exit code 0 = all passed, 1 = failures).
    /// </summary>
    internal static class Program
    {
        private static int _passed, _failed;

        private static int Main()
        {
            Console.WriteLine("PasswordHelper tests");
            Console.WriteLine("====================");

            Test("Hash is not plaintext and is flagged hashed", () =>
            {
                string h = PasswordHelper.Hash("secret123");
                Assert(h != "secret123");
                Assert(PasswordHelper.IsHashed(h));
            });

            Test("Verify accepts the correct password", () =>
                Assert(PasswordHelper.Verify("secret123", PasswordHelper.Hash("secret123"))));

            Test("Verify rejects a wrong password", () =>
                Assert(!PasswordHelper.Verify("wrong", PasswordHelper.Hash("secret123"))));

            Test("Same password hashes differently (random salt)", () =>
                Assert(PasswordHelper.Hash("abc") != PasswordHelper.Hash("abc")));

            Test("Legacy plaintext verifies and is not flagged hashed", () =>
            {
                Assert(PasswordHelper.Verify("plain", "plain"));
                Assert(!PasswordHelper.IsHashed("plain"));
            });

            Test("Empty/null stored value never verifies", () =>
            {
                Assert(!PasswordHelper.Verify("x", ""));
                Assert(!PasswordHelper.Verify("x", null));
            });

            Console.WriteLine();
            Console.WriteLine($"Passed: {_passed}   Failed: {_failed}");
            return _failed == 0 ? 0 : 1;
        }

        private static void Test(string name, Action body)
        {
            try { body(); _passed++; Console.WriteLine("  [PASS] " + name); }
            catch (Exception ex) { _failed++; Console.WriteLine($"  [FAIL] {name} -> {ex.Message}"); }
        }

        private static void Assert(bool condition)
        {
            if (!condition) throw new Exception("assertion failed");
        }
    }
}
