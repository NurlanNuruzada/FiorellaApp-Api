namespace Fiorello.Application.DTOs.AuthDTOs
{
    public record RegisterDto(string? fullname, string username, string email, string password);
}
