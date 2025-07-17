namespace Pessoa.Util;

public static class Validations
{
    public static bool CnpjEhValido( string cnpj)
    {
        if(string.IsNullOrEmpty(cnpj)) return false;

        cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

        if(cnpj.Length != 14) return false;
        
        int[] multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string validacao = cnpj.Substring(0,12);

        int soma =  validacao.Select((caracter, i) => (caracter - '0') * multiplier1[i]).Sum();
        int restoDivisao = soma % 11;
        int digitoPrimeiroVerificador = restoDivisao < 2 ? 0 : 11- restoDivisao;

        validacao +=  digitoPrimeiroVerificador;

        soma =  validacao.Select((caracter, i) => (caracter - '0') * multiplier2[i]).Sum();
        restoDivisao = soma % 11;
        int digitoSegundoVerificador = restoDivisao < 2 ? 0 : 11- restoDivisao;

        return cnpj.EndsWith($"{digitoPrimeiroVerificador}{digitoSegundoVerificador}");
    }

}