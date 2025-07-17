using Microsoft.EntityFrameworkCore;
using Pessoa.Models;
using Pessoa.Data;
using Pessoa.Util;

namespace Pessoa.Routes;


public static class PessoaRoute{
    public static void PessoaRoutes( this WebApplication app){
        
        var route = app.MapGroup("pessoa");


        route.MapPost("", async (PessoaRequest request, PessoaContext context) =>
        {
            
            if(!Validations.CnpjEhValido(request.cnpj))
                return Results.BadRequest("Não Funfou");


            PessoaModel pessoa = new PessoaModel(request.nome, request.cnpj, request.telefone, request.email);

            await context.AddAsync(pessoa);
            await context.SaveChangesAsync();

            return Results.Created($"Pessoa", pessoa);

        });

        
        route.MapGet("", async(PessoaContext context) =>
        {
            List<PessoaModel> pessoa = await context.Pessoa.ToListAsync();
            return Results.Ok(pessoa);
        });
    }
}

