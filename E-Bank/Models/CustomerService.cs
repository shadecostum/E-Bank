using Microsoft.EntityFrameworkCore;

namespace E_Bank.Models
{
    public class CustomerService:ICustomerService  
    {

        IRepository<Customer> _repository;
        IRepository<Account> _accountRepository;

        public CustomerService(IRepository<Customer> repository,IRepository<Account> account)
        {
            _repository = repository;
            _accountRepository = account;   
        }


        public List<Customer> GetAll()
        {
            return _repository.GetAll().Where(cus => cus.IsActive)
                .Include(acnt => acnt.Accounts.Where(acnt => acnt.IsActive == true)).ToList();
        }

        public Customer GetById(int id)
        {
            var tableName = _repository.Get();
          var DataFound=  tableName.Where(cus=>cus.IsActive==true &&cus.CustomerId==id )
                .OrderBy(cus=>cus.CustomerId)
                .FirstOrDefault();
            if (DataFound != null)
            {
                _repository.Detached(DataFound);
            }
            return DataFound;
        }


        public int Add(Customer customer)
        {
          return _repository.Add(customer);
           
        }


        public Customer Update(Customer customer)
        {
           return _repository.Update(customer);

        }

        public void Delete(Customer customer)
        {
            _repository.delete(customer);
            var customerQuery=_accountRepository.Get();
            foreach(var item in customerQuery.Where(acn=>acn.CustomerId==customer.CustomerId).ToList())
            {
                _accountRepository.delete(item);
            }
           
        }
    }
}
