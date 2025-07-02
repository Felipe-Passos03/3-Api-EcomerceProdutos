namespace _3_Api_EcomerceProdutos.Application.Validators;

public class PasswordEncrypt
{
    public static string Encriptor(string senha)
    {
       return BCrypt.Net.BCrypt.HashPassword(senha);
    }
}