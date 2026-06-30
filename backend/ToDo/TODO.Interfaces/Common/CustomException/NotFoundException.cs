namespace TODO.Interfaces.Common.CustomException;

public class NotFoundException(string message) : Exception(message)
{
}
