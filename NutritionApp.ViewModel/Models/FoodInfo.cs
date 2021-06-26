using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace NutritionApp.ViewModel.Models
{
    public class FoodInfo : IEquatable<FoodInfo>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public double PortionWeight { get; set; }
        public List<Nutrient> Nutrition { get; set; }

        public bool Equals([AllowNull] FoodInfo other)
        {
            return Name == other.Name;
        }
    }
}
