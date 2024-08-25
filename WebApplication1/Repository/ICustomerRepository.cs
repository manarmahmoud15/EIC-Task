using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface ICustomerRepository
    {
        public List<Customer> GetAll();
        public Customer GetCustomerByID(string id);
        public void insert(Customer obj);
        public void update(Customer obj);
        public void delete(Customer obj);
        public void Save();

    }
}
