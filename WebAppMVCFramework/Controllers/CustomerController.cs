using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVCFramework.DTO;
using WebAppMVCFramework.Models;

using System.Data.Entity;


namespace WebAppMVCFramework.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("Customer")]
        public ActionResult Index()
        {    
            if(User.IsInRole(Ruoli.canManageMovies))
            {
                return View("Index");
            }

            return View("IndexReadOnly");
        }

        [Route("Customer/Details/{id}")]
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(x => x.MembershipType).FirstOrDefault(x => x.Id == id);

            if (customer == null) { return HttpNotFound(); }

            logger.Info($"User {User.Identity.Name} requested Customer Details for ID : {id}");

            return View("Details", customer);
        }

        [Route("Customer/New")]
        [Authorize(Roles = Ruoli.canManageMovies)]

        public ActionResult New()
        {
            CustomerFormDTO dto = new CustomerFormDTO()
            {
                Customer = new Customer(),
                MembershipTypes = _context.MembershipTypes.ToList(),
            };
            return View("CustomerForm", dto);
        }

        [Route("Customer/Edit/{id}")]
        [Authorize(Roles = Ruoli.canManageMovies)]

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            CustomerFormDTO dto = new CustomerFormDTO()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", dto);
        }

        [HttpPost]
        [Route("Customer/Save")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Ruoli.canManageMovies)]

        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid) {
                var c = new CustomerFormDTO()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList(),
                };
                return View("CustomerForm",c);
            }


            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();

                logger.Info($"User {User.Identity.Name} created a new customer ID: {customer.Id}");

            }
            else
            {
                var customerOld = _context.Customers.Single(x => x.Id == customer.Id);
                customerOld.Name = customer.Name;
                customerOld.Cognome = customer.Cognome;
                customerOld.Birthdate = customer.Birthdate;
                customerOld.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerOld.MembershipTypeId = customer.MembershipTypeId;
                _context.SaveChanges();

                logger.Info($"User {User.Identity.Name} Updated the Customer with ID : {customer.Id}");
            }

            return RedirectToAction("Index", "Customer");
        }
    }
}