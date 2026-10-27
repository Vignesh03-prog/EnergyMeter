namespace SabEnergy.Application.Common.Models;

public class apiResponseDto<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}
