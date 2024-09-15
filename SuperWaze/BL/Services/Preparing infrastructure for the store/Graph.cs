using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Preparing_infrastructure_for_the_store
{
    public class Graph
    {
        public List<Node> Nodes { get; set; } = new List<Node>();

        // פונקציה להוספת צומת לגרף
        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }

        // פונקציה להוספת קשת בין שני צמתים
        public void AddEdge(Node from, Node to, int weight)
        {
            from.Edges.Add(new Edge(to, weight));
        }
    }
}
