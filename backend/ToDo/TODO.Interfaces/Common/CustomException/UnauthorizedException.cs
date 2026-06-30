namespace TODO.Interfaces.Common.CustomException;

public class UnauthorizedException(string message) : Exception(message)
{
}