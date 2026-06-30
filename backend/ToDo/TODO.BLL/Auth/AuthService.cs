using AutoMapper;
using TODO.Interfaces.Auth;
using TODO.Interfaces.Auth.DTO;
using TODO.Interfaces.Auth.Entity;
using TODO.Interfaces.Common;
using TODO.Interfaces.Common.CustomException;
using TODO.Interfaces.User;
using TODO.Interfaces.User.Entities;

namespace TODO.BLL.Auth;

public class AuthService(
    IRefreshTokenRepository rtRepository,
    IUserRepository userRepository,
    IMapper mapper,
    IPasswordHasher hasher,
    ITokenGenerator tokenGenerator)
    : IAuthService
{
    private readonly IRefreshTokenRepository _rtRepository = rtRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordHasher _hasher = hasher;
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<TokenResponse> RefreshAsync(string refreshToken)
    {
        var tokenEntity = await _rtRepository.GetByTokenAsync(refreshToken);

        if (tokenEntity is null || tokenEntity.IsRevoked || tokenEntity.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedException("Invalid or expired refresh token");

        await _rtRepository.RevokeAsync(refreshToken);

        return await IssueTokensAsync(tokenEntity.UserId);
    }

    public async Task<TokenResponse> SignInAsync(SignInDTO dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.UsernameOrEmail)
            ?? await _userRepository.GetByEmailAsync(dto.UsernameOrEmail);

        if (user is null || !_hasher.Verify(dto.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid username/email or password");

        return await IssueTokensAsync(user.Id);
    }

    public async Task SignOutAsync(string refreshToken)
    {
        await _rtRepository.RevokeAsync(refreshToken);
    }

    public async Task<TokenResponse> SignUpAsync(SignUpDTO dto)
    {
        var existingByUsername = await _userRepository.GetByUsernameAsync(dto.Username);
        if (existingByUsername is not null)
            throw new Exception();

        var existingByEmail = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingByEmail is not null)
            throw new Exception();

        var entity = _mapper.Map<UserEntity>(dto);
        entity.PasswordHash = _hasher.Hash(dto.Password);

        var created = await _userRepository.AddAsync(entity);

        return await IssueTokensAsync(created.Id);
    }

    private async Task<TokenResponse> IssueTokensAsync(Guid userId)
    {
        var accessToken = _tokenGenerator.GenerateAccessToken(userId);
        var refreshTokenValue = _tokenGenerator.GenerateRefreshToken();

        await _rtRepository.AddAsync(new RefreshTokenEntity
        {
            UserId = userId,
            Token = refreshTokenValue,
            ExpiresAt = DateTime.UtcNow.AddDays(30)
        });

        return new TokenResponse(accessToken, refreshTokenValue);
    }
}