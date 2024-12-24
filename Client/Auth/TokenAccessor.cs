using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Client.Auth;

public class TokenAccessor
{
    private ProtectedSessionStorage _sessionStorage;
    public TokenAccessor(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }
    
    public async Task<string> GetToken()
    {
        return (await _sessionStorage.GetAsync<string>("token")).Value ?? String.Empty;
    }
}