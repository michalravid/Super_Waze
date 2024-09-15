using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Preparing_infrastructure_for_the_store
{
    public class Node
    {
        public int X { get; set; }  // מיקום X במטריצה
        public int Y { get; set; }  // מיקום Y במטריצה
        public List<Edge> Edges { get; set; } = new List<Edge>();  // רשימת הקשתות המחברות לצומת הזה
        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

}
