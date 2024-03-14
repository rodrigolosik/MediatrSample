namespace MediatSample.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> Get(int id);
    Task Add(T item);
    Task Edit(T item);
    Task Delete(int id);
}
