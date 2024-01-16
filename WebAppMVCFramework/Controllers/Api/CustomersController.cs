using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using WebAppMVCFramework.Models;

using System.Data.Entity;
using WebAppMVCFramework.DTO.ApiDTO;
using AutoMapper;

namespace WebAppMVCFramework.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            IEnumerable<CustomerApiDTO> Customers = _context.Customers.Include(x => x.MembershipType).ToList().Select(Mapper.Map<Customer, CustomerApiDTO>);
            return Ok(Customers);
        }

        //GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {

            logger.Info($"User {User.Identity.Name} requested Customer Details for ID : {id}");

            var customer = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customer == null) return NotFound();

            var customerDTO = Mapper.Map<Customer, CustomerApiDTO>(customer);

            customerDTO.Id = id;

            return Ok(customerDTO);
        }

        //POST /api/customers
        [HttpPost]
        [Authorize(Roles = Ruoli.canManageMovies)]

        public IHttpActionResult CreateCustomer(CustomerApiDTO customerDto)
        {
            if (!ModelState.IsValid)
            {
                //equivale a HTTP BadRequest
                return BadRequest();
            }

            var customer = Mapper.Map<CustomerApiDTO, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            logger.Info($"Customer created: {customer.Id}");

            //necessario per popolare il campo ID autogenerato durante la Add
            customerDto.Id = customer.Id;

            //return code 201 CREATED + Location URI
            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id),customerDto);
        }

        //PUT /api/customers/1
        [HttpPut]
        [Authorize(Roles = Ruoli.canManageMovies)]

        public IHttpActionResult UpdateCustomer(int id, CustomerApiDTO customerDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var customerInDB = _context.Customers.SingleOrDefault(x => x.Id == id);

            if(customerInDB == null) return NotFound();

            Mapper.Map<CustomerApiDTO, Customer>(customerDto, customerInDB);

            _context.SaveChanges();

            logger.Info($"User {User.Identity.Name} Updated the Customer with ID : {customerDto.Id}");

            return Ok();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        [Authorize(Roles = Ruoli.canManageMovies)]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDB = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customerInDB == null) return NotFound();

            _context.Customers.Remove(customerInDB);
            _context.SaveChanges();

            logger.Info($"User {User.Identity.Name} Deleted the Customer with ID : {id}");

            return Ok();
        }
    }
}
