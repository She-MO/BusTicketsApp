using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Auth;

public class AuthenticationStateHandler(
    CircuitServicesAccessor circuitServicesAccessor) 
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authStateProvider = circuitServicesAccessor.Services
            .GetRequiredService<CustomAuthenticationStateProvider>();
        var token = await authStateProvider.GetTokenAsync();

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //request.Headers.Add("Bearer", user.FindFirstValue("jwt") ?? "");
            
        return await base.SendAsync(request, cancellationToken);
    }
}