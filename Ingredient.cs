// ST10355049 PROG6221 POE Liam Knipe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10355049_POEPart3_LiamKnipe
{
    public class Ingredient
    {
        // Properties
        public string Name { get; }
        public double Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; }
        public double OriginalQuantity { get; }
        public string OriginalUnitOfMeasurement { get; }
        public int OriginalCalories { get; }

        // Constructor
        public Ingredient(string name, double quantity, string unitOfMeasurement, int calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            OriginalQuantity = quantity;
            UnitOfMeasurement = unitOfMeasurement;
            OriginalUnitOfMeasurement = unitOfMeasurement;
            Calories = calories;
            OriginalCalories = calories;
            FoodGroup = foodGroup;
        }

        // Override ToString method for better representation
        public override string ToString()
        {
            return $"{Name}, {Quantity} {UnitOfMeasurement}, {Calories} calories, {FoodGroup}";
        }
    }
}

// ----- End of Ingredient.cs -----