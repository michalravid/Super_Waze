using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class Customer
    {
        public string Name { get; set; }
        public int Id_Customer { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Shop> Shops { get; set; }

    }
}
