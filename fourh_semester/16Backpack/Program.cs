using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16Backpack
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] weight = { 3, 4, 5, 8, 9 }; // веса предметов
            int[] price = { 1, 6, 4, 7, 6 }; // стоимости предметов
            int count = weight.Length; // количество предметов
            int backpackWeight = 13; // размер рюкзака
            int[,] optimalCost = new int[count + 1, backpackWeight + 1]; // таблица с оптимальными весами

            // Реализация метода динамического программирования
            for (int k = 0; k <= count; k++) // k - текущее кол-во вложенных предметов
                for (int s = 0; s <= backpackWeight; s++) // текущий вес рюкзака
                {
                    if (k == 0 || s == 0) optimalCost[k, s] = 0; // если кол-во предметов или размер рюкзака = 0, в таблице ставим стоимость 0
                    else
                    {
                        if (weight[k - 1] > s) optimalCost[k, s] = optimalCost[k - 1, s]; // если предмет больше вместимости рюкзака, то оставляем всё как есть
                        else optimalCost[k, s] = Math.Max(optimalCost[k - 1, s], optimalCost[k - 1, s - weight[k - 1]] + price[k - 1]); // кладем в таблицу максимум, если положить и если не положить текущий предмет
                    }
                }

            Console.WriteLine("Оптимальная стоимость: {0}", optimalCost[count, backpackWeight]);
            Console.Write("Положили предметы: ");
            // Извлечение предметов, которые попали в рюкзак
            int sum = backpackWeight;
            for (int k = count; k > 0; k--)
            {
                if (optimalCost[k, sum] == optimalCost[k - 1, sum]) continue;
                else
                {
                    Console.Write(k + " ");
                    sum -= weight[k - 1];
                }
            }
            Console.WriteLine();

        }
    }
}
