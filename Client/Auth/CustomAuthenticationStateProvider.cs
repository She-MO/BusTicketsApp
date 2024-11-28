using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Client.Auth;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private ProtectedSessionStorage _sessionStorage;
    private IConfiguration _configuration;

    public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage, IConfiguration configuration)
    {
        _sessionStorage = sessionStorage;
        _configuration = configuration;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _sessionStorage.GetAsync<string>("token");
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
        await _sessionStorage.SetAsync("token", token);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogOutAsync()
    {
        await _sessionStorage.DeleteAsync("token");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}