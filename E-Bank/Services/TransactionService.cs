using E_Bank.Models;
using E_Bank.Repository;

using static E_Bank.Repository.IRepository;

namespace E_Bank.Services
{
    public class TransactionService : ITransactionService
    {
        private IRepository<TransactionClass> _repository;

        public TransactionService(IRepository<TransactionClass> repository)
        {
            this._repository = repository;
        }
       
        public List<TransactionClass> GetAll()
        {
            return _repository.GetAll().Where(tran=>tran.IsActive).ToList();

        }
        public TransactionClass GetById(int id)
        {
            var tableName = _repository.Get();
            var DataFound = tableName.Where(cus => cus.IsActive == true && cus.TransactionId == id)
                  .OrderBy(cus => cus.TransactionId)
                  .FirstOrDefault();
            if (DataFound != null)
            {
                _repository.Detached(DataFound);
            }
            return DataFound;
        }

        public int Add(TransactionClass transaction)
        {
            return _repository.Add(transaction);
        }

        public TransactionClass Update(TransactionClass transaction)
        {
            return _repository.Update(transaction);
        }

        public void Delete(TransactionClass transaction)
        {
            _repository.delete(transaction);

        }
     

    }
}
