using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Client.Auth;

public static class CustomTokenValidator
{
    public static List<Claim> ValidateToken(string token, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
            }, out SecurityToken validatedToken);
        }
        catch
        {
            return new List<Claim>();
        }

        var securityToken = tokenHandler.ReadJwtToken(token);
        var claims = securityToken.Claims.ToList();
        //claims.Add(new Claim("jwt", token));
        return claims;
    }
}
