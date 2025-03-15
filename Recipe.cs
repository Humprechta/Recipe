using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe
{
    public enum DishType
    {
        Main, 
        Dessert
    }
    public class Recipe : IPrintable
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<Instruction> Instructions { get; set; } = new List<Instruction>();// Krok za krokem postup
        //public List<Allergen> Allergens { get; set; } // Alergeny obsažené v receptu
        private DishType dishType;

        public Recipe(string name)
        {
            Name = name;
        }
        public Recipe(string name, List<Ingredient> ingredients, List<Instruction> instructions, DishType dishType) : this(name)
        {
            Ingredients = ingredients;
            Instructions = instructions;
            this.dishType = dishType;
        }

        public DishType DishType
        {
            get { return dishType; }
            set { dishType = value; }
            /*set
            {
                if (!Enum.IsDefined(typeof(DishType), value))
                {
                    throw new CustomException("Zadali jste špatnou hodnotu pro dishType");
                }
                DishType = value;
            }*/
        }

        public int GetDuration()
        {
            int duration = 0;
            foreach (Instruction instruction in Instructions)
            {
                duration += instruction.Duration;
            }
            return duration;
        }

        public HashSet<Allergen> GetUniqAllergens()
        {
            HashSet<Allergen> uniqueAllergens = new HashSet<Allergen>();
            foreach (Ingredient ingredient in Ingredients)
            {
                foreach (var allergen in ingredient.Allergens)
                {
                    uniqueAllergens.Add(allergen);
                }
            }
            return uniqueAllergens;
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
           // Allergens.AddRange(ingredient.Allergens);
        }

        public void AddInstruction(Instruction instruction)
        {
            Instructions.Add(instruction);
        }

        public int GetTotalTime()
        {
            return Instructions.Sum(i => i.Duration);
        }
        public string GetPrintableString()
        {
            string s = $"{Name} ({dishType})\n";
            s += "\n└──Ingredietns:\n";
            int ingredientCount = 1;
            foreach (Ingredient ingredient in Ingredients)
            {
                s += $"    └──{ingredientCount}. {ingredient.GetPrintableString()}\n";
                ingredientCount++;
            }

            s += "\n└──Instruction:\n";
            int instructionCount = 1;
            foreach (Instruction instruction in Instructions)
            {
                s += $"   └──{instructionCount}. {instruction.GetPrintableString()}\n";
                instructionCount++;
            }
            s += $"\n└──Overall duration: {GetDuration()} min\n";

            return s;
        }
    }
}
