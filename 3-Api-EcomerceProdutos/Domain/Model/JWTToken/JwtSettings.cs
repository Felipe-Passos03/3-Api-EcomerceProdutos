namespace _3_Api_EcomerceProdutos.Domain.Model.JWTToken;

public class JwtSettings
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int UserExpirationHours { get; set; }
}