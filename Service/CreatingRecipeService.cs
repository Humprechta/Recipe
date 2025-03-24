namespace Recipe
{
    public static class CreatingRecipeService
    {
        public static PrintService Print = new();
        public static Recipe createRecipe()
        {
            Print.deli();
            Print.Print("│ [Creating recipe]  │");
            Print.deli();
            Recipe recipe;
            DishType dishType;
            while (true)
            {
                try
                {
                    Print.Print("Name of recipe: ");

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
                    Print.Print("Type of dish (Main, Dessert): ");
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
                    Print.Print("[Ingredient saved]", true, ConsoleColor.Green);
                }
            }

            // Allergens
            Print.Print("Any allergens?? (y/n): ", false);
            if (Console.ReadLine().ToLower() == "y")
            {
                while (true)
                {
                    HashSet<Allergen> allergens = AddAllergens();
                    Print.Print("Recap: ", false);
                    Print.Print($"[{string.Join("], [", allergens)}]");
                    Print.Print("OK? y/n:", false);
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        recipe.Allergens = allergens;
                        break;
                    }
                }
            }

            List<Instruction> listInstructions = createInstructions();
            recipe.AddInstructions(listInstructions);
            return recipe;
        }
        private static HashSet<Allergen> AddAllergens()
        {
            HashSet<Allergen> allergens = new();
            while (true)
            {
                Print.Print("Allergen (Gluten, Dairy, Nuts, Eggs, Soy, Seafood) separated by ,");
                string allergenInput = Console.ReadLine();
                try
                {
                    if (Enum.TryParse(allergenInput.Trim(), out Allergen allergen))
                    {
                        allergens.Add(allergen);
                    }
                    else
                    {
                        throw new CustomException("Invalid alergen.");
                    }
                }
                catch (CustomException e)
                {
                    Print.Print("{" + e.Message + "}");
                }
                return allergens;
            }
        }
        private static Ingredient? CreateIngredient()
        {
            Ingredient ingredient = null;
            while (true)//_name
            {
                try
                {
                    Print.Print("Name of ingredient: ");
                    string ingredientName = Console.ReadLine();
                    ingredient = new Ingredient(ingredientName);
                    break;
                }
                catch (CustomException e)
                {
                    Print.Print("{" + e.Message + "}");
                }
            }
            while (true)//_quantity
            {
                try
                {
                    Print.Print("Quantity / weight: ");
                    if (!double.TryParse(Console.ReadLine(), out double quantity))
                    {
                        throw new CustomException("Only number - for decimal use ,");
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
                    Print.Print("Unit (Gram, Milliliter, Piece, Teaspoon, Tablespoon, Cup): ");
                    if (!Enum.TryParse(Console.ReadLine(), out Unit unit))
                    {
                        throw new CustomException("Invalid unit");
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
        private static List<Instruction> createInstructions()
        {
            List<Instruction> instructions = new List<Instruction>();

            while (true) //creating instruction
            {
                Print.Print("Add instruction (y/n): ", false);
                if (Console.ReadLine().ToLower() != "y")
                {
                    Print.Print("Recap: ");
                    Print.PrintList(instructions, ConsoleColor.Yellow);
                    Print.Print("Save? y/n:", false);
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
                while (true)//_quantity
                {
                    try
                    {
                        Print.Print("Duration (min)");
                        if (!int.TryParse(Console.ReadLine(), out int duration))
                        {
                            throw new CustomException("Write only number (not decimal)");
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
    }
}
