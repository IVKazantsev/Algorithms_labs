using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Dijkstra
{
    public class ShortestPathFinder
    {
        private int[,] Matrix { get; }
        private int MatrixSize { get; }
        public ShortestPathFinder(int[,] matrix, int matrixSize)
        {
            Matrix = matrix;
            MatrixSize = matrixSize;
        }
        private int MinDistance(int[] distance, bool[] visited)
        {
            int minDist = int.MaxValue;
            int minIndex = -1;
            for (int i = 0; i < MatrixSize; i++)
                if (!visited[i] && distance[i] <= minDist)
                {
                    minDist = distance[i];
                    minIndex = i;
                }
            return minIndex;
        }
        public int[] Dijkstra(int[,] matrix, int root)
        {
            int[] distance = new int[MatrixSize];
            bool[] visited = new bool[MatrixSize];
            for (int i = 0; i < MatrixSize; i++) // Все точки с oo расстоянием и не посещены
            {
                distance[i] = int.MaxValue;
                visited[i] = false;
            }
            distance[root] = 0;
            for (int i = 0; i < MatrixSize; i++)
            {
                int minIndex = MinDistance(distance, visited);
                visited[minIndex] = true;
                for (int j = 0; j < MatrixSize; j++)
                    if (!visited[j] && matrix[minIndex, j] != 0 && distance[minIndex] + matrix[minIndex, j] < distance[j])
                        distance[j] = distance[minIndex] + matrix[minIndex, j];
            }
            return distance;
        }
    }
}
