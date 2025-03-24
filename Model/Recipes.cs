using System.Text.Json.Serialization;

namespace Recipe
{
    public class Recipes
    {
        public List<Recipe> recipes { get; private set; } = new();
        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }
        public void AddRecipe(List<Recipe> recipes)
        {
            this.recipes.AddRange(recipes);
        }
        public List<Recipe> GetRecipes()
        {
            return recipes;
        }
        public Recipes()
        {
            /*List<Ingredient> scrambledEggsIngredients =
            [
                new Ingredient("Eggs", 2, Unit.Piece),
                new Ingredient("Milk", 2, Unit.Tablespoon),
                new Ingredient("Salt", 0.5, Unit.Teaspoon),
                new Ingredient("Pepper", 1, Unit.Teaspoon),
                new Ingredient("Butter", 1, Unit.Tablespoon)
            ];

            List<Instruction> scrambledEggsInstructions =
            [
                new Instruction("Crack eggs into a bowl, add milk, salt, and pepper, and mix well.", 2),
                new Instruction("Heat butter in a pan over medium heat.", 1),
                new Instruction("Pour egg mixture into the pan and stir until eggs are set.", 5),
                new Instruction("Serve immediately.", 1)
            ];

            recipes.Add(
                new Recipe(
                    "Scrambled Eggs",
                    scrambledEggsIngredients,
                    scrambledEggsInstructions,
                    DishType.Main,
                    new HashSet<Allergen>() { Allergen.Dairy, Allergen.Eggs }
                ));*/
        }
    }
}
