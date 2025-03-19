using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<Instruction> Instructions { get; set; } = new List<Instruction>();// Krok za krokem postup
        //public List<Allergen> Allergens { get; set; } // Alergeny obsažené v receptu
        private HashSet<Allergen> _allergens = new();
        private DishType _dishType;
        public Recipe(){ }

        public Recipe(string name)
        {
            Name = name;
        }
        public Recipe(string name, List<Ingredient> ingredients, List<Instruction> instructions, DishType dishType, HashSet<Allergen> allergens) : this(name) //send _name toother constructor
        {
            Ingredients = ingredients;
            Instructions = instructions;
            this._dishType = dishType;
            Allergens = allergens;
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
            foreach (Ingredient ingredient in Ingredients)
            {
                s += $"    └──{count}. {ingredient.GetPrintableString()}\n";
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
                s += $"    └──{count}. {allergen}\n";
                count++;
            }

            s += "\n└──[Instruction:] \n";
            count = 1;
            foreach (Instruction instruction in Instructions)
            {
                s += $"   └──{count}. {instruction.GetPrintableString()}\n";
                count++;
            }
            s += $"\n└──[Overall duration:] {GetDuration()} min\n";

            return s;
        }
    }
}
