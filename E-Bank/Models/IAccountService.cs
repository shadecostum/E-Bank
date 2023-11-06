namespace E_Bank.Models
{
    public interface IAccountService
    {
        public Account GetById(int id);

        public int Add(Account account);
    }
}
