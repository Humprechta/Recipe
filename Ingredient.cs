using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe
{
    // Enum pro jednotky
    public enum Unit
    {
        Gram,
        Milliliter,
        Piece,
        Teaspoon,
        Tablespoon,
        Cup
    }
    // Třída pro ingredience
    public class Ingredient : IPrintable
    {
        private string _name;
        private double _quantity;
        private Unit _unit;

        public Ingredient() { } //for serialization
        public Ingredient(string name)
        {
            Name = name;
        }
        public Ingredient(string name, double quantity, Unit unit)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;

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

        public double Quantity
        {
            get { return _quantity; }
            set
            {
                if (value <= 0)
                {
                    throw new CustomException("Quantity must be greater than zero");
                }
                _quantity = value;
            }
        }

        public Unit Unit
        {
            get { return _unit; }
            set
            {
                if (!Enum.IsDefined(typeof(Unit), value))
                {
                    throw new CustomException("Zadali jste špatnou hodnotu pro jednotku");
                }
                _unit = value;
            }
        }
        public string GetPrintableString()
        {
            return $"{Name} {Quantity} {Unit}";
        }
    }
}
