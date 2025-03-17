using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Recipe
{
    public class ProgramService
    {
        private PrintService Print = new ();
        private Recipes Recipes = new();
        private Dictionary<string, Action> mainMenuOptions;
        private SearchService searchService = new ();
        private FileService fileService = new ("Recipe.json");
        public ProgramService()
        {
            mainMenuOptions = new Dictionary<string, Action>
            {
                { "Přidat recept", AddRecipe },
                { "Vyhledat recept", SearchRecipe },
                { "Vypsat všechny recepty", ListAllRecipes },
                { "Konec", ExitProgram }
            };
        }

        public void start()
        {
            Print.Print("Welcome in Recipe app by Václav Taitl as Assignment for Object-Oriented Programming class - XAMK v1.0");
            Print.Print("Choose a file with name Recipes -> [Recipe.json], if file not exist, create new.");
            fileService.SelectFile();
            Recipes.recipes = fileService.LoadRecipes();
            while (true) //program loop
            {
                try
                {
                    HandleMenu(mainMenuOptions);
                } 
                catch(Exception e)
                {
                    Print.Print("{Err occured: " + e.Message + "}");
                }
                
            }
        } 
        public void AddRecipe()
        {
            Recipe recipe = CreatingRecipeService.createRecipe();
            Recipes.recipes.Add(recipe);
            fileService.SaveRecipes(Recipes.GetRecipes());
            Print.Print("[Recipe saved]", true, ConsoleColor.Green);
        }

        private void SearchRecipe()
        {
            searchService.SearchRecipe(Recipes.GetRecipes());
        }

        public void ListAllRecipes()
        {
            Print.ListRecipes(Recipes.GetRecipes());
        }
        public void ExitProgram()
        {
            fileService.SaveRecipes(Recipes.GetRecipes());
            Environment.Exit(0);
        }
        public void HandleMenu(Dictionary<string, Action> menuOpt)
        {
            Print.deli();
            Print.Print($"│       [Menu]       │");
            Print.deli();
            int index = 1;
            foreach (var option in menuOpt.Keys)
            {
                Print.Print("{" + index + "}. ", false);
                Print.Print($"{option}");
                index++;
            }

            Print.Print("Choose action: ", false);
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int optionIndex) && optionIndex > 0 && optionIndex <= menuOpt.Count)
            {
                string selectedOption = new List<string>(menuOpt.Keys)[optionIndex - 1];
                Print.Print($"You choose: [{selectedOption}]");
                
                menuOpt[selectedOption].Invoke(); // Zavolá odpovídající metodu
            }
            else
            {
                Print.Print("{Invalid number, try again}");
            }
        }
    }
}
