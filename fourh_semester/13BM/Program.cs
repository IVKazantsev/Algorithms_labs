using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace _13BM
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader file = new StreamReader("file.txt");
            string str = file.ReadLine();
            Console.WriteLine("Какую подстроку будем искать?\n");
            string pattern = Console.ReadLine();

            BoyerMoore(str, pattern);
        }

        static int[] arrayFormation(string pattern) // формируем вспомогательный массив (на сколько следует провести сдвиг)
        {
            int[] d = new int[pattern.Length];
            for (int i = pattern.Length - 2; i >= 0; i--)
                if (!pattern.Substring(i + 1, pattern.Length - (i + 2)).Contains(pattern[i]))
                    d[i] = pattern.Length - i - 1;
                else
                    d[i] = d[pattern.Substring(i).IndexOf(pattern[i]) + i + 1];

            if (!pattern.Substring(0, pattern.Length - 2).Contains(pattern[pattern.Length - 1]))
                d[d.Length - 1] = pattern.Length;
            else
                d[d.Length - 1] = d[pattern.Substring(0, pattern.Length - 2).IndexOf(pattern[pattern.Length - 1])];

            return d;
        }

        static void BoyerMoore(string str, string pattern)
        {
            if (pattern.Length > str.Length)
            {
                Console.WriteLine("Исходная строка меньше вашей!");
                return;
            }
            int i = 0;
            int j = 1;
            bool isFound = false;
            int[] d = arrayFormation(pattern);
            while (i + pattern.Length - 1 < str.Length) // пока не сдвинули образ за границы строки или не нашли строку
            {
                if (str[i + pattern.Length - 1] != pattern[pattern.Length - 1]) // Если последние символы не совпали, то
                    if (!pattern.Contains(str[i + pattern.Length - 1]))         // при отстутсвии символа из строки в алфавите, смещаемся на длину образа
                        i += pattern.Length;
                    else
                        i += d[pattern.IndexOf(str[i + pattern.Length - 1])];   // при присутствии символа строки в алфавите, смещаемся на значение, находящееся в доп массиве
                else                                                            // при совпадении последнего символа, идем по подстроке (справа налево) и сравниваем
                {
                    j++;
                    if (str[i + pattern.Length - j] == pattern[pattern.Length - j]) // при совпадении след символа, идем дальше (справа налево) по подстроке
                    {
                        if (j == pattern.Length)
                        {
                            if (!isFound)
                                Console.WriteLine("\nОбраз найден!");
                            Console.WriteLine("Номер начала подстроки в строке: {0}.", i);
                            isFound = true;
                            j = 1;
                            i += pattern.Length;
                        }
                        continue;
                    }
                    else                                                           // при несовпадении символа, сдвигаемся на значение последнего символа в доп массиве
                    {
                        i += d[d.Length - 1];
                        j = 1;
                    }
                }
            }
            if(isFound == false)
                Console.WriteLine("\nДанного образа нет!");
        }
    }
}
