namespace TODO.Interfaces.Common.CustomException;

public class ForbiddenException(string message) : Exception(message);