
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        DBContext context;
        public CustomerRepository(DBContext context)
        {
            this.context = context;
        }
        public List <Customer> GetAll ()
        {
            return context.Customers.ToList();
        }
        public Customer GetCustomerByID (string id)
        {
            return context.Customers.FirstOrDefault(c => c.Id == id);
        }
        public void insert (Customer obj)
        {
            context.Add(obj);
        }
        public void update (Customer obj)
        {
            context.Update(obj);
        }
        public void delete (Customer obj) 
        {
            context.Remove(obj);
        }
        public void Save()
        {
            context.SaveChanges();
        }

    }
}
