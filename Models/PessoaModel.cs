namespace Pessoa.Models;

public class PessoaModel
{
    public Guid Id { get; init;}

    public string? Nome { get; private set; }

    public string? Cnpj {get; private set; }

    public string? Telefone {get; private set; }

    public string? Email {get; private set; }

    public bool Ativo {get; private set; }

    public PessoaModel(string nome, string cnpj, string telefone, string email, bool ativo 
    ){
        Nome = nome;
        Cnpj = cnpj; 
        Telefone = telefone; 
        Email = email;
        Id = Guid.NewGuid();
        Ativo = true;
    }

    public void MudarNome(string nome)
    {
        Nome = nome;
    }

    public void Desativar()
    {
        Ativo = false;
    }
}