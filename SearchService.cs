using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe
{
    public class SearchService
    {
        private PrintService _printService = new();
        public void SearchRecipe(List<Recipe> recipes)
        {
            //HandleMenu(menuSearch);

            List<string> searchIngredients = new List<string>();
            DishType? dishType = null;
            HashSet<Allergen> excludedAllergens = new HashSet<Allergen>();

            while (true)
            {
                _printService.deli();
                _printService.Print("|[Recipe Search Menu]|");
                _printService.deli();
                _printService.Print("{0}. Back");
                _printService.Print("{1}. Filter by Ingredients");
                _printService.Print("{2}. Filter by Dish Type");
                _printService.Print("{3}. Filter by Excluded Allergens");
                _printService.Print("{4}. Apply Filters and Search");
                _printService.Print("{5}. Reset filter");

                _printService.Print("\n[Current Filters:]");
                _printService.Print($"  └──Ingredients: [{string.Join("], [", searchIngredients)}]");
                _printService.Print($"  └──Dish Type: [{dishType}]");
                _printService.Print($"  └──Excluded Allergens: [{string.Join("], [", excludedAllergens)}]");

                _printService.Print("\nEnter your choice: ", false);
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        return;
                    case "1":
                        _printService.Print("Enter ingredient(s) (comma-separated): ", false);
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
                        _printService.Print("Select Dish Type:");
                        _printService.Print("{0}. Back");
                        _printService.Print("{1}. Main");
                        _printService.Print("{2}. Dessert");
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
                        _printService.Print("Select Excluded Allergens (comma-separated):");
                        _printService.Print("{1}. Gluten");
                        _printService.Print("{2}. Nuts");
                        _printService.Print("{3}. Dairy");
                        _printService.Print("{4}. Eggs");
                        _printService.Print("{5}. Soy");
                        _printService.Print("{6}. Seafood");
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
                        /*_printService.deli();
                        _printService._printService("|  [Search Results]  |");
                        _printService.deli();*/
                        if (results.Count == 0)
                        {
                            _printService.Print(" [- noting found -]");
                        }
                        else
                        {
                            _printService.ListRecipes(results);
                        }
                        break;
                    case "5":
                        searchIngredients.Clear();
                        dishType = null;
                        excludedAllergens.Clear();
                        _printService.Print("[Filter successfully reset]", true, ConsoleColor.Green);
                        break;
                    default:
                        _printService.Print("Invalid choice.");
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
