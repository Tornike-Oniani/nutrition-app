using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace NutritionApp.ViewModel.Models
{
    public class Nutrient : IEquatable<Nutrient>
    {
        public string Name { get; set; }
        public double AmountGained { get; set; }
        public double AmountRecommended { get; set; }
        public string Unit { get; set; }
        public int PercentGained { get { return (int)(AmountGained * 100 / AmountRecommended); } }

        public bool Equals([AllowNull] Nutrient other)
        {
            return Name == other.Name;
        }
    }
}
