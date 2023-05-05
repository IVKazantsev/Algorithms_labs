using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _6Kruskal
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0; int j = 0; int k = 0;
            // Чтение Файла //////////////////////////////////////////
            StreamReader file = new StreamReader("Matrix.txt");
            List<string> linesOfFile = new List<string>();
            List<Edge> Edges = new List<Edge>();
            while (file.Peek() > -1)
                linesOfFile.Add(file.ReadLine());
            file.Close();
            // Создание списка рёбер /////////////////////////////////
            foreach (var line in linesOfFile)
            {
                for (k = i; k < linesOfFile.Count; k++) // Идём с i-го, чтобы не читать нижнюю часть матрицы
                    if (line[k] != '0')
                    {
                        Edge edge = new Edge(i, k, (int)line[k] - 48);
                        Edges.Add(edge);
                    }
                i++;
            }
            int countOfVertexes = i;
            linesOfFile.Clear();
            // Сортировка вставками //////////////////////////////////
            Edge edgeForSort = new Edge(0, 0, 0);
            for (i = 0; i < Edges.Count; i++)
            {
                j = i - 1;
                edgeForSort = Edges[i];
                while ((j >= 0) && (Edges[j].GetWeight() > edgeForSort.GetWeight()))
                {
                    Edges[j + 1] = Edges[j];
                    j -= 1;
                }
                Edges[j + 1] = edgeForSort;
            }
            // Алгоритм Краскала /////////////////////////////
            List<Edge> MST = new List<Edge>(); // MST - минимальное остовное дерево
            List<List<int>> Components = new List<List<int>>();
            bool flag = false;
            // Создаем список компонент свзности
            for (i = 0; i < countOfVertexes; i++)
            {
                List<int> Vertexes = new List<int>();
                Vertexes.Add(i);
                Components.Add(Vertexes);
            }
            k = 0;
            for (i = 0; i < Edges.Count; i++)
            {
                foreach (var vertexes in Components) // Цикл на проверку того, чтобы в компоненту связности не входили обе вершины
                { // т.к. если обе вершины входят в компоненту связности, то образуется цикл
                    if (vertexes.Contains(Edges[i].GetVertexes()[0]) && vertexes.Contains(Edges[i].GetVertexes()[1]))
                    {
                        flag = true;
                        break;
                    }
                    else if (vertexes == Components.Last())
                        flag = false;
                }
                if (flag)
                    continue;
                MST.Add(Edges[i]);
                int first = 0; int second = 0; j = 0;
                for (j = 0; j < Components.Count; j++) // Находим вершины, которые связывает текущее ребро
                {
                    if ((Components[j].Contains(MST.Last().GetVertexes()[0]) || Components[j].Contains(MST.Last().GetVertexes()[1])) && k == 0)
                    {
                        first = j;
                        k = 1;
                    }
                    else if ((Components[j].Contains(MST.Last().GetVertexes()[0]) || Components[j].Contains(MST.Last().GetVertexes()[1])) && k != 0)
                    {
                        second = j;
                        k = 0;
                    }
                }
                Components[first] = Components[first].Concat(Components[second]).ToList();
                Components.RemoveAt(second);
            }
            Console.WriteLine("Список рёбер:");
            foreach (var edge in MST)
            {
                Console.Write("Ребро: ");
                foreach (var vertex in edge.GetVertexes())
                {
                    Console.Write($"{vertex + 1} ");
                }
                Console.WriteLine("Вес: " + edge.GetWeight());
            }
        }
    }
}
