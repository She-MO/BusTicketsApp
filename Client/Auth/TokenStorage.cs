using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Client.Auth;

public class TokenStorage
{
    public string? Token { get; set; }
}