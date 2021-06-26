using Newtonsoft.Json;
using NutritionApp.ViewModel.Models;
using NutritionApp.ViewModel.Repositories;
using NutritionApp.ViewModel.Repositories.Base;
using System;
using System.Collections.Generic;
using System.IO;

namespace NutritionApp.ViewModel.Classes
{
    public class StatsCalculator
    {
        // Private attributes
        private int nextId = 1;
        private List<FoodInfo> foodInfoList;
        private List<NutrientGainedAndRecommended> nutrients;

        // Public
        public List<FoodElement> foodHistory;

        // Constructor
        public StatsCalculator()
        {
            foodHistory = new List<FoodElement>();
            initializeFoodRepository();
            initializeNutrition();
        }

        // Public actions
        public bool checkFood(string foodName)
        {
            return foodInfoList.Find(f => f.Name == foodName) != null;
        }
        public double getAmountInGrams(string foodName, string amount)
        {
            if (amount.IndexOf('g') != -1)
            {
                string[] amountSplit = amount.Split('g');
                return Double.Parse(amountSplit[0]);
            }
            if (amount.IndexOf('p') != -1)
            {
                string[] amountSplit = amount.Split('p');
                int portions = int.Parse(amountSplit[0]);
                return foodInfoList.Find(f => f.Name == foodName).PortionWeight * portions;
            }

            return -1;
        }
        public void generateStats()
        {
            // Reset for new calculation
            double caloriesTotal = 0;
            double weightTotal = 0;
            foreach (NutrientGainedAndRecommended nutrient in nutrients)
            {
                nutrient.AmountGained = 0;
            }

            // Calculate stats
            foreach (FoodElement foodElement in foodHistory)
            {
                FoodInfo info = foodInfoList.Find(f => f.Name == foodElement.Name);
                double amount = foodElement.Amount / 100;
                caloriesTotal += amount * info.Calories;
                weightTotal += foodElement.Amount;
                foreach (Nutrient nutrientElement in info.Nutrition)
                {
                    NutrientGainedAndRecommended curNutrientStat = nutrients.Find(n => n.Name == nutrientElement.Name);
                    if (nutrientElement.Unit != curNutrientStat.Unit) { continue; }
                    curNutrientStat.AmountGained += amount * nutrientElement.Amount;
                }

            }
            nutrients.Find(n => n.Name == "Calories").AmountGained = caloriesTotal;
        }
        public List<NutrientGainedAndRecommended> getStats()
        {
            List<NutrientGainedAndRecommended> res = new List<NutrientGainedAndRecommended>();
            foreach (NutrientGainedAndRecommended nutrient in nutrients)
            {
                res.Add(nutrient);
            }
            return res;
        }
        public void AddFoodToHistory(FoodElement food)
        {
            food.Id = nextId;
            foodHistory.Add(food);
            nextId++;
        }
        public void RemoveFoodFromHistory(FoodElement food)
        {
            foodHistory.RemoveAll(cur => cur.Id == food.Id);
        }

        // Private helpers
        private void initializeFoodRepository()
        {
            foodInfoList = new FoodRepo().Find();
        }
        private void initializeNutrition()
        {
            nutrients = new List<NutrientGainedAndRecommended>();
            List<Nutrient> recommendedNutrients = new List<Nutrient>();
            foreach (Nutrient recomNutrientAmount in recommendedNutrients)
            {
                nutrients.Add(new NutrientGainedAndRecommended()
                {
                    Name = recomNutrientAmount.Name,
                    AmountGained = 0,
                    AmountRecommended = recomNutrientAmount.Amount,
                    Unit = recomNutrientAmount.Unit
                });
            }
        }
    }
}
