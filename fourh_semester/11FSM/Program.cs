using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace _11FSM
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader file = new StreamReader("file.txt");
            string str = file.ReadLine();
            Console.WriteLine("Какую строку будем искать?\n");
            string pattern = Console.ReadLine();
            Dictionary<char, int[]> automat = new Dictionary<char, int[]>();

            foreach (var character in pattern)
            {
                int[] temp = new int[pattern.Length];
                for (int i = 0; i < pattern.Length; i++)
                {
                    if (pattern[0] == character) // Если попавшийся символ = первому символу в образе, то везде записываем 1
                        temp[i] = 1;
                    if (pattern[i] == character) // Если попавшийся символ - не первый, то везде, кроме места данного символа записываем 0
                        temp[i] = i + 1;
                }
                if (!automat.ContainsKey(character)) automat.Add(character, temp);
            }
            foreach (var character in str)
            {
                int[] temp = new int[pattern.Length];
                if (!automat.ContainsKey(character)) automat.Add(character, temp);
            }

            int state = 0;
            bool isFound = false;
            for (int i = 0; i < str.Length; i++)
            {
                state = automat[str[i]][state];
                if (state == pattern.Length)
                {
                    if (!isFound)
                        Console.WriteLine("\nОбраз найден!");
                    Console.WriteLine("Номер начала подстроки в строке: {0}.", i - pattern.Length + 1);
                    state = 0;
                    isFound = true;
                    continue;
                }
                
            }
            if(isFound == false)   Console.WriteLine("\nДанного образа нет!");
        }
    }
}
