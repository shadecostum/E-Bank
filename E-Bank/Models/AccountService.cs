namespace E_Bank.Models
{
    public class AccountService:IAccountService
    {
        private IRepository<Account> _repository;

        public AccountService(IRepository<Account> repository)
        {
            _repository = repository;
        }

        public int Add(Account account)
        {
            return _repository.Add(account);
        }

        public Account GetById(int id)
        {
          var account= _repository.Get()
                .Where(acn=>acn.AccountNumber == id && acn.IsActive)
                .OrderBy(acn=>acn.AccountNumber)
                .FirstOrDefault();
            if(account != null)
            {
                _repository.Detached(account);
            }
            return account;

        }
    }
}
