namespace Library.Domain.Interface;
public interface IGenericRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    T Update(T entity); //no need async, doesnt involve I/O operations
    Task<T> GetAsync(int id);
    bool Delete(T entity);  //spesific instantiation for delation
}