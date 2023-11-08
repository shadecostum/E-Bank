using E_Bank.Models;

namespace E_Bank.Services
{
    public interface IQueryService
    {
        public List<Query> GetAll();

        public Query GetById(int id);

        public int Add(Query customer);

        public Query Update(Query customer);

        public void Delete(Query customer);
    }
}
