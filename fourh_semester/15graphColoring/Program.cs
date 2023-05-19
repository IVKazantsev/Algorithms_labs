using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace _15graphColoring
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader file = new StreamReader("Matrix.txt");
            string line = file.ReadLine();
            int matrixLen = line.Length;

            int[,] matrix = new int[matrixLen, matrixLen];
            int[,] arrayOfDegrees = new int[matrixLen, 2];
            int degree = 0;
            for (int i = 0; i < matrixLen; i++)
            {
                for (int j = 0; j < matrixLen; j++)
                {
                    matrix[i, j] = line[j] - 48;
                    if (matrix[i, j] == 1)
                        degree++;
                }
                line = file.ReadLine();
                arrayOfDegrees[i, 0] = i;
                arrayOfDegrees[i, 1] = degree;
                degree = 0;
            }
            file.Close();

            int[] colors = new int[matrixLen];
            int color = 0;

            int max = 0;
            int maxInd = 0;
            int maxVert = 0;
            for (int i = 0; i < matrixLen; i++)
            {
                max = 0;
                maxInd = 0;
                for (int j = i; j < matrixLen; j++)
                    if (max < arrayOfDegrees[j, 1])
                    {
                        max = arrayOfDegrees[j, 1];
                        maxInd = j;
                        maxVert = arrayOfDegrees[j, 0];
                    }
                arrayOfDegrees[maxInd, 1] = arrayOfDegrees[i, 1];
                arrayOfDegrees[maxInd, 0] = arrayOfDegrees[i, 0];
                arrayOfDegrees[i, 1] = max;
                arrayOfDegrees[i, 0] = maxVert;
            }

            int tempInd = 0;
            int tempVert = arrayOfDegrees[tempInd, 0];
            while (colors.Contains(0))
            {
                color++;
                colors[tempVert] = color;
                for (int i = tempInd; i < matrixLen; i++)
                {
                    bool isColored = false;
                    if (colors[arrayOfDegrees[i, 0]] > 0)
                        continue;
                    for (int j = 0; j < matrixLen; j++)
                    {
                        if (matrix[arrayOfDegrees[i, 0], j] != 0)
                            if (colors[j] >= color)
                            {
                                isColored = true;
                                break;
                            }
                    }
                    if (isColored == false)
                        colors[arrayOfDegrees[i, 0]] = color;
                }
                for (int i = 0; i < matrixLen; i++)
                {
                    if (colors[arrayOfDegrees[i, 0]] == 0)
                    {
                        tempInd = i;
                        break;
                    }
                }
                tempVert = arrayOfDegrees[tempInd, 0];
            }

            for (int i = 0; i < colors.Length; i++)
            {
                switch(colors[i])
                {
                    case 1:
                        Console.WriteLine($"Вершина {(char)(i + 97)} синяя");
                        break;
                    case 2:
                        Console.WriteLine($"Вершина {(char)(i + 97)} зеленая");
                        break;
                    case 3:
                        Console.WriteLine($"Вершина {(char)(i + 97)} красная");
                        break;
                }
            }
        }
    }
}
