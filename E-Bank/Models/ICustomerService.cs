namespace E_Bank.Models
{
    public interface ICustomerService
    {

        public List<Customer> GetAll();

        public Customer  GetById(int id);

        public int Add(Customer customer);

        public Customer Update(Customer customer);

        public void Delete(Customer customer);






    }
}
