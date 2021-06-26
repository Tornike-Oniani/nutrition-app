using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using NutritionApp.ViewModel.Models;
using NutritionApp.ViewModel.Repositories.Base;

namespace NutritionApp.ViewModel.Repositories
{
    class DietPlanRepo : BaseRepo
    {
        public void Save(DietPlan dietPlan)
        {
            using (LiteDatabase db = new LiteDatabase(connectionString))
            {
                ILiteCollection<DietPlan> collection = db.GetCollection<DietPlan>("diet_plan");
                collection.Insert(dietPlan);
            }
        }
        public List<DietPlan> Find()
        {
            using (LiteDatabase db = new LiteDatabase(connectionString))
            {
                ILiteCollection<DietPlan> collection = db.GetCollection<DietPlan>("food");
                return collection.Query().ToList();
            }
        }

        public DietPlan FindByName(string name)
        {
            using (LiteDatabase db = new LiteDatabase(connectionString))
            {
                ILiteCollection<DietPlan> collection = db.GetCollection<DietPlan>("food");
                return collection.FindOne(dp => dp.Name == name);
            }
        }
    }
}
