using E_Bank.Models;
using static E_Bank.Repository.IRepository;

namespace E_Bank.Services
{
    public class QueryService : IQueryService
    {


      IRepository<Query> _repository;
       

        public QueryService(IRepository<Query> repository)
        {
            _repository = repository;
           
        }




        public int Add(Query customer)
        {
            return _repository.Add(customer);
        }

        public void Delete(Query customer)
        {
            _repository.delete(customer);
           

        }

        public List<Query> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Query GetById(int id)
        {
            var tableName = _repository.Get();
            var DataFound = tableName.Where(qu=>qu.QueryId == id).OrderBy(qu=>qu.QueryId)
                .FirstOrDefault();

            if (DataFound != null)
            {
                _repository.Detached(DataFound);
            }
            return DataFound;
        }

        public Query Update(Query customer)
        {
            return _repository.Update(customer);
        }
    }
}
