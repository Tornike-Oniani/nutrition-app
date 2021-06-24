using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using NutritionApp.ViewModel.Models;

namespace NutritionApp.ViewModel.Repositories.Base
{
    class FoodRepo : BaseRepo
    {
        public void Save(FoodInfo food)
        {
            using (LiteDatabase db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<FoodInfo>("food");
                collection.Insert(food);
            }
        }
        public List<FoodInfo> Find()
        {
            using (LiteDatabase db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<FoodInfo>("food");
                return collection.Query().ToList();
            }
        }
        public void Plan()
        {
            using (LiteDatabase db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<NutrientElement>("recommended");
                collection.Insert(new NutrientElement() { Nutrient = "Calories", Amount = 1000 });
                collection.Insert(new NutrientElement() { Nutrient = "Lycopene", Amount = 90, Unit = "mg" });
                collection.Insert(new NutrientElement() { Nutrient = "VitaminC", Amount = 50, Unit = "g" });
                collection.Insert(new NutrientElement() { Nutrient = "Fiber", Amount = 30, Unit= "mg" });
                collection.Insert(new NutrientElement() { Nutrient = "Selenium", Amount = 40, Unit = "mg" });
                }
        }
        public List<NutrientElement> GetPlan()
        {
            using (LiteDatabase db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<NutrientElement>("recommended");
                return collection.Query().ToList();
            }
        }
    }
}
