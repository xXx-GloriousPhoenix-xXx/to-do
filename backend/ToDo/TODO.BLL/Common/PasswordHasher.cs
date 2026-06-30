using TODO.Interfaces.Common;
using BC = BCrypt.Net.BCrypt;

namespace TODO.BLL.Common;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BC.HashPassword(password);
    }

    public bool Verify(string password, string hash)
    {
        return BC.Verify(password, hash);
    }
}