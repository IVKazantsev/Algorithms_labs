using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _8Dijkstra
{
    class Program
    {
        static void Main(string[] args)
        {
            // Чтение Файла //////////////////////////////////////////
            StreamReader file = new StreamReader("Matrix.txt");
            List<string> linesOfFile = new List<string>();
            List<Vertex> Vertexes = new List<Vertex>();
            while (file.Peek() > -1)
                linesOfFile.Add(file.ReadLine());
            file.Close();
            // Создание списка вершин /////////////////////////////////
            int numberOfVertex = 0; int countOfTab = 0;
            for (int i = 0; i < linesOfFile.Count; i++)
            {
                Vertex vertex = new Vertex(i);
                Vertexes.Add(vertex);
            }
            for (int i = 0; i < linesOfFile.Count - 1; i++)
            {
                countOfTab = 0;
                numberOfVertex = -1;
                for (int k = 0; k < linesOfFile[i].Length; k++) // Идём с i-го, чтобы не читать нижнюю часть матрицы
                {
                    while (countOfTab < i + 1)
                    {
                        if (linesOfFile[i][k] == '\t')
                            countOfTab++;
                        else
                        {
                            if (k + 1 < linesOfFile[i].Length)
                                if (linesOfFile[i][k + 1] != '\t' && (linesOfFile[i][k + 1] != '\n'))
                                {
                                    numberOfVertex++;
                                    k++;
                                }
                                else
                                    numberOfVertex++;
                        }
                        k++;
                    }
                    if (linesOfFile[i][k] == '\t')
                        continue;
                    numberOfVertex++;
                    if (linesOfFile[i][k] == '0')
                        continue;
                    if (k + 1 < linesOfFile[i].Length)
                        if (linesOfFile[i][k + 1] != '\t' && (linesOfFile[i][k + 1] != '\n'))
                        {
                            Vertexes[i].AddVertex(Vertexes.Find(vertex => vertex.GetNumber() == numberOfVertex), ((int)linesOfFile[i][k] - 48) * 10 + ((int)linesOfFile[i][k + 1] - 48));
                            Vertexes[numberOfVertex].AddVertex(Vertexes.Find(vertex => vertex.GetNumber() == i), ((int)linesOfFile[i][k] - 48) * 10 + ((int)linesOfFile[i][k + 1] - 48));
                            k++;
                            continue;
                        }
                    Vertexes[i].AddVertex(Vertexes.Find(vertex => vertex.GetNumber() == numberOfVertex), (int)linesOfFile[i][k] - 48);
                    Vertexes[numberOfVertex].AddVertex(Vertexes.Find(vertex => vertex.GetNumber() == i), (int)linesOfFile[i][k] - 48);
                }
            }
            int countOfVertexes = linesOfFile.Count;
            linesOfFile.Clear();

            int[,] Adjacency = new int[countOfVertexes, countOfVertexes];
            for (int i = 0; i < countOfVertexes; i++)
                for (int j = 0; j < countOfVertexes; j++)
                    if (Vertexes[i].ConnectedVertexes.Any(vertex => vertex.GetNumber() == j))
                        Adjacency[i, j] = Vertexes[i].Weight[Vertexes[i].ConnectedVertexes.FindIndex(vertex => vertex.GetNumber() == j)];
                    else Adjacency[i, j] = 0;

            Console.WriteLine("Матрица смежности:");
            for (int i = 0; i < countOfVertexes; i++)
            {
                for (int j = 0; j < countOfVertexes; j++)
                    Console.Write(Adjacency[i, j] + "\t");
                Console.WriteLine();
            }

            ShortestPathFinder finder = new ShortestPathFinder(Adjacency, countOfVertexes);
            Console.WriteLine("Кратчайшие пути:");
            for (int i = 0; i < countOfVertexes; i++)
            {
                for (int j = 0; j < countOfVertexes; j++)
                    Console.Write(finder.Dijkstra(Adjacency, i)[j] + "\t");
                Console.WriteLine();
            }
        }
    }
}
