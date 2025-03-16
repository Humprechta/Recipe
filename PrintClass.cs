using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Recipe
{
    public class PrintClass

    {
        /// <summary>
        /// PrintClass deli
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
                Print("{" + line + "}. ", false);
                Console.ForegroundColor = color;
                Print(item.GetPrintableString());
                Console.ResetColor();
                line++;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">Text</param>
        /// <param name="nextLine">true = adding \n</param>
        /// <param name="marker">Color of []</param>
        /// <param name="err">Color of {}</param>
        public void Print(string message = "",bool nextLine = true, ConsoleColor marker = ConsoleColor.Yellow, ConsoleColor err = ConsoleColor.Red)
        {
            var pieces = Regex.Split(message, @"(\{[^}]*\}|\[[^\]]*\])");

            foreach (var piece in pieces)
            {
                if (piece.StartsWith("{") && piece.EndsWith("}"))
                {
                    Console.ForegroundColor = err;
                    Console.Write(piece.Substring(1, piece.Length - 2));
                }
                else if (piece.StartsWith("[") && piece.EndsWith("]"))
                {
                    Console.ForegroundColor = marker;
                    Console.Write(piece.Substring(1, piece.Length - 2));
                }
                else
                {
                    Console.ResetColor();
                    Console.Write(piece);
                }
            }
            if (nextLine)
            {
                Console.WriteLine();
            }
        }
        public void pr(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(s);
            Console.ResetColor();
        }
    }
}
