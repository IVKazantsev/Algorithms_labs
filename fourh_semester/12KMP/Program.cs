using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace _12KMP
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader file = new StreamReader("file.txt");
            string str = file.ReadLine();
            Console.WriteLine("Какую подстроку будем искать?\n");
            string pattern = Console.ReadLine();
            int result = KMP(str, pattern);
            if (result != -1) Console.WriteLine("Номер начала подстроки в строке: {0}", result);
        }
        public static int[] prefix_func(string str) // на метсе каждого символа стоит число (максимальная длина совпадающего префикса с суфиксом)
        {
            int[] pi = new int[str.Length];
            int j = 0; int i = 1;
            while (i < str.Length)
                if (str[i] == str[j])
                {
                    pi[i] = j + 1;
                    i++; j++;
                }
                else if (j == 0)
                {
                    pi[i] = 0;
                    i++;
                }
                else j = pi[j - 1];
            return pi;
        }

        public static int KMP(string source, string pattern)
        {
            if (pattern.Length > source.Length)
            {
                Console.WriteLine("Исходная строка меньше вашей!");
                return -1;
            }
            int[] pi = prefix_func(pattern);
            
            int j = 0; int i = 0;
            while (i < source.Length)
            {
                if (source[i] == pattern[j]) // при совпадении текущих символов просто идем вправо
                { j++; i++; }
                else if (j == 0) i++;       // при несовпадении, если до этого не было совпадений, то идем вправо ТОЛЬКО в строке
                else j = pi[j - 1]; // source[i] != pattern[j] && j != 0        при несовпадении текущих, если до этого были совпадения, перемещаемся в образе на соответствующий сдвиг

                if (j == pattern.Length)
                {
                    Console.WriteLine("\nОбраз найден!");
                    return i - j;
                }
            }
            Console.WriteLine("\nДанного образа нет!");
            return -1;
        }
    }
}
