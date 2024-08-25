namespace WebApplication1.Models
{
    public class AddCustomerViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime Registration { get; set; } = DateTime.Now;
    }

}
