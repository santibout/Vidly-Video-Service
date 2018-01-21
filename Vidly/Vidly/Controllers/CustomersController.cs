using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var person = _context.Customers.Single(c => c.Id == customer.Id);

                // Mapper.Map(customer, person);
                person.Name = customer.Name;
                person.BirthDate = customer.BirthDate;
                person.MembershipTypeId = customer.MembershipTypeId;
                person.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult CustomerForm()
        {
            CustomerFormViewModel view = new CustomerFormViewModel
            {
                MembershipType = _context.MembershipTypes.ToList()
            };
            return View(view);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            CustomerFormViewModel view = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipType = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", view);
        }

        public ActionResult Index()
        {
            var People = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(People);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>()
            {
                 new Customer { Id = 1, Name = "John Smith" },
                 new Customer { Id = 2, Name = "Marry Williams" }
            };
            return customers;
        }
    }
}