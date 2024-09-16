namespace Library.Domain.Interface;
public interface IGenericRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task<T> GetAsync(Guid id);
    T Update(T entity); //no need async, doesnt involve I/O operations
    bool Delete(T entity);  //spesific instantiation for delation
}