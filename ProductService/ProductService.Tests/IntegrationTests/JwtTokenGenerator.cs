using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.IntegrationTests
{
    public class JwtTokenGenerator
    {
        public static string GenerateJwt()
        {
            var key = Environment.GetEnvironmentVariable("SECRET");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "R0sale"),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var token = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "https://localhost:7269",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
