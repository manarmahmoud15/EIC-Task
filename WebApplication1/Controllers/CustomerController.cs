using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DbContext;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly DBContext dbContext;
        ICustomerRepository CustomerRepository;

        public CustomerController(DBContext dbContext , ICustomerRepository customerRepository)
        {
            this.dbContext = dbContext;
            this.CustomerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult AddNewCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewCustomer(Customer Model)
        {
            CustomerRepository.insert(Model);
            CustomerRepository.Save();
            return View();
        }

        [HttpGet]
        public IActionResult AllCustomer() {
            var customer =  CustomerRepository.GetAll();
            return View(customer);
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var customer =  CustomerRepository.GetCustomerByID(id);
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(Customer Model)
        {
            var customer = CustomerRepository.GetCustomerByID(Model.Id);
            if (customer != null)
            {
                customer.Registration = Model.Registration;
                customer.Code = Model.Code;
                customer.Name = Model.Name;

                CustomerRepository.Save();
            }

            return RedirectToAction("AllCustomer");
        }
        [HttpPost]
        public  IActionResult Delete (string id)
        {
            var customer = CustomerRepository.GetCustomerByID(id);
             
            if (customer != null)
            {
                CustomerRepository.delete(customer);

                CustomerRepository.Save();
            }

            return RedirectToAction("AllCustomer");
        }
        public IActionResult PrintReport()
        {
            var rptPath = "WebApplication1.Reports.PrintReport";
            XtraReport report = (XtraReport)Activator.CreateInstance(Type.GetType(rptPath));
            ViewBag.ReportName = report;
            ViewBag.ReportHeader = "Print Customer Data";
            return View("~/Views/reportViewer.cshtml");
        }
    }
}
