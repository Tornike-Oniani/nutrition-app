using System;
using System.Collections.Generic;
using System.Text;

namespace NutritionApp.ViewModel.Models
{
    public class FoodElement
    {
        // Id is for removing element from list (let's say we have 2 entries of "rice 1p", if I want to remove one of them I'll do it with Id)
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
    }
}
