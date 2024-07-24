using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL.Interfaces;
using MODELS;
using DAL.Data;

namespace BL.Services
{
    public class JWT
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly ICustomer _Customer;

        public JWT(IConfiguration configuration, ICustomer Customer)
        {
            _key = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _Customer = Customer;
        }
        public string GenerateJwtToken(Customer customer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name,customer.Name)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Task<Customer> Authenticate(int idCustomer)
        {
            var customer = _Customer.GetCustomerById(idCustomer);
            return customer;
        }
    }

}
