using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.App_Start;
using Vidly.DTOS;
using Vidly.Models;
using System.Data.Entity;
namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //get /api/customers/
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers
                .Include(c=>c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
        }
        //Get /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound(); //throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }
        //Post 
        //Api/Customers/
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(); //throw new HttpResponseException(HttpStatusCode.BadRequest);
            

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id; 
            //api/customers/10
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto); //customerDto;
        }
        //edit
        //Api/Customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();//throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customerInDb == null)
                return NotFound(); /*throw new HttpResponseException(HttpStatusCode.NotFound)*/
            Mapper.Map(customerDto, customerInDb);
            _context.SaveChanges();
            return Ok();
        }
        //Delete
        //Api/Customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customer=_context.Customers.SingleOrDefault(c => c.Id == id);
            if(customer==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}
