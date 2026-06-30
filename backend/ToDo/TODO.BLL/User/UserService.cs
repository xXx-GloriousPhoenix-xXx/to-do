using AutoMapper;
using TODO.Interfaces.Common;
using TODO.Interfaces.Common.CustomException;
using TODO.Interfaces.User;
using TODO.Interfaces.User.DTO;

namespace TODO.BLL.User;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper,
    IPasswordHasher hasher)
    : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordHasher _hasher = hasher;

    public async Task ChangePasswordAsync(Guid userId, ChangePasswordDTO dto)
    {
        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new NotFoundException("User not found");

        if (!_hasher.Verify(dto.CurrentPassword, user.PasswordHash))
            throw new UnauthorizedException("Current password is incorrect");

        user.PasswordHash = _hasher.Hash(dto.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteAsync(Guid userId)
    {
        var entity = await _userRepository.GetByIdAsync(userId)
            ?? throw new NotFoundException("User not found");

        await _userRepository.DeleteAsync(entity);
    }

    public async Task<GetUserDTO> GetByIdAsync(Guid userId)
    {
        var entity = await _userRepository.GetByIdAsync(userId)
            ?? throw new NotFoundException("User not found");

        return _mapper.Map<GetUserDTO>(entity);
    }

    public async Task UpdateProfileAsync(Guid userId, UpdateProfileDTO dto)
    {
        var entity = await _userRepository.GetByIdAsync(userId)
        ?? throw new NotFoundException("User not found");

        if (dto.Username is not null)
        {
            var existing = await _userRepository.GetByUsernameAsync(dto.Username);
            if (existing is not null && existing.Id != userId)
                throw new ConflictException("Username already taken");

            entity.Username = dto.Username;
        }

        if (dto.Email is not null)
        {
            var existing = await _userRepository.GetByEmailAsync(dto.Email);
            if (existing is not null && existing.Id != userId)
                throw new ConflictException("Email already taken");

            entity.Email = dto.Email;
        }

        entity.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(entity);
    }
}