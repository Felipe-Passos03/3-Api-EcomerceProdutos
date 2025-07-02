using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _3_Api_EcomerceProdutos.Domain;
using _3_Api_EcomerceProdutos.Domain.Model.JWTToken;
using Microsoft.IdentityModel.Tokens;

namespace _3_Api_EcomerceProdutos.Domain.Service;

public static class TokenService 
{
    private static JwtSettings?  _jwtSettings;

    public static void Initialize(IConfiguration configuration)
    {
        _jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
    }

    public static string GenerateToken(string email)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, "User")
        };
        
        return GenerateToken(claims, _jwtSettings.UserExpirationHours); 
    }

    private static string GenerateToken(List<Claim> claims, int expirationHours)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(expirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}