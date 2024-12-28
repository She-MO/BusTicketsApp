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
            .GetRequiredService<AuthenticationStateProvider>();
        var authState = await authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is not null && user.Identity.IsAuthenticated)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.FindFirstValue("jwt") ?? "");
            //request.Headers.Add("Bearer", user.FindFirstValue("jwt") ?? "");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}