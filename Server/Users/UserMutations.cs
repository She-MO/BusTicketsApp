using System.CommandLine.Parsing;
using System.Security.Claims;
using BusTicketsApp.Server.Auth;
using BusTicketsApp.Server.Data;
//using BusTicketsApp.Server.Users.UserInputValidators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Users;

[MutationType]
public static class UserMutations
{
    [Error<EmailAlreadyInUseException>]
    public static async Task<string> RegisterUserAsync(
        RegisterUserInput input,
        //RegisterUserInputValidator validator,
        IPasswordHasher<User> passwordHasher,
        //IHttpContextAccessor httpContextAccessor,
        TokenProvider tokenProvider,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        //var result = await validator.ValidateAsync(input, cancellationToken);
        var user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == input.Email, cancellationToken);
        if (user is not null)
        {
            throw new EmailAlreadyInUseException();
        }

        user = new User
        {
            Email = input.Email,
            Password = input.Password,
            Phone = input.Phone
        };
        user.Password = passwordHasher.HashPassword(user, user.Password);
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        return tokenProvider.Create(user);
        //List<Claim> claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //    new Claim(ClaimTypes.Email, user.Email),
        //};
        //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        //var authProperties = new AuthenticationProperties
        //{
        //    AllowRefresh = true,
        //    ExpiresUtc = DateTimeOffset.Now.AddMinutes(20),
        //    IsPersistent = true
        //};
        //if (httpContextAccessor.HttpContext is not null)
        //{
        //    await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        //        new ClaimsPrincipal(claimsIdentity), authProperties);
        //}
    }
    //[Error<InvalidEmailOrPasswordException>]
    public static async Task<string> LogInUserAsync(
        LogInUserInput input,
        IPasswordHasher<User> passwordHasher,
        //IHttpContextAccessor httpContextAccessor,
        TokenProvider tokenProvider,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == input.Email, cancellationToken);
        if (user is null ||
            passwordHasher.VerifyHashedPassword(user, user.Password, input.Password) != PasswordVerificationResult.Success)
        {
            return String.Empty;
        }
        return tokenProvider.Create(user);
        //List<Claim> claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //    new Claim(ClaimTypes.Email, user.Email),
        //};
        //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        //var authProperties = new AuthenticationProperties
        //{
        //    AllowRefresh = true,
        //    ExpiresUtc = DateTimeOffset.Now.AddMinutes(20),
        //    IsPersistent = true
        //};
        //if (httpContextAccessor.HttpContext is not null)
        //{
        //    await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        //        new ClaimsPrincipal(claimsIdentity), authProperties);
        //}
    }
}