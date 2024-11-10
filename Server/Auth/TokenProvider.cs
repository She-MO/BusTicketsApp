﻿using System.Security.Claims;
using System.Text;
using BusTicketsApp.Server.Data;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BusTicketsApp.Server.Auth;
public sealed class TokenProvider
{
   private IConfiguration _configuration;

   public TokenProvider(IConfiguration configuration)
   {
      _configuration = configuration;
   }

   public string Create(User user)
   {
      string secretKey = _configuration["Jwt:Secret"]!;
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
         Subject = new ClaimsIdentity([
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("Role", user.Role.ToString())
         ]),
         Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
         SigningCredentials = credentials,
         Issuer = _configuration["Jwt:Issuer"],
         Audience = _configuration["Jwt:Audience"]
      };
      var handler = new JsonWebTokenHandler();
      string token = handler.CreateToken(tokenDescriptor);
      return token;
   }
}
