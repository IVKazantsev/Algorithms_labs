using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace _10EulerianPath
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
            int i = 0;
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
                        Vertexes.ElementAt<Vertex>(k).AddVertex(Vertexes.ElementAt<Vertex>(i));
                    }
                i++;
            }
            linesOfFile.Clear();
            if (checkForEulerPath(Vertexes))
                findEulerPath(Vertexes);
        }
        public static void DFS(List<Vertex> Vertexes, int i, bool[] visited)
        {
            visited[i] = true;
            foreach (var vertex in Vertexes[i].ConnectedVertexes)
                if (visited[vertex.GetNumber()] != true)
                    DFS(Vertexes, vertex.GetNumber(), visited);
        }

        public static bool checkForEulerPath(List<Vertex> Vertexes)
        {
            int OddVertex = 0; // Кол-во вершин с нечетным количеством присоединённых
            foreach (var vertex in Vertexes)
                if (vertex.ConnectedVertexes.Count % 2 == 1)
                    OddVertex++;
            if (OddVertex > 2)
            {
                Console.WriteLine("Не эйлеров граф");
                return false;
            }
                bool[] visited = new bool[Vertexes.Count];
            for (int i = 0; i < visited.Count(); i++)
                visited[i] = false;
            foreach (var vertex in Vertexes)
                if (vertex.ConnectedVertexes.Count > 0)
                {
                    DFS(Vertexes, vertex.GetNumber(), visited);
                    break;
                }
            foreach (var vertex in Vertexes)
                if ((vertex.ConnectedVertexes.Count > 0) && (!visited[vertex.GetNumber()]))
                    return false;
            if (OddVertex == 0)
                Console.WriteLine("Эйлеров граф");
            else
                Console.WriteLine("Полуэйлеров граф");
            return true;
        }


        public static void findEulerPath(List<Vertex> Vertexes)
        {
            Vertex v = new Vertex(-1);
            foreach (var vertex in Vertexes)
                if (vertex.ConnectedVertexes.Count % 2 == 1)
                {
                    v = vertex;
                    break;
                }
                else
            if (v.GetNumber() == -1)
                    v = Vertexes[0];
            Stack<Vertex> S = new Stack<Vertex>();
            List<Edge> E = new List<Edge>();
            S.Push(v);
            while (S.Count > 0)
            {
                v = S.Peek();
                if(v.ConnectedVertexes.Count == 0)
                {
                    v = S.Pop();
                    Console.Write($"{v.GetNumber()} ");
                }
                else
                {
                    Vertex u = v.ConnectedVertexes[0];
                    S.Push(u);
                    Vertexes[Vertexes.FindIndex(vertex => vertex.GetNumber() == v.GetNumber())].
                        ConnectedVertexes.RemoveAt(Vertexes[Vertexes.FindIndex(vertex => vertex.GetNumber() == v.GetNumber())].ConnectedVertexes.FindIndex(vertex => vertex.GetNumber() == u.GetNumber()));
                    Vertexes[Vertexes.FindIndex(vertex => vertex.GetNumber() == u.GetNumber())].
                        ConnectedVertexes.RemoveAt(Vertexes[Vertexes.FindIndex(vertex => vertex.GetNumber() == u.GetNumber())].ConnectedVertexes.FindIndex(vertex => vertex.GetNumber() == v.GetNumber()));
                }
            }
        }
    }
}
