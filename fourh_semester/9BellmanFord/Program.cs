using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _9BellmanFord
{
    class Program
    {
        static void Main(string[] args)
        {
            // Чтение Файла /////////////////////////////////
            StreamReader file = new StreamReader("Matrix.txt");
            List<string> linesOfFile = new List<string>();
            List<Vertex> Vertexes = new List<Vertex>();
            while (file.Peek() > -1)
                linesOfFile.Add(file.ReadLine());
            file.Close();
            // Создание списка вершин ///////////////////////
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
                    if (linesOfFile[i][k] == '-') // Обработка отрицательных значений
                    {
                        k++;
                        if (k + 1 < linesOfFile[i].Length)
                            if (linesOfFile[i][k + 1] != '\t' && (linesOfFile[i][k + 1] != '\n'))
                            {
                                Vertexes[i].AddVertex(Vertexes.Find(vertex => vertex.GetNumber() == numberOfVertex), (-1) * (((int)linesOfFile[i][k] - 48) * 10 + ((int)linesOfFile[i][k + 1] - 48)));
                                Vertexes[numberOfVertex].AddVertex(Vertexes.Find(vertex => vertex.GetNumber() == i), (-1) * (((int)linesOfFile[i][k] - 48) * 10 + ((int)linesOfFile[i][k + 1] - 48)));
                                k++;
                                continue;
                            }
                        Vertexes[i].AddVertex(Vertexes.Find(vertex => vertex.GetNumber() == numberOfVertex), (-1) * ((int)linesOfFile[i][k] - 48));
                        Vertexes[numberOfVertex].AddVertex(Vertexes.Find(vertex => vertex.GetNumber() == i), (-1) * ((int)linesOfFile[i][k] - 48));
                    }
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
            // Создание списка рёбер ////////////////////////
            List<Edge> Edges = new List<Edge>();
            foreach (var vert in Vertexes)
                for (int i = 0; i < vert.ConnectedVertexes.Count; i++)
                    if (!Edges.Any(edge =>
                    ((edge.GetV1() == vert.GetNumber()) && (edge.GetV2() == vert.ConnectedVertexes[i].GetNumber()))
                    || (edge.GetV2() == vert.GetNumber()) && (edge.GetV1() == vert.ConnectedVertexes[i].GetNumber())))
                    {
                        Edge edge = new Edge(vert.GetNumber(), vert.ConnectedVertexes[i].GetNumber(), vert.Weight[i]);
                        Edges.Add(edge);
                    }
            // Беллман-Форд /////////////////////////////////
            ShortestPathFinder shortestPathFinder = new ShortestPathFinder(Edges, countOfVertexes);
            Console.WriteLine("От 1 вершины");
            for (int i = 0; i < countOfVertexes; i++)
                Console.WriteLine($"до {i + 1} вершины Вес: {shortestPathFinder.BellmanFord(Edges, 0)[i, 0]} Длина пути: {shortestPathFinder.BellmanFord(Edges, 0)[i, 1]}");
        }
    }
}