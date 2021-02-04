using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    

    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult CustomerForm()
        {
            var memberShip = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = memberShip
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                    {Customer = customer, MembershipTypes = _context.MembershipTypes.ToList()};
                return View("CustomerForm",viewModel);
            }
            //if Id== 0 this means is new Customer
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                //update Customer
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                //ref to database 
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerInDb.Name = customer.Name;
                customerInDb.MemberShipTypeId = customer.MemberShipTypeId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
        // GET: Customer
        public ActionResult Index()
        {
            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View();//customers
        }

        public ActionResult Details(int? id)
        {
            //entity framework only loads customers objects not related objects so we combine Include 
            var customer = _context.Customers
                .Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null) return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }
    }
}