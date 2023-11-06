using E_Bank.Models;

namespace E_Bank.Services
{
    public interface IAccountService
    {
        public Account GetById(int id);

        public int Add(Account account);
    }
}
