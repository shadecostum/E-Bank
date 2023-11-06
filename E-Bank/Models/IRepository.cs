using E_Bank.Data;

namespace E_Bank.Models
{
    public interface IRepository<T>
    {
        public IQueryable<T> GetAll();

        public IQueryable<T> Get();
        public void Detached(T entity);

        public int Add(T entity);

        public T Update(T entity);

        public void delete(T entity);
    }
}
