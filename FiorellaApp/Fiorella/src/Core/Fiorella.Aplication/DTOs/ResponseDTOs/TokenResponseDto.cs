namespace Fiorello.Application.DTOs.ResponseDTOs
{
    public record TokenResponseDto(string token, DateTime expireDate);
}
