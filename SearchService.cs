using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe
{
    public class SearchService
    {
        private PrintService Print = new();
        public void SearchRecipe(List<Recipe> recipes)
        {
            //HandleMenu(menuSearch);

            List<string> searchIngredients = new List<string>();
            DishType? dishType = null;
            HashSet<Allergen> excludedAllergens = new HashSet<Allergen>();

            while (true)
            {
                Print.deli();
                Print.Print("|[Recipe Search Menu]|");
                Print.deli();
                Print.Print("{0}. Back");
                Print.Print("{1}. Filter by Ingredients");
                Print.Print("{2}. Filter by Dish Type");
                Print.Print("{3}. Filter by Excluded Allergens");
                Print.Print("{4}. Apply Filters and Search");
                Print.Print("{5}. Reset filter");

                Print.Print("\n[Current Filters:]");
                Print.Print($"  └──Ingredients: [{string.Join("], [", searchIngredients)}]");
                Print.Print($"  └──Dish Type: [{dishType}]");
                Print.Print($"  └──Excluded Allergens: [{string.Join("], [", excludedAllergens)}]");

                Print.Print("\nEnter your choice: ", false);
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        return;
                    case "1":
                        Print.Print("Enter ingredient(s) (comma-separated): ", false);
                        string ingredientsInput = Console.ReadLine();

                        if (string.IsNullOrEmpty(ingredientsInput))
                        {
                            // if empty nothing to add
                            break;
                        }

                        var newIngredients = ingredientsInput.Split(',').Select(s => s.Trim()).ToList();
                        searchIngredients.AddRange(newIngredients);
                        break;
                    case "2":
                        Print.Print("Select Dish Type:");
                        Print.Print("{0}. Back");
                        Print.Print("{1}. Main");
                        Print.Print("{2}. Dessert");
                        Console.Write("Enter your choice: ");
                        string dishTypeChoice = Console.ReadLine();
                        switch (dishTypeChoice)
                        {
                            case "0":
                                return;
                            case "1":
                                dishType = DishType.Main;
                                break;
                            case "2":
                                dishType = DishType.Dessert;
                                break;
                            default:
                                dishType = null;
                                break;
                        }
                        break;
                    case "3":
                        Print.Print("Select Excluded Allergens (comma-separated):");
                        Print.Print("{1}. Gluten");
                        Print.Print("{2}. Nuts");
                        Print.Print("{3}. Dairy");
                        Print.Print("{4}. Eggs");
                        Print.Print("{5}. Soy");
                        Print.Print("{6}. Seafood");
                        Console.Write("Enter your choice: ");
                        string allergensInput = Console.ReadLine();

                        var selectedAllergens = allergensInput.Split(',').Select(s => s.Trim()).Select(a =>
                        {
                            switch (a)
                            {
                                case "1":
                                    return Allergen.Gluten;
                                case "2":
                                    return Allergen.Nuts;
                                case "3":
                                    return Allergen.Dairy;
                                case "4":
                                    return Allergen.Eggs;
                                case "5":
                                    return Allergen.Soy;
                                case "6":
                                    return Allergen.Seafood;
                                default:
                                    return (Allergen?)null;
                            }
                        }).Where(a => a.HasValue).Select(a => a.Value); // Filtrujeme null hodnoty a získáme Allergen

                        foreach (var allergen in selectedAllergens)
                        {
                            excludedAllergens.Add(allergen);
                        }
                        break;
                    case "4":
                        List<Recipe> results = SearchRecipes(recipes, searchIngredients, dishType, excludedAllergens);
                        /*Print.deli();
                        Print.Print("|  [Search Results]  |");
                        Print.deli();*/
                        if (results.Count == 0)
                        {
                            Print.Print(" [- noting found -]");
                        }
                        else
                        {
                            Print.ListRecipes(results);
                        }
                        break;
                    case "5":
                        searchIngredients.Clear();
                        dishType = null;
                        excludedAllergens.Clear();
                        Print.Print("[Filter successfully reset]", true, ConsoleColor.Green);
                        break;
                    default:
                        Print.Print("Invalid choice.");
                        break;
                }
            } 
        }
        private List<Recipe> SearchRecipes(List<Recipe> recipes, List<string> searchIngredients, DishType? dishType, HashSet<Allergen> excludedAllergens)
        {
            return recipes.Where(r =>
                (searchIngredients.Count == 0 || r.Ingredients.Any(i => searchIngredients.Contains(i.Name))) &&
                (!dishType.HasValue || r.DishType == dishType) &&
                (!excludedAllergens.Any() || !r.Allergens.Overlaps(excludedAllergens))
            ).ToList();
        }
    }
}
