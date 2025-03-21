﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Recipe
{
    public class Recipes
    {
        public List<Recipe> recipes { get; set; } = new();
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
