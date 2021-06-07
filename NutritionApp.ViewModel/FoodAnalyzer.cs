using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace NutritionApp.ViewModel
{
    public class FoodAnalyzer
    {
        public FoodAnalyzer(string foodRepositoryPath, string recommendedNutritionPath)
        {
            foodHistory = new List<FoodElement>();
            importReposity(foodRepositoryPath);
            initializeNutrition(recommendedNutritionPath);
        }
        
        public Dictionary<string,Food> FoodRepository;
        public List<FoodElement> foodHistory;
        public Dictionary<string,Tuple<string,string>> Nutrition;

        private void importReposity(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            FoodRepository = JsonConvert.DeserializeObject<Dictionary<string, Food>>(jsonString);
        }

        public bool checkFood(string foodName)
        {
            return FoodRepository.ContainsKey(foodName);
        }

        public double getAmountInGrams(string foodName,string amount)
        {
            if (amount.IndexOf('g') != -1) {
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

        private void initializeNutrition(string filePath)
        {
            Nutrition = new Dictionary<string, Tuple<string, string>>();
            string nutritionString = File.ReadAllText(filePath);
            string[] nutritionElements = nutritionString.Split('\n');
            Nutrition.Add("Weight", new Tuple<string, string>("0", "n/a"));
            foreach (string el in nutritionElements)
            {
                string[] splitValues = el.Split('|');
                Nutrition.Add(splitValues[0], new Tuple<string,string>("0",splitValues[1]));
            }
        }

        private Tuple<double,string> getNutrientAmount(string s)
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
            return new Tuple<double, string>( Double.Parse(amount), measuringUnit );
        }

        public void generateStats()
        {
            double caloriesTotal = 0;
            double weightTotal = 0;
            foreach(FoodElement f in foodHistory)
            {
                Food thisNutrition = FoodRepository[f.Name];
                caloriesTotal += (f.Amount / 100) * thisNutrition.Calories;
                weightTotal += f.Amount;
                foreach(KeyValuePair<string,string> nameAndAmount in thisNutrition.Nutrition)
                {
                    Tuple<double, string> thisAmountAndMeasure = getNutrientAmount(nameAndAmount.Value);
                    Tuple<double, string> recommendedAmountAndMeasure = getNutrientAmount(this.Nutrition[nameAndAmount.Key].Item2);
                    if (thisAmountAndMeasure.Item2 != recommendedAmountAndMeasure.Item2) continue;
                    double thisAmount = f.Amount / 100;
                    double current = thisAmount * thisAmountAndMeasure.Item1 + Double.Parse(this.Nutrition[nameAndAmount.Key].Item1);
                    Tuple<string, string> thisTuple = new Tuple<string, string>(current.ToString(), Nutrition[nameAndAmount.Key].Item2);
                    Nutrition[nameAndAmount.Key] = thisTuple;
                }

            }
            Nutrition["Calories"] = new Tuple<string, string>(caloriesTotal.ToString(), Nutrition["Calories"].Item2);
            Nutrition["Weight"] = new Tuple<string, string>(weightTotal.ToString(), Nutrition["Weight"].Item2);

        }

        public string generateStatString()
        {
            string res = "";
            string format = "{0}: {1}/{2}\n"; 
            foreach(KeyValuePair<string,Tuple<string,string>> el in Nutrition)
            {
                res += String.Format(format, el.Key, Math.Round(Double.Parse(el.Value.Item1),2), el.Value.Item2);
            }

            return res;
        }
    }
}
