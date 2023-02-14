using System;
using System.Collections.Generic;

namespace BFS
{
    class Vertex
    {
        public List<Vertex> Vertexes = new List<Vertex>();
        private int number;
        public bool isTaken = false;

        public Vertex(int Number)
        {
            number = Number;
        }


        public int GetNumber()
        {
            return number;
        }
        public void AddVertex(Vertex vertex)
        {
            if(!Vertexes.Contains(vertex))
                Vertexes.Add(vertex);
        }
    }
}
