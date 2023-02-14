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
            Stack<Vertex> Way = new Stack<Vertex>();

            int componentCount = 0;
            for (int i = 0; i < Vertexes.Count; i++)
                if (Vertexes[i].isTaken == false)
                {
                    componentCount++;
                    Console.Write($"{componentCount} компонента связности: ");
                    DFS(Vertexes, i, Way);
                    Console.WriteLine();
                }
        }

        public static void DFS(List<Vertex> Vertexes, int i, Stack<Vertex> Way)
        {
            Way.Push(Vertexes[i]);
            Vertexes[i].isTaken = true;
            foreach (var vertex in Vertexes[i].Vertexes)
            {
                if (vertex.isTaken != true)
                {
                    DFS(Vertexes, vertex.GetNumber() - 1, Way);
                }
            }
            Console.Write(Way.Pop().GetNumber());
        }
    }
}
