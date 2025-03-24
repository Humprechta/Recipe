using System.Text.RegularExpressions;


namespace Recipe
{
    public class PrintService

    {
        /// <summary>
        /// PrintService deli
        /// </summary>
        public void deli()
        {
            Print("--------------------");
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
        /// Print message to user with colors - accepting [] and {}
        /// </summary>
        /// <param _name="message">Text</param>
        /// <param _name="nextLine">true = adding \n</param>
        /// <param _name="marker">Color of []</param>
        /// <param _name="err">Color of {}</param>
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
        public void ListRecipes(List<Recipe> recipes)
        {
            while (true)
            {
                deli();
                Print("│ [Select a Recipe]  │");
                deli();
                Print("{0}. Go Back");
                for (int i = 0; i < recipes.Count; i++)
                {
                    Print($"{{{i + 1}}}. {recipes[i].Name}");
                }
                if(recipes.Count > 1) {
                    Print($"{{{recipes.Count + 1}}}. Show All Recipes");
                }
                Print("Enter your choice: ", false);
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        break; // Go back
                    }
                    if (choice == recipes.Count + 1)
                    {
                        deli();
                        Print("│[Listing all recipes]│");
                        deli();
                        PrintList(recipes);  // Show all recipes
                    }
                    if (choice > 0 && choice <= recipes.Count)
                    {
                        Print($"U choose: [{recipes[choice - 1].Name}]");
                        deli();
                        Print(recipes[choice - 1].GetPrintableString());
                    }
                }
            }
        }
    }
}
