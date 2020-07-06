namespace Common.Domain.Multi
{
    public interface ICryptography
    {
        string HashPassword(string plaintext);
        bool VerifyHashedPassword(string plaintext, string hash);
    }
}
