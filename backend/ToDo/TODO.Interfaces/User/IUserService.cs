using TODO.Interfaces.User.DTO;

namespace TODO.Interfaces.User;

public interface IUserService
{
    Task<GetUserDTO?> GetByIdAsync(Guid userId);
    Task<bool> UpdateProfileAsync(Guid userId, UpdateProfileDTO dto);
    Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDTO dto);
    Task<bool> DeleteAsync(Guid userId);
}
