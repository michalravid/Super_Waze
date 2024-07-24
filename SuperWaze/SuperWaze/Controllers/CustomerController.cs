using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.Models;
using DAL.Interfaces;
using MODELS;
using BL.Services;

namespace Super_Waze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomer _customer;
        private readonly JWT _jwt;

        public CustomerController(ICustomer customer, JWT jwt)
        {
            _customer = customer;
            _jwt = jwt;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] Customer customer)
        {
            // הוספת הלקוח ל-DB
            _customer.AddCustomer(customer);

            // יצירת ה-JWT Token
            var token = _jwt.GenerateJwtToken(customer);

            return Ok(new { Token = token });
        }
    }
}