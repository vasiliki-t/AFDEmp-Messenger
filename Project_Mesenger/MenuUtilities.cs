using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Mesenger
{
    static class MenuUtilities
    {
        public delegate void Header();

        public static int SelectOperations(string[] currentList, Header header = null)
        {
            int currentIndex = 0;
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.ResetColor();

               if(header != null)
                {
                    header();
                }


                for (int i = 0; i < currentList.Length; i++)
                {
                    string preface = $"{ (i + 1).ToString()}.";

                    if (i == currentIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        preface = ">";
                    }
                    else
                    {
                        Console.ResetColor();
                    }

                    Console.WriteLine($"{preface} {currentList[i]}");
                }

                ConsoleKey k = Console.ReadKey().Key;

                if (k == ConsoleKey.DownArrow)
                {
                    currentIndex++;
                    if (currentIndex > currentList.Length - 1)
                    {
                        currentIndex = 0;
                    }
                }
                else if (k == ConsoleKey.UpArrow)
                {
                    currentIndex--;
                    if (currentIndex < 0)
                    {
                        currentIndex = currentList.Length - 1;
                    }
                }
                else if (k == ConsoleKey.Enter)
                {
                    exit = true;
                }
            }
            
            return currentIndex;
        }

           
        public static string MaskPassword(Header header = null)
        {
            string result = "";
            List<string> pass = new List<string>();
            List<string> mask = new List<string>();
            bool isWriting = true;
           
            while (isWriting)
            {
                Console.Clear();
                if(header != null)
                {
                   header();
                }

                string m = "";
                for (int i = 0; i < mask.Count; i++)
                {
                    m += mask[i];
                }

                Console.WriteLine(m);
               
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    isWriting = false;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (pass.Count > 0)
                    {
                        pass.RemoveAt(pass.Count - 1);
                        mask.RemoveAt(mask.Count - 1);
                    }
                }
                else
                {
                    pass.Add(key.KeyChar.ToString());
                    mask.Add("*");
                }

             
            }

            for (int i = 0; i < pass.Count; i++)
            {
                result += pass[i];
            }

            return result;
        }
    }
}
