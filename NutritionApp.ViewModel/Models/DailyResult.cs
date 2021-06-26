using System;
using System.Collections.Generic;
using System.Text;

namespace NutritionApp.ViewModel.Models
{
    public class DailyResult
    {
        public DateTime Day { get; set; }
        public List<Nutrient> NutrientsGained { get; set; }
        public DietPlan DietPlan { get; set; }
        public string DayDisplay { get { return Day.ToShortDateString(); } }
    }
}
