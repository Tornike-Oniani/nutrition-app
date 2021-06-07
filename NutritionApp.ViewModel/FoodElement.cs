using System;
using System.Collections.Generic;
using System.Text;

namespace NutritionApp.ViewModel
{
    public class FoodElement
    {
        public string Name { get; set; }
        public double Amount { get; set; }

        public override string ToString()
        {
            return Name + ',' + Amount;
        }
    }
}
