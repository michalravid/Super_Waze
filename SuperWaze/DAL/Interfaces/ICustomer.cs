using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODELS;
using MODELS.Models;

namespace DAL.Interfaces
{
    public interface ICustomer
    {
        Task<bool> AddCustomer(Customer c);
        Task<Customer> GetCustomerById(int id);  

    }
}
