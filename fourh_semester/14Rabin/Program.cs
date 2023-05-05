using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace _14Rabin
{
    class Program
    {
        static int b = 13;  // основание степени (чем больше, тем меньше коллизий)
        static int q = 256; // кол-во символов в аски
        static void Main(string[] args)
        {
            StreamReader file = new StreamReader("file.txt");
            string str = file.ReadLine();
            Console.WriteLine("Какую подстроку будем искать?\n");
            string pattern = Console.ReadLine();

            Rabin(str, pattern);
        }
        static int hashFunc(string str) // полиномиальный хеш
        {
            int result = 0;
            for (int i = 0; i < str.Length; i++) // для каждого символа по методу Хорнера считаем хеш || c0*b^2 + c1 * b^1 + c2*b^0 = c2 + b * (c1 + b * (c0))
                result = (b * result + (int)str[i]) % q; // деление модулю на q, чтобы не было переполнения (но увеличивается кол-во коллизий)
            return result;
        }
        static void Rabin(string str, string pattern)
        {
            int patternLen = pattern.Length;
            int strLen = str.Length;

            int patternHash = hashFunc(pattern);
            bool isFound = false;
            int count = 0;
            for (int i = 0; i < strLen - patternLen + 1; i++)
            {
                int substrHash = hashFunc(str.Substring(i, patternLen));
                if ((patternHash == substrHash) && (str.Substring(i, patternLen) == pattern)) 
                {
                    if (isFound == false)
                        Console.WriteLine("\nОбраз найден!");
                    isFound = true;
                    Console.WriteLine("Номер начала подстроки в строке: {0}.", i);
                    count += 1;
                }
            }
            if(isFound == false)
                Console.WriteLine("\nДанного образа нет!");
        }
    }
}
