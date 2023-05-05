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
        static void Main(string[] args) // Косарайю
        {
            // Чтение Файла //////////////////////////////////////////
            StreamReader file = new StreamReader("Matrix.txt");
            List<string> linesOfFile = new List<string>();
            List<Vertex> Vertexes = new List<Vertex>();
            List<Vertex> VertexesInvert = new List<Vertex>(); // Инвертированные пути до вершин
            while (file.Peek() > -1)
                linesOfFile.Add(file.ReadLine());
            file.Close();
            // Создание вершин ///////////////////////////////////////
            int i = 0;
            foreach (var line in linesOfFile)
            {
                Vertex vertex = new Vertex(i);
                Vertex vertexInvert = new Vertex(i);
                Vertexes.Add(vertex);
                VertexesInvert.Add(vertexInvert);
                i++;
            }
            // Связывание вершин, у которых есть путь друг с другом //
            i = 0;
            foreach (var line in linesOfFile)
            {
                for (int k = 0; k < linesOfFile.Count; k++)
                    if (line[k] != '0')
                    {
                        Vertexes[i].AddVertex(Vertexes[k]);
                        VertexesInvert[k].AddVertex(VertexesInvert[i]); // Список с инвертированными путями
                    }
                i++;
            }
            linesOfFile.Clear();

            int[] Dfs = new int[Vertexes.Count];
            for (int j = 0; j < Vertexes.Count; j++)
            {
                Dfs = DFS(Vertexes, j);
                for (int p = 0; p < Dfs.Length; p++)
                    if (Dfs[p] != -1)
                        Vertexes[j].AddVertex(Vertexes[p]);
            }

            for (int j = 0; j < VertexesInvert.Count; j++)
            {
                Dfs = DFS(VertexesInvert, j);
                for (int p = 0; p < Dfs.Length; p++)
                    if (Dfs[p] != -1)
                        VertexesInvert[j].AddVertex(VertexesInvert[p]);
            }

            bool[] visited = new bool[Vertexes.Count];
            for (int j = 0; j < Vertexes.Count; j++)
            {
                if (visited[j] == true)
                    continue;
                Console.Write("Сильная компонента связности: ");
                for (int k = 0; k < Vertexes[j].Vertexes.Count(); k++)
                    for (int l = 0; l < VertexesInvert[j].Vertexes.Count; l++)
                        if (Vertexes[j].Vertexes[k].GetNumber() == VertexesInvert[j].Vertexes[l].GetNumber())
                        {
                            visited[Vertexes[j].Vertexes[k].GetNumber()] = true;
                            Console.Write(Vertexes[j].Vertexes[k].GetNumber() + 1 + " ");
                        }
                Console.WriteLine();
            }
        }

        public static int[] DFS(List<Vertex> Vertexes, int i)
        {
            int[] depth = new int[Vertexes.Count];
            bool[] visited = new bool[Vertexes.Count];
            for (int j = 0; j < Vertexes.Count; depth[j++] = -1) ;

            Stack<Vertex> stack = new Stack<Vertex>();
            Vertex vertex = Vertexes[i];
            stack.Push(vertex);

            depth[vertex.GetNumber()] = 0;
            while (stack.Count != 0)
            {
                vertex = stack.Pop();
                visited[vertex.GetNumber()] = true;
                for (int j = 0; j < vertex.Vertexes.Count; j++)
                    if (!visited[vertex.Vertexes[j].GetNumber()])
                    {
                        stack.Push(vertex.Vertexes[j]);
                        depth[vertex.Vertexes[j].GetNumber()] = depth[vertex.GetNumber()] + 1;
                    }
            }
            return depth;
        }
    }
}
