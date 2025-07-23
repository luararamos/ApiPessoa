namespace Pessoa.Models;

public record PessoaRequest(string nome, string cnpj,string telefone, string email, bool ativo);