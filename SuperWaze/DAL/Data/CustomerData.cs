using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using MODELS;
using MODELS.Models;

namespace DAL.Data
{
    public class CustomerData: ICustomer
    {
        private readonly ProjectContext _context;
        public CustomerData(ProjectContext context)
        {
            _context = context; 
        }

        public async Task<bool> AddCustomer(Customer c)
        {
            await _context.Customers.AddAsync(c);
            var isOk = _context.SaveChanges() >= 0;
            return isOk;
        }
        public async Task<Customer> GetCustomerById(int id)
        {
            Customer customer = await _context.Customers.SingleOrDefaultAsync(x => x.Id_Customer == id);
            return customer;
        }
      
    }
}
