namespace Application.DTO;

public class AccessTokenDTO
{
    public string? Token { get; set; }
    public string Type { get; set; }
    public TimeSpan Expires { get; set; }
}
