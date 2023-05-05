using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _7Prim
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0; int j = 0; int k = 0;
            // Чтение Файла //////////////////////////////////////////
            StreamReader file = new StreamReader("Matrix.txt");
            List<string> linesOfFile = new List<string>();
            List<Vertex> Vertexes = new List<Vertex>();
            while (file.Peek() > -1)
                linesOfFile.Add(file.ReadLine());
            file.Close();
            // Создание списка рёбер /////////////////////////////////
            foreach (var line in linesOfFile)
            {
                for (k = i; k < linesOfFile.Count; k++) // Идём с i-го, чтобы не читать нижнюю часть матрицы
                    if (line[k] != '0')
                    {
                        Vertex Vertex1 = new Vertex(i);
                        Vertex Vertex2 = new Vertex(k);
                        Vertex1.AddVertex(Vertex2, (int)line[k] - 48);
                        Vertex2.AddVertex(Vertex1, (int)line[k] - 48);
                        if (!Vertexes.Any(vertex => vertex.GetNumber() == i))
                            Vertexes.Add(Vertex1);
                        else
                            Vertexes.Find(vertex => vertex.GetNumber() == i).AddVertex(Vertex2, (int)line[k] - 48);
                        if (!Vertexes.Any(vertex => vertex.GetNumber() == k))
                            Vertexes.Add(Vertex2);
                        else
                            Vertexes.Find(vertex => vertex.GetNumber() == k).AddVertex(Vertex1, (int)line[k] - 48);
                    }
                i++;
            }
            int countOfVertexes = i;
            linesOfFile.Clear();
            // Алгоритм Прима /////////////////////////////
            List<Vertex> MST = new List<Vertex>(); // MST - минимальное остовное дерево
            MST.Add(Vertexes[0]);
            int minWeight = int.MaxValue;
            int indOfMinWeight = 0;
            int indOfVertex = 0;
            int indOfLastVertex = 0;
            Console.WriteLine($"Минимальное остовное дерево:");

            while (MST.Count != countOfVertexes)
            {
                minWeight = int.MaxValue;
                indOfMinWeight = 0;
                for (i = 0; i < MST.Count; i++)
                {
                    for (j = 0; j < MST[i].Weight.Count; j++)
                    {
                        if ((MST[i].Weight[j] < minWeight) && !MST.Any(vertex => vertex.GetNumber() == MST[i].ConnectedVertexes[j].GetNumber()))
                        {
                            minWeight = MST[i].Weight[j];
                            indOfMinWeight = j;
                            indOfLastVertex = i;
                            indOfVertex = Vertexes.FindIndex(vertex => vertex.GetNumber() == MST[i].GetNumber());
                        }
                    }
                }
                if (!MST.Any(vertex => vertex.GetNumber() == Vertexes[indOfVertex].ConnectedVertexes[indOfMinWeight].GetNumber()))
                {
                    MST.Add(Vertexes.Find(vertex => vertex.GetNumber() == Vertexes[indOfVertex].ConnectedVertexes[indOfMinWeight].GetNumber()));
                    Console.WriteLine($"{MST[indOfLastVertex].GetNumber() + 1} {MST.Last().GetNumber() + 1}, вес: {minWeight}");
                }
            }
        }
    }
}
