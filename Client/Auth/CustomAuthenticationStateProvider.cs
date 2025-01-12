using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Client.Auth;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private ProtectedLocalStorage _localStorage;
    private IConfiguration _configuration;

    public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorage, IConfiguration configuration)
    {
        _localStorage = localStorage;
        _configuration = configuration;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetAsync<string>("token");
        if (!String.IsNullOrEmpty(token.Value))
        {
            var claims = CustomTokenValidator.ValidateToken(token.Value, _configuration);
            var identity = new ClaimsIdentity(claims, "JWT Auth");
            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));
        }
        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
    }

    public async Task LoginAsync(string token)
    {
        await _localStorage.SetAsync("token", token);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogOutAsync()
    {
        await _localStorage.DeleteAsync("token");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task<string> GetTokenAsync()
    {
        return (await _localStorage.GetAsync<string>("token")).Value ?? string.Empty;
    }
}