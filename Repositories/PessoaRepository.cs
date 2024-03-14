using MediatSample.Application.Models;

namespace MediatSample.Repositories;

public class PessoaRepository : IRepository<Pessoa>
{
    private static Dictionary<int, Pessoa> pessoas = new Dictionary<int, Pessoa>();

    public async Task Add(Pessoa pessoa) => await Task.Run(() => pessoas.Add(pessoa.Id, pessoa));

    public async Task Delete(int id) => await Task.Run(() => pessoas.Remove(id));

    public async Task Edit(Pessoa pessoa) =>
        await Task.Run(() =>
        {
            pessoas.Remove(pessoa.Id);
            pessoas.Add(pessoa.Id, pessoa);
        });

    public async Task<Pessoa> Get(int id) => await Task.Run(() => pessoas.GetValueOrDefault(id));

    public async Task<IEnumerable<Pessoa>> GetAllAsync() => await Task.Run(() => pessoas.Values.ToList());
}
