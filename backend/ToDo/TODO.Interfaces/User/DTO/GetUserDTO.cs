namespace TODO.Interfaces.User.DTO;

public record GetUserDTO(
    Guid Id,
    string Username,
    string Email);
