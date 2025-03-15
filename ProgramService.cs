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
        
        private Print _p = new Print();
        private Recipes Recipes = new Recipes();
        public Dictionary<string, Action> mainMenuOptions = new Dictionary<string, Action>();
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
            _p.pl("Vítejte v aplikaci Recipe by Václav Taitl as Assignment for Object-Oriented Programming class - XAMK v1.0");
            while (true) //program loop
            {
                HandleMenu(mainMenuOptions);
            }
        } 
        private Ingredient? CreateIngredient()
        {
            string ingredientName = "";
            double quantity = 0;
            Unit unit = Unit.Gram; // Výchozí hodnota
            DishType dishType = DishType.Main; // Výchozí hodnota
            List<Allergen> allergens = new List<Allergen>();
            Ingredient ingredient = null;
            while (true)//name
            {
                try
                {
                    _p.pl("Název ingredience: ");
                    ingredientName = Console.ReadLine();
                    ingredient = new Ingredient(ingredientName);
                    break;
                }
                catch (CustomException e)
                {
                    _p.prl(e.Message);
                }
            }
            while (true)//quantity
            {
                try
                {
                    _p.pl("Množství / váha: ");
                    if (!double.TryParse(Console.ReadLine(), out quantity))
                    {
                        throw new CustomException("Zadejte prosí, číslo");
                    }
                    ingredient.Quantity = quantity;
                    break;
                }
                catch (CustomException e)
                {
                    _p.prl(e.Message);
                }
            }
            // Jednotka
            while (true)
            {
                try
                {
                    _p.pl("Jednotka (Gram, Milliliter, Piece, Teaspoon, Tablespoon, Cup): ");
                    if (!Enum.TryParse(Console.ReadLine(), out unit))
                    {
                        throw new CustomException("Neplatná jednotka.");
                    }
                    ingredient.Unit = unit;
                    break;
                }
                catch (CustomException e)
                {
                    _p.prl(e.Message);
                }
            }
            // Alergeny
            _p.p("Obsahuje alergeny? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                _p.pl("Add alergens (0 for exit):");
                while (true)
                {
                    _p.pl("Alergen (Gluten, Dairy, Nuts, Eggs, Soy, Seafood) nebo 0 pro ukončení: ");
                    string allergenInput = Console.ReadLine();
                    if (allergenInput == "0")
                    {
                        break;
                    }
                    try
                    {
                        if (Enum.TryParse(allergenInput.Trim(), out Allergen allergen))
                        {
                            allergens.Add(allergen);
                        }
                        else
                        {
                            throw new CustomException("Neplatný alergen.");
                        }
                    }
                    catch (CustomException e)
                    {
                        _p.prl(e.Message);
                    }
                }
            }
            ingredient.Allergens = allergens;
            _p.p("Recap: ");
            _p.PrintLnColor(ingredient.GetPrintableString(), ConsoleColor.Yellow);
            _p.p("Save? y/n:");
            if (Console.ReadLine().ToLower() == "y")
            {
                return ingredient;
            }
            return null;
        }
        public void AddRecipe()
        {
            _p.deli();
            _p.pl("│ Creating recipe  │");
            _p.deli();
            _p.pl("Zadejte název receptu: ");
            string name = Console.ReadLine();
            DishType dishType = DishType.Main; // Výchozí hodnota
            Recipe recipe = new Recipe(name);
            // Typ jídla
            while (true)
            {
                try
                {
                    _p.pl("Typ jídla (Main, Dessert): ");
                    if (Enum.TryParse(Console.ReadLine(), out dishType))
                    {
                        recipe.DishType = dishType;
                        break; // Ukončí cyklus pouze pokud se převod podaří
                    }
                    else
                    {
                        throw new CustomException("Neplatný typ jídla.");
                    }
                }
                catch (CustomException e)
                {
                    _p.pl(e.Message);
                }
            }
            while (true)
            {
                _p.p("Add ingredient (y/n): ");
                if (Console.ReadLine().ToLower() != "y") break;
                Ingredient ingredient = CreateIngredient();
                if (ingredient != null)
                {
                    recipe.AddIngredient(ingredient);
                    _p.PrintLnColor("Ingredient saved.", ConsoleColor.Green);
                } 
            }
            List<Instruction> listInstructions = createInstructions();
            recipe.Instructions = listInstructions;
            _p.PrintLnColor("Recipe saved.", ConsoleColor.Green);
            _p.deli();
            Recipes.recipes.Add(recipe);
        }
        private List<Instruction> createInstructions()
        {
            List<Instruction> instructions = new List<Instruction>();

            while (true) //creating instruction
            {
                _p.deli();
                _p.p("Add instruction (y/n): ");
                _p.deli();
                if (Console.ReadLine().ToLower() != "y") {
                    _p.pl("Recap: ");
                    _p.PrintList(instructions, ConsoleColor.Yellow);
                    _p.p("Save? y/n:");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        break; //return
                    }
                    instructions = new List<Instruction>(); //reset
                };
                Instruction instruction = new Instruction();
                while (true)//instruction text
                {
                    try
                    {
                        _p.pl("Postup: "); //1.2...,
                        string step = Console.ReadLine();
                        instruction.Step = step;
                        break;
                    }
                    catch (CustomException e)
                    {
                        _p.prl(e.Message);
                    }
                }
                while (true)//quantity
                {
                    try
                    {
                        _p.pl("Duration (min)");
                        if (!int.TryParse(Console.ReadLine(), out int duration))
                        {
                            throw new CustomException("Zadejte prosí, číslo");
                        }
                        instruction.Duration = duration;
                        break;
                    }
                    catch (CustomException e)
                    {
                        _p.prl(e.Message);
                    }
                }
                instructions.Add(instruction);
            }
            return instructions;
        }
        public void SearchRecipe()
        {

        }
        public void ListAllRecipes()
        {
            _p.deli();
            _p.pl("│Listing all recipes in system...│");
            _p.deli();
            _p.PrintList(Recipes.recipes);
        }
        public void ExitProgram()
        {
            Environment.Exit(0);
        }
        public void HandleMenu(Dictionary<string, Action> menuOpt)
        {
            _p.deli();
            _p.pl("│       Menu       │");
            _p.deli();
            int index = 1;
            foreach (var option in menuOpt.Keys)
            {
                _p.pr($"{index}. ");
                _p.pl($"{option}");
                index++;
            }

            _p.p("Choose action: ");
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int optionIndex) && optionIndex > 0 && optionIndex <= menuOpt.Count)
            {
                string selectedOption = new List<string>(menuOpt.Keys)[optionIndex - 1];
                _p.pl($"You choose: {selectedOption}");
                
                menuOpt[selectedOption].Invoke(); // Zavolá odpovídající metodu
            }
            else
            {
                _p.pl("Neplatná volba, zkuste to znovu.");
            }
        }
    }
}
