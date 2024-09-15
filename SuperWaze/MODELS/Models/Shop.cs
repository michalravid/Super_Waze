using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class Shop
    {
        public int Id_Shop { get; set; }
        public string Name { get; set; }
        public string MapJson { get; set; } // שדה שיאחסן את ה-JSON

        [NotMapped]
        public bool[,] MapMatrix { get; set; } // מטריצה שלא תמופה ישירות ל-DB

    }
}
