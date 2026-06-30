namespace TODO.Interfaces.User.DTO;

public record ChangePasswordDTO(
    string CurrentPassword,
    string NewPassword);