using Common.Domain.Multi;
using Sodium;

namespace Common.Infra.Multi
{
    public class Cryptography : ICryptography
    {
        public string HashPassword(string plaintext)
        {
            return PasswordHash.ArgonHashString(plaintext, PasswordHash.StrengthArgon.Sensitive);
        }

        public bool VerifyHashedPassword(string plaintext, string hash)
        {
            return PasswordHash.ArgonHashStringVerify(hash, plaintext);
        }
    }
}
