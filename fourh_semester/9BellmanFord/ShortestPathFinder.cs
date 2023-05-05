﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9BellmanFord
{
    class ShortestPathFinder
    {
        private List<Edge> Edges { get; }
        private int VertexesCount { get; }
        public ShortestPathFinder(List<Edge> edges, int vertexesCount)
        {
            Edges = edges;
            VertexesCount = vertexesCount;
        }
        public int[,] BellmanFord(List<Edge> Edges, int root)
        {
            int edgesCount = Edges.Count;
            int[,] distance = new int[VertexesCount, 2];
            for (int i = 0; i < VertexesCount; i++)
                distance[i, 0] = int.MaxValue;
            distance[root, 0] = 0;
            distance[root, 1] = 0;
            for (int i = 0; i < VertexesCount; i++)
                for (int j = 0; j < edgesCount; j++)
                {
                    if (i != Edges[j].GetV1() && i != Edges[j].GetV2())
                        continue;
                    int parent = i;
                    int child = (Edges[j].GetV1() == i) ? Edges[j].GetV2() : Edges[j].GetV1();
                    int weight = Edges[j].GetWeight();
                    if (distance[parent, 0] != int.MaxValue && (distance[parent, 0] + weight) < distance[child, 0])
                    {
                        distance[child, 0] = distance[parent, 0] + weight;
                        distance[child, 1] = distance[parent, 1] + 1;
                    }
                }
            return distance;
        }
    }
}
