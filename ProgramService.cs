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
        
        private PrintClass Print = new PrintClass();
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
            Print.Print("Vítejte v aPrintikaci Recipe by Václav Taitl as Assignment for Object-Oriented Programming class - XAMK v1.0");
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
            Ingredient ingredient = null;
            while (true)//name
            {
                try
                {
                    Print.Print("Název ingredience: ");
                    ingredientName = Console.ReadLine();
                    ingredient = new Ingredient(ingredientName);
                    break;
                }
                catch (CustomException e)
                {
                    Print.Print("{" + e.Message + "}");
                }
            }
            while (true)//quantity
            {
                try
                {
                    Print.Print("Množství / váha: ");
                    if (!double.TryParse(Console.ReadLine(), out quantity))
                    {
                        throw new CustomException("Zadejte prosí, číslo");
                    }
                    ingredient.Quantity = quantity;
                    break;
                }
                catch (CustomException e)
                {
                    Print.Print("{" + e.Message + "}");
                }
            }
            // Jednotka
            while (true)
            {
                try
                {
                    Print.Print("Jednotka (Gram, Milliliter, Piece, Teaspoon, Tablespoon, Cup): ");
                    if (!Enum.TryParse(Console.ReadLine(), out unit))
                    {
                        throw new CustomException("NePrintatná jednotka.");
                    }
                    ingredient.Unit = unit;
                    break;
                }
                catch (CustomException e)
                {
                    Print.Print("{" + e.Message + "}");
                }
            }
            Print.Print("Recap: ", false);
            Print.Print("[" + ingredient.GetPrintableString() + "]");
            Print.Print("Save? y/n:", false);
            if (Console.ReadLine().ToLower() == "y")
            {
                return ingredient;
            }
            return null;
        }
        public void AddRecipe()
        {
            Print.deli();
            Print.Print("│ [Creating recipe]  │");
            Print.deli();
            HashSet<Allergen> allergens = new();
            Recipe recipe;
            DishType dishType;
            while (true)
            {
                try
                {
                    Print.Print("Zadejte název receptu: ");
                    recipe = new Recipe(Console.ReadLine());
                    break;
                }
                catch (CustomException e)
                {
                    Print.Print("{" + e.Message + "}");
                }
            }
            // Typ jídla
            while (true)
            {
                try
                {
                    Print.Print("Typ jídla (Main, Dessert): ");
                    if (Enum.TryParse(Console.ReadLine(), out dishType))
                    {
                        recipe.DishType = dishType;
                        break; // Ukončí cyklus pouze pokud se převod podaří
                    }
                    else
                    {
                        throw new CustomException("NePrintatný typ jídla.");
                    }
                }
                catch (CustomException e)
                {
                    Print.Print("{" + e.Message + "}");
                }
            }
            while (true)
            {
                Print.Print("Add ingredient (y/n): ", false);
                if (Console.ReadLine().ToLower() != "y") break;
                Ingredient ingredient = CreateIngredient();
                if (ingredient != null)
                {
                    recipe.AddIngredient(ingredient);
                    Print.Print("[Ingredient saved]", true,ConsoleColor.Green);
                } 
            }

            // Alergens
            Print.Print("Obsahuje alergeny? (y/n): ", false);
            if (Console.ReadLine().ToLower() == "y")
            {
                Print.Print("Add alergens (0 for exit):");
                while (true)
                {
                    Print.Print("Alergen (Gluten, Dairy, Nuts, Eggs, Soy, Seafood) nebo 0 pro ukončení: ");
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
                            throw new CustomException("NePrintatný alergen.");
                        }
                    }
                    catch (CustomException e)
                    {
                        Print.Print("{" + e.Message + "}");
                    }
                }
            }
            recipe.Allergens = allergens;
            List<Instruction> listInstructions = createInstructions();
            recipe.Instructions = listInstructions;
            Print.Print("[Recipe saved]", true, ConsoleColor.Green);
            Recipes.recipes.Add(recipe);
        }
        private List<Instruction> createInstructions()
        {
            List<Instruction> instructions = new List<Instruction>();

            while (true) //creating instruction
            {
                Print.Print("Add instruction (y/n): ",false);
                Print.deli();
                if (Console.ReadLine().ToLower() != "y") {
                    Print.Print("Recap: ");
                    Print.PrintList(instructions, ConsoleColor.Yellow);
                    Print.Print("Save? y/n:",false);
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
                        Print.Print("Step by step (recipe procedure): "); //1.2...,
                        string step = Console.ReadLine();
                        instruction.Step = step;
                        break;
                    }
                    catch (CustomException e)
                    {
                        Print.Print("{" + e.Message + "}");
                    }
                }
                while (true)//quantity
                {
                    try
                    {
                        Print.Print("Duration (min)");
                        if (!int.TryParse(Console.ReadLine(), out int duration))
                        {
                            throw new CustomException("Zadejte prosí, číslo");
                        }
                        instruction.Duration = duration;
                        break;
                    }
                    catch (CustomException e)
                    {
                        Print.Print("{" + e.Message + "}");
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
            Print.deli();
            Print.Print("│[Listing all recipes in system...]│");
            Print.deli();
            Print.PrintList(Recipes.recipes);
        }
        public void ExitProgram()
        {
            Environment.Exit(0);
        }
        public void HandleMenu(Dictionary<string, Action> menuOpt)
        {
            Print.deli();
            Print.Print("│       [Menu]       │");
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
                Print.Print($"You choose: {selectedOption}");
                
                menuOpt[selectedOption].Invoke(); // Zavolá odpovídající metodu
            }
            else
            {
                Print.Print("{Invalid number, try again}");
            }
        }
    }
}
