namespace TODO.Interfaces.Common;

public interface ITokenGenerator
{
    string GenerateAccessToken(Guid userId);
    string GenerateRefreshToken();
}