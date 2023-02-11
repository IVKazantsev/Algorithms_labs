using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1ConvexHull
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] points = ArrayReading();
            ArrayOutput(points);
            Jarvis(points);
        }

        public static void Jarvis(int[,] points)
        {
            int lowerPoint = LowerPoint(points);

            int currentPoint = lowerPoint, comparePoint = int.MinValue;
            List<int> Hull = new List<int>();
            Hull.Add(lowerPoint);
            while (comparePoint != lowerPoint)
            {
                double ang = double.MaxValue;
                for (int i = 0; i < points.GetLength(0); i++)
                {
                    double currentAng = angle0(points[i, 0], points[i, 1], points[currentPoint, 0], points[currentPoint, 1]);
                    if (Hull.Count > 1)
                        currentAng = angle(points[i, 0], points[i, 1], points[currentPoint, 0], points[currentPoint, 1], points[Hull[Hull.Count - 2], 0], points[Hull[Hull.Count - 2], 1]);
                    if (ang > currentAng && currentAng != 0)
                    {
                        ang = currentAng;
                        comparePoint = i;
                    }
                    else if (ang == currentAng)
                        if (Math.Sqrt(Math.Pow((points[i, 0] - points[currentPoint, 0]), 2) + Math.Pow((points[i, 1] - points[currentPoint, 1]), 2)) > Math.Sqrt(Math.Pow((points[comparePoint, 0] - points[currentPoint, 0]), 2) + Math.Pow((points[comparePoint, 1] - points[currentPoint, 1]), 2)))
                            comparePoint = i;
                }
                if (comparePoint != int.MinValue)
                    currentPoint = comparePoint;
                bool flag = true;
                foreach (int point in Hull)
                    if (currentPoint == point)
                        flag = false;
                if (flag)
                    Hull.Add(currentPoint);
            }

            Console.Write("Оболочка: ");
            foreach (int point in Hull)
                Console.Write($"({points[point, 0]}, {points[point, 1]})");
            Console.ReadKey();
        }

        public static int[,] ArrayReading()
        {
            Console.Write("Введите кол-во точек: ");
            int pointsCount = Convert.ToInt32(Console.ReadLine());
            int[,] points = new int[pointsCount, 2];
            for (int i = 0; i < pointsCount; i++)
            {
                Console.WriteLine($"Введите координаты {i + 1} точки:");
                Console.Write("x = ");
                points[i, 0] = Convert.ToInt32(Console.ReadLine());
                Console.Write("y = ");
                points[i, 1] = Convert.ToInt32(Console.ReadLine());
            }
            return points;
        }

        public static void ArrayOutput(int[,] points)
        {
            Console.Write("Массив точек:");
            for (int i = 0; i < points.GetLength(0); i++)
            {
                Console.Write($"({points[i, 0]}, {points[i, 1]});");
            }
            Console.WriteLine();
        }

        public static int LowerPoint(int[,] points)
        {
            int pointsLen = points.GetLength(0);
            int maxY = int.MinValue;
            int maxX = int.MinValue;
            int maxPoint = 0;

            // Ищу максимальный Y
            for (int i = 0; i < pointsLen; i++)
                if (points[i, 1] > maxY)
                    maxY = points[i, 1];

            // Ищу точку с максимальным Y и максимальным X (нижнюю правую точку)
            for (int i = 0; i < pointsLen; i++)
                if ((points[i, 1] == maxY) && (points[i, 0] > maxX))
                {
                    maxPoint = i;
                    maxX = points[i, 0];
                }

            Console.WriteLine($"Нижняя правая точка: ({points[maxPoint, 0]},{points[maxPoint, 1]})");
            return maxPoint;
        }

        public static double angle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            x3 = x2 + (x2 - x3);
            y3 = y2 + (y2 - y3);
            double a = Math.Sqrt(Math.Pow(x1 - x2,2)+Math.Pow(y1-  y2,2));
            double b = Math.Sqrt(Math.Pow(x2 - x3, 2) + Math.Pow(y2 - y3, 2));
            double c = Math.Sqrt(Math.Pow(x1 - x3, 2) + Math.Pow(y1 - y3, 2));
            double k = Math.Acos((Math.Pow(a,2)+ Math.Pow(b, 2) - Math.Pow(c, 2))/(2*a*b)) * 180 / Math.PI;
            if (k < 0) k += 360;
            return k;
        }

        public static double angle0(int x1, int y1, int x2, int y2)
        {
            double opposite = 0, adjacent = 0, k = 0;
            adjacent = x1 - x2;
            opposite = y2 - y1;
            k = Math.Atan2(opposite,adjacent)*180/Math.PI;
            if (k < 0) k += 360;
            return k;
        }
    }
}