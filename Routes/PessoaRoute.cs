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


            PessoaModel pessoa = new PessoaModel(request.nome, request.cnpj, request.telefone, request.email, request.ativo);

            await context.AddAsync(pessoa);
            await context.SaveChangesAsync();

            return Results.Created($"Pessoa", pessoa);

        });

        route.MapPut("{id:guid}", async ( Guid id, PessoaRequest request, PessoaContext context) =>
        {
            var pessoa = await context.Pessoa.FirstOrDefaultAsync(p => p.Id == id);
            var pesso = await context.Pessoa.FindAsync(id);

            if(pessoa == null){
                return Results.NotFound();
            }

            pessoa.MudarNome(request.nome);

            await context.SaveChangesAsync();
            return Results.Created($"/Pessoa/{pessoa.Id}", pessoa);
        

        });

        
        route.MapDelete("{id:guid}", async ( Guid id,  PessoaContext context) =>
        {
            var pessoa = await context.Pessoa.FindAsync(id);

            if(pessoa == null)
                return Results.NotFound();

            pessoa.Desativar();

           await context.SaveChangesAsync();
           return Results.Created($"Pessoa deletada:{pessoa.Id}(não ativa)", pessoa);
        
    

        });

                
        route.MapPut("{id:guid}", async ( Guid id,  PessoaContext context) =>
        {
            var pessoa = await context.Pessoa.FindAsync(id);

            if(pessoa == null)
                return Results.NotFound();

            context.Pessoa.Remove(pessoa);

            await context.SaveChangesAsync();
            return Results.Ok($"Pessoa com id:{id} Deletada com sucesso");
        

        });


        
        route.MapGet("", async(PessoaContext context) =>
        {
            List<PessoaModel> pessoa = await context.Pessoa.ToListAsync();
            return Results.Ok(pessoa);
        });
    }
}

