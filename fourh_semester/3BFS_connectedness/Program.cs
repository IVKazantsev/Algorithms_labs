using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BFS
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
            // Создание вершин ///////////////////////////////////////
            int i = 1;
            foreach (var line in linesOfFile)
            {
                Vertex vertex = new Vertex(i);
                Vertexes.Add(vertex);
                i++;
            }
            // Связывание вершин, у которых есть путь друг с другом //
            i = 0;
            foreach (var line in linesOfFile)
            {
                for (int k = 0; k < linesOfFile.Count; k++)
                    if (line[k] != '0')
                    {
                        Vertexes.ElementAt<Vertex>(i).AddVertex(Vertexes.ElementAt<Vertex>(k));
                        Vertexes.ElementAt<Vertex>(k).AddVertex(Vertexes.ElementAt<Vertex>(i)); // Нам неважно, сильная связь или нет
                    }
                i++;
            }
            linesOfFile.Clear();

            Component(Vertexes);
        }

        public static void Component(List<Vertex> Vertexes)
        {
            Queue<Vertex> Way = new Queue<Vertex>();

            int componentCount = 0;
            for (int i = 0; i < Vertexes.Count; i++)
                if(Vertexes[i].isTaken == false)
                {
                    componentCount++;
                    Console.Write($"{componentCount} компонента связности: ");
                    Vertexes[i].isTaken = true;
                    Way.Enqueue(Vertexes[i]);
                    Vertex tempVertex = Vertexes[i];
                    while(Way.Count() != 0)
                    {
                        foreach (var vertex in tempVertex.Vertexes)
                            if (Vertexes[vertex.GetNumber() - 1].isTaken != true)
                            {
                                Way.Enqueue(vertex);
                                Vertexes[vertex.GetNumber() - 1].isTaken = true;
                            }
                        tempVertex = Way.Dequeue();
                        Console.Write(tempVertex.GetNumber());
                    }
                    Console.WriteLine();
                }
        }
    }
}
