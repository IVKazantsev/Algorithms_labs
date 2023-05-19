using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17Boxes
{
    class Program
    {
        static void Main(string[] args)
        {
            int boxWeight = 10;
            int[] weight = { 5, 7, 3, 9, 6, 8, 1, 4, 2, 5 };
            //int[] weight = {2, 6, 5, 2, 8, 3, 2, 2 };

            List<int> boxes = new List<int>();
            int count = weight.Length;

            // BF (Best Fit) алгоритм. Кладем предмет туда, где получится наименьший остаток.
            if (boxes.Count() == 0)
                boxes.Add(weight[0]);
            for (int i = 1; i < count; i++)
            {
                int minWeight = int.MaxValue;
                int minInd = 0;
                for (int j = 0; j < boxes.Count(); j++)
                    if (weight[i] > (boxWeight - boxes[j]))
                        continue;
                    else if (boxWeight - (boxes[j] + weight[i]) < minWeight)
                    {
                        minWeight = boxWeight - (boxes[j] + weight[i]);
                        minInd = j;
                    }
                if (minWeight == int.MaxValue)
                {
                    boxes.Add(weight[i]);
                    Console.WriteLine($"Положили в {boxes.Count()} ящик {i} предмет.");
                }
                else
                {
                    boxes[minInd] += weight[i];
                    Console.WriteLine($"Положили в {minInd + 1} ящик {i} предмет.");
                }
            }

            Console.WriteLine("\nИмеем заполненности ящиков:");
            for (int i = 0; i < boxes.Count(); i++)
                Console.WriteLine($"{i + 1} ящик заполнен на {boxes[i]} из {boxWeight}");
        }
    }
}
