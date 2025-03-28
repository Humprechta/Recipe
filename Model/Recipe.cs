﻿using System.Text.Json.Serialization;

namespace Recipe
{

    // Enum for alergens
    public enum Allergen
    {
        Gluten,
        Dairy,
        Nuts,
        Eggs,
        Soy,
        Seafood
    }
    public enum DishType
    {
        Main, 
        Dessert
    }
    public class Recipe : IPrintable
    {
        private string _name;
        [JsonInclude] //becouse private set ... otherwise deserialization will return empty...
        public List<Ingredient> Ingredients { get; private set; } = new();
        //this can be better - private set
        [JsonInclude]
        public List<Instruction> Instructions { get; private set; } = new();// Krok za krokem postup
        //public List<Allergen> Allergens { get; set; } // Alergeny obsažené v receptu
        private HashSet<Allergen> _allergens = new();
        private DishType _dishType;

        public List<Ingredient> getIngredients()
        {
            return Ingredients;
        }

        public Recipe(){ } //for serialization

        public Recipe(string name)
        {
            Name = name;
        }

        public Recipe(string name, List<Ingredient> ingredients, List<Instruction> instructions, DishType dishType, HashSet<Allergen> allergens) : this(name) //send _name toother constructor
        {
            this.Ingredients = ingredients;
            this.Instructions = instructions;
            this._dishType = dishType;
            Allergens = allergens;
        }

        public void AddInstructions(List<Instruction> instruction)
        {
            this.Instructions.AddRange(instruction);
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CustomException("Name cannot be empty");
                }
                _name = value;
            }
        }

        public DishType DishType
        {
            get { return _dishType; }
            set { _dishType = value; }
            /*set
            {
                if (!Enum.IsDefined(typeof(DishType), value))
                {
                    throw new CustomException("Zadali jste špatnou hodnotu pro _dishType");
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

        public HashSet<Allergen> Allergens
        {
            get { return _allergens; }
            set { _allergens = value ?? new HashSet<Allergen>(); }
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
        /*private string getAlergens()
        {
            if(Allergens.Count == 0)
            {
                return " └── - No _allergens -";
            }
            foreach (Ingredient ingredient in Ingredients)

            return $"Alergens: {string.Join(", ", Allergens)}";
        }*/
        public string GetPrintableString()
        {
            string s = $"[{Name}] ({_dishType})\n";
            s += "\n└──[Ingredietns:]\n";
            int count = 1;
            foreach (Ingredient ingredient in getIngredients())
            {
                s += $"    └──{{{count}}}. {ingredient.GetPrintableString()}\n";
                count++;
            }

            s += "\n└──[Alergens:]\n";
            count = 1;
            if(Allergens.Count == 0)
            {
                s += "    └── - No allergens -";
            }
            foreach (Allergen allergen in Allergens)
            {
                s += $"    └──{{{count}}}. {allergen}\n";
                count++;
            }

            s += "\n└──[Instruction:] \n";
            count = 1;
            foreach (Instruction instruction in Instructions)
            {
                s += $"   └──{{{count}}}. {instruction.GetPrintableString()}\n";
                count++;
            }
            s += $"\n└──[Overall duration:] {GetDuration()} min\n";

            return s;
        }
    }
}
