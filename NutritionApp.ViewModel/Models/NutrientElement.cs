using System;
using System.Collections.Generic;
using System.Text;

namespace NutritionApp.ViewModel.Models
{
    public class NutrientElement
    {
        public int Id { get; set; }
        public string Nutrient { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
    }
}
