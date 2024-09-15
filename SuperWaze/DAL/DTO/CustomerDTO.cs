using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class CustomerDTO
    {
        public string Name { get; set; }
        public int Id_Customer { get; set; }
        public ICollection<ProductDTO> Cart { get; set; }
        public ICollection<ShopDTO> Shops { get; set; }
    }
}
