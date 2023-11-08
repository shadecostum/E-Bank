using E_Bank.Models;


namespace E_Bank.Services
{
    public interface ITransactionService
    {
        public int Add(TransactionClass transaction);

        public TransactionClass GetById(int id);
        public List<TransactionClass> GetAll();

        public TransactionClass Update(TransactionClass transaction);

        public void Delete(TransactionClass transaction);
    }
}
