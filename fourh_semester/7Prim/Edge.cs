using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7Prim
{
    class Edge
    {
        private Vertex[] Vertexes = new Vertex[2];
        private int weight;

        public Edge(Vertex Vertex1, Vertex Vertex2, int Weight)
        {
            Vertexes[0] = Vertex1;
            Vertexes[1] = Vertex2;
            weight = Weight;
        }
        public Vertex[] GetVertexes()
        {
            return Vertexes;
        }
        public int GetWeight()
        {
            return weight;
        }
    }
}
