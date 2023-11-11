using E_Bank.Dto;
using E_Bank.Models;

namespace E_Bank.Services
{
    public interface IAccountService
    {
        public List<Account> GetAll();

        public List<Account> GetAllRequest();

        public int ActivateRequest(int id);

        public Account GetById(int id);

        public int Add(Account account);

        public Account Update(Account account);

        public void Delete(Account account);
    }
}
