using System;
using System.Collections.Generic;
using System.Text;

namespace NutritionApp.ViewModel.Models
{
    public class GainedNutrient
    {
        public string Nutrient { get; set; }
        public double AmountGained { get; set; }
        public double AmountRecommended { get; set; }
        public string Unit { get; set; }
    }
}
