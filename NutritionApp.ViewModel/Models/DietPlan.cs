using System;
using System.Collections.Generic;
using System.Text;

namespace NutritionApp.ViewModel.Models
{
    public class DietPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Nutrient> RecommendedNutrients { get; set; }
    }
}
