using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using NutritionApp.ViewModel.Models;
using NutritionApp.ViewModel.Repositories.Base;

namespace NutritionApp.ViewModel.Repositories
{
    class FoodRepo : BaseRepo
    {
        public void Save(FoodInfo food)
        {
            using (LiteDatabase db = new LiteDatabase(connectionString))
            {
                ILiteCollection<FoodInfo> collection = db.GetCollection<FoodInfo>("food");
                collection.Insert(food);
            }
        }
        public List<FoodInfo> Find()
        {
            using (LiteDatabase db = new LiteDatabase(connectionString))
            {
                ILiteCollection<FoodInfo> collection = db.GetCollection<FoodInfo>("food");
                return collection.Query().ToList();
            }
        }
    }
}
