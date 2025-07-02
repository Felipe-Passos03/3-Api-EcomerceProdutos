
namespace _3_Api_EcomerceProdutos.Application.Validators;

public static class CpfValidator
{
    
    public static bool IsValid(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return false;

        // Verifica se todos os dígitos são iguais
        if (cpf.Distinct().Count() == 1)
            return false;

        // Valida os dígitos verificadores
        var cpfArray = cpf.Select(c => int.Parse(c.ToString())).ToArray();

        int soma1 = 0;
        for (int i = 0; i < 9; i++)
            soma1 += cpfArray[i] * (10 - i);

        int digito1 = soma1 % 11;
        digito1 = digito1 < 2 ? 0 : 11 - digito1;

        if (cpfArray[9] != digito1)
            return false;

        int soma2 = 0;
        for (int i = 0; i < 10; i++)
            soma2 += cpfArray[i] * (11 - i);

        int digito2 = soma2 % 11;
        digito2 = digito2 < 2 ? 0 : 11 - digito2;

        return cpfArray[10] == digito2;
    }

}