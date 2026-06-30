using TODO.Interfaces.Auth.DTO;
using TODO.Interfaces.Common;

namespace TODO.Interfaces.Auth;

public interface IAuthService
{
    Task<TokenResponse> SignUpAsync(SignUpDTO dto);
    Task<TokenResponse> SignInAsync(SignInDTO dto);
    Task SignOutAsync(string refreshToken);
    Task<TokenResponse> RefreshAsync(string refreshToken);
}
