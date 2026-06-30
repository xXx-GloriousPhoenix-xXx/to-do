using TODO.Interfaces.User.DTO;

namespace TODO.Interfaces.User;

public interface IUserService
{
    Task<GetUserDTO> GetByIdAsync(Guid userId);
    Task UpdateProfileAsync(Guid userId, UpdateProfileDTO dto);
    Task ChangePasswordAsync(Guid userId, ChangePasswordDTO dto);
    Task DeleteAsync(Guid userId);
}
