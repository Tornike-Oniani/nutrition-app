using Newtonsoft.Json;
using NutritionApp.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace NutritionApp.ViewModel.Classes
{
    public class FoodAnalyzer
    {
        // Private attributes
        private int nextId = 1;
        private Dictionary<string, Food> FoodRepository;
        private Dictionary<string, Tuple<string, string>> Nutrition;
        private Dictionary<string, GainedNutrient> nutrients;

        // Public
        public List<FoodElement> foodHistory;

        // Constructor
        public FoodAnalyzer(string foodRepositoryPath, string recommendedNutritionPath)
        {
            foodHistory = new List<FoodElement>();
            importReposity(foodRepositoryPath);
            initializeNutrition(recommendedNutritionPath);
        }

        // Public actions
        public bool checkFood(string foodName)
        {
            return FoodRepository.ContainsKey(foodName);
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
                return FoodRepository[foodName].PortionWeight * portions;
            }

            return -1;
        }
        public void generateStats()
        {
            double caloriesTotal = 0;
            double weightTotal = 0;
            foreach (GainedNutrient nutrient in nutrients.Values)
            {
                nutrient.AmountGained = 0;
            }
            foreach (FoodElement f in foodHistory)
            {
                Food thisNutrition = FoodRepository[f.Name];
                caloriesTotal += f.Amount / 100 * thisNutrition.Calories;
                weightTotal += f.Amount;
                foreach (KeyValuePair<string, string> nameAndAmount in thisNutrition.Nutrition)
                {
                    Tuple<double, string> thisAmountAndMeasure = getNutrientAmount(nameAndAmount.Value);
                    Tuple<double, string> recommendedAmountAndMeasure = getNutrientAmount(this.Nutrition[nameAndAmount.Key].Item2);
                    if (thisAmountAndMeasure.Item2 != recommendedAmountAndMeasure.Item2) continue;
                    double thisAmount = f.Amount / 100;
                    double current = thisAmount * thisAmountAndMeasure.Item1 + Double.Parse(this.Nutrition[nameAndAmount.Key].Item1);
                    Tuple<string, string> thisTuple = new Tuple<string, string>(current.ToString(), Nutrition[nameAndAmount.Key].Item2);
                    Nutrition[nameAndAmount.Key] = thisTuple;
                    nutrients[nameAndAmount.Key].AmountGained = current;
                }

            }
            Nutrition["Calories"] = new Tuple<string, string>(caloriesTotal.ToString(), Nutrition["Calories"].Item2);
            Nutrition["Weight"] = new Tuple<string, string>(weightTotal.ToString(), Nutrition["Weight"].Item2);
            nutrients["Calories"].AmountGained = caloriesTotal;

        }
        public List<GainedNutrient> getStats()
        {
            List<GainedNutrient> res = new List<GainedNutrient>();

            foreach (KeyValuePair<string, GainedNutrient> entry in nutrients)
            {
                res.Add(entry.Value);
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
        private void importReposity(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            FoodRepository = JsonConvert.DeserializeObject<Dictionary<string, Food>>(jsonString);
        }
        private void initializeNutrition(string filePath)
        {
            Nutrition = new Dictionary<string, Tuple<string, string>>();
            nutrients = new Dictionary<string, GainedNutrient>();
            string nutritionString = File.ReadAllText(filePath);
            string[] nutritionElements = nutritionString.Split('\n');
            Nutrition.Add("Weight", new Tuple<string, string>("0", "n/a"));
            foreach (string el in nutritionElements)
            {
                string[] splitValues = el.Split('|');
                Nutrition.Add(splitValues[0], new Tuple<string, string>("0", splitValues[1]));
                Tuple<double, string> recAmountAndUnit = getNutrientAmount(splitValues[1]);
                nutrients.Add(splitValues[0], new GainedNutrient()
                {
                    Nutrient = splitValues[0],
                    AmountGained = 0,
                    AmountRecommended = recAmountAndUnit.Item1,
                    Unit = recAmountAndUnit.Item2
                });
            }
        }
        private Tuple<double, string> getNutrientAmount(string s)
        {
            string amount = "";
            string measuringUnit = "";
            int l = s.Length;
            int splitIndex = -1;
            char c;
            for (int i = 0; i < l; i++)
            {
                c = s[i];
                if (char.IsLetter(c))
                {
                    splitIndex = i;
                    break;
                }
                amount += c;
            }
            if (splitIndex != -1)
            {
                for (int i = splitIndex; i < l; i++)
                {
                    measuringUnit += s[i];
                }
            }

            measuringUnit = measuringUnit.Trim();
            return new Tuple<double, string>(Double.Parse(amount), measuringUnit);
        }
    }
}
