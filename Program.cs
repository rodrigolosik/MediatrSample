using MediatSample.Application.Models;
using MediatSample.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddSingleton<IRepository<Pessoa>, PessoaRepository>()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/Pessoas", async ([FromServices] IRepository<Pessoa> repository) =>
{
    return Results.Ok(await repository.GetAllAsync());
})
.WithName("ListarPessoas")
.WithOpenApi();

app.MapGet("/Pessoas/{id}", async ([FromServices] IRepository<Pessoa> repository, int id) =>
{
    return Results.Ok(await repository.Get(id));
})
.WithName("PegarPessoaPorId")
.WithOpenApi();

app.MapPost("/Pessoas", async ([FromServices] IRepository<Pessoa> repository, [FromBody] Pessoa pessoa) =>
{
    await repository.Add(pessoa);

    return Results.Created();
})
.WithName("CriarPessoa")
.WithOpenApi();

app.MapPut("/Pessoas", async ([FromServices] IRepository<Pessoa> repository, [FromBody] Pessoa pessoa) =>
{
    await repository.Edit(pessoa);

    return Results.NoContent();
})
.WithName("EditarPessoa")
.WithOpenApi();

app.MapDelete("/Pessoas/{id}", async ([FromServices] IRepository<Pessoa> repository, int id) =>
{
    await repository.Delete(id);

    return Results.NoContent();
})
.WithName("RemoverPessoa")
.WithOpenApi();

app.Run();


