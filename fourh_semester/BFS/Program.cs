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
                        Vertexes.ElementAt<Vertex>(i).AddVertex(Vertexes.ElementAt<Vertex>(k));
                i++;
            }
            linesOfFile.Clear();

            WayFromOneToAll(1, Vertexes);
        }

        public static void WayFromOneToAll(int vertexIndex, List<Vertex> Vertexes)
        {
            Queue<Vertex> Way = new Queue<Vertex>();
            Vertex[] ViewedVertexes = new Vertex[Vertexes.Count];
            int[] WayCount = new int[Vertexes.Count];
            Way.Enqueue(Vertexes.ElementAt<Vertex>(vertexIndex - 1));
            ViewedVertexes[vertexIndex - 1] = Vertexes.ElementAt<Vertex>(vertexIndex - 1);
            WayCount[vertexIndex - 1] = 0;
            while (Way.Count != 0)
            {
                Vertex tempVertex = Way.Dequeue();
                foreach (var vertex in tempVertex.Vertexes)
                    if (!ViewedVertexes.Contains(vertex))
                    {
                        Way.Enqueue(vertex);
                        ViewedVertexes[vertex.GetNumber() - 1] = vertex;
                        WayCount[vertex.GetNumber() - 1] = WayCount[tempVertex.GetNumber() - 1] + 1;
                        Console.WriteLine($"Путь от {vertexIndex} до {vertex.GetNumber()} - {WayCount[vertex.GetNumber() - 1]}");
                    }
            }
            for (int i = 0; i < ViewedVertexes.Length; i++)
            {
                if(ViewedVertexes[i] == null)
                    Console.WriteLine($"До вершины {i + 1} пути нет");
            }
        }
    }
}
