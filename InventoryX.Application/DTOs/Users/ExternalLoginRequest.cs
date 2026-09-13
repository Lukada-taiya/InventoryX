namespace InventoryX.Application.DTOs.Users;

public class ExternalLoginRequest
{
    public string Provider { get; set; } = string.Empty;
    public string ReturnUrl { get; set; } = string.Empty;
    public string? BusinessName { get; set; }
    public string? Country { get; set; }
    public string? Currency { get; set; }
    public string? BusinessType { get; set; }
}
