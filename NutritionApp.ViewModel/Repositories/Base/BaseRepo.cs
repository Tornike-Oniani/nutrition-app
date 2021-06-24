using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LiteDB;

namespace NutritionApp.ViewModel.Repositories.Base
{
    class BaseRepo
    {
        protected string connectionString = Path.Combine(Environment.CurrentDirectory, "database.db");
    }
}
