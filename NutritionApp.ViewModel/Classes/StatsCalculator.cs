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
        private Dictionary<string, double> gainedNutrients;
        private DietPlan selectedPlan;
        private List<FoodElement> foodHistory;

        // Public


        // Constructor
        public StatsCalculator(DietPlan selectedPlan)
        {
            this.selectedPlan = selectedPlan;
            foodHistory = new List<FoodElement>();
            foodInfoList = new FoodRepo().Find();
        }

        // Public actions
        public bool TryAddFood(string foodName, string amount)
        {
            // Check if we have food information in API
            if (!checkFood(foodName)) { return false; }

            // Check if amount is in correct format (g or p) !!! Might want to move this validation in viewmodel 
            // because we want to only return false if we have no food information (for future notifications implementation)
            double amountInGrams = getAmountInGrams(foodName, amount);
            if (amountInGrams == -1) { return false; }

            // Add food into history (We keep track of history for delete functionality)
            foodHistory.Add(new FoodElement() { Id = nextId, Name = foodName, Amount = amountInGrams });
            nextId++;

            // Add gained nutrients
            // In Food info we keep what amount of nutrients have per 100g, thus we need to divide it by 100 to use it in calculation
            double amountForNutrientCalculations = amountInGrams / 100;
            FoodInfo info = foodInfoList.Find(f => f.Name == foodName);
            foreach (Nutrient nutrient in info.Nutrition)
            {
                if (!gainedNutrients.ContainsKey(nutrient.Name)) { gainedNutrients.Add(nutrient.Name, 0); }

                gainedNutrients[nutrient.Name] += nutrient.Amount * amountForNutrientCalculations;
            }

            return true;
        }
        public void RemoveFood(FoodElement food)
        {
            // Remove from history
            foodHistory.RemoveAll(cur => cur.Id == food.Id);

            // Subtract it's nutrients from gained nutrients
            FoodInfo info = foodInfoList.Find(f => f.Name == food.Name);
            foreach (Nutrient nutrient in info.Nutrition)
            {
                gainedNutrients[nutrient.Name] -= nutrient.Amount;
            }
        }
        
        
        public List<NutrientGainedAndRecommended> generateStats()
        {
            List<NutrientGainedAndRecommended> stats = new List<NutrientGainedAndRecommended>();

            foreach (KeyValuePair<string, double> nutrientAmount in gainedNutrients)
            {
                Nutrient recommended = selectedPlan.RecommendedNutrients.Find(n => n.Name == nutrientAmount.Key);
                stats.Add(new NutrientGainedAndRecommended()
                {
                    Name = nutrientAmount.Key,
                    AmountGained = nutrientAmount.Value,
                    AmountRecommended = recommended.Amount,
                    Unit = recommended.Unit
                });
            }

            return stats;
        }

        // Private helpers
        private bool checkFood(string foodName)
        {
            return foodInfoList.Find(f => f.Name == foodName) != null;
        }
        private double getAmountInGrams(string foodName, string amount)
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
    }
}
