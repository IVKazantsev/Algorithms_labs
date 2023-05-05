using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9BellmanFord
{
    class Edge
    {
        private int V1;
        private int V2;
        private int weight;

        public Edge(int v1, int v2, int Weight)
        {
            V1 = v1;
            V2 = v2;
            weight = Weight;
        }
        public int GetV1()
        {
            return V1;
        }
        public int GetV2()
        {
            return V2;
        }
        public int GetWeight()
        {
            return weight;
        }
    }
}
