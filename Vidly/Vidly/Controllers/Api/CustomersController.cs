﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
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

        //GET /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }
        //GET /api/customers/1
        public CustomerDto GetCustomerById(int id)
        {
            var customer = _context.Customers.Single(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Customer, CustomerDto>(customer);
        }
        //POST /api/customers
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return customerDto;
        }
        //PUT /api/customers/1
        //you can either use customer or void
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var p = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (p == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, p);
           
            _context.SaveChanges();
        }
        //DELETE /api/customer/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}