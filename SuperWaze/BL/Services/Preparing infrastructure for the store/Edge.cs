using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Preparing_infrastructure_for_the_store
{
    public class Edge
    {
        public Node Target { get; set; }  // הצומת אליו הקשת מובילה
        public int Weight { get; set; }   // המשקל (עלות המעבר) של הקשת

        public Edge(Node target, int weight)
        {
            Target = target;
            Weight = weight;
        }
    }
}
