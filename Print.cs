using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe
{
    public class Print
    {
        /// <summary>
        /// Console.WriteLine(s)
        /// </summary>
        /// <param name="s"></param>
        public void pl(string s = null)
        {
            Console.WriteLine(s);
        }
        /// <summary>
        /// Console.Write(s)
        /// </summary>
        /// <param name="s"></param>
        public void p(string s)
        {
            Console.Write(s);
        }
        /// <summary>
        /// Print deli
        /// </summary>
        public void deli()
        {
            Console.WriteLine("--------------------");
        }
        public void PrintList<T>(List<T> list, ConsoleColor color = ConsoleColor.White) where T : IPrintable
        {
            int line = 1;
            foreach (var item in list)
            {
                pr($"{line}. ");
                Console.ForegroundColor = color;
                pl(item.GetPrintableString());
                Console.ResetColor();
                line++;
            }
        }
        public void prl(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s);
            Console.ResetColor();
        }
        public void pr(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(s);
            Console.ResetColor();
        }
        public void PrintLnColor(string s, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(s);
            Console.ResetColor();
        }
        public void PrintColor(string s, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(s);
            Console.ResetColor();
        }
    }
}
