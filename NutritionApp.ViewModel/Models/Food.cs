using System;
using System.Collections.Generic;
using System.Text;

namespace NutritionApp.ViewModel.Models
{
    public class Food
    {
        public double Calories { get; set; }
        public double PortionWeight { get; set; }
        public Dictionary<string, string> Nutrition { get; set; }
    }
}
