using NutritionApp.ViewModel.Models;
using NutritionApp.ViewModel.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NutritionApp.ViewModel.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        // Private attributes

        // Public properties
        public ObservableCollection<DailyResult> DailyResults { get; set; }

        // Commands

        // Constructor
        public DashboardViewModel()
        {
            DailyResults = new ObservableCollection<DailyResult>()
            {
                new DailyResult() { Day = DateTime.Now },
                new DailyResult() { Day = DateTime.Now },
                new DailyResult() { Day = DateTime.Now },
                new DailyResult() { Day = DateTime.Now }
            };
        }

        // Command actions

        // Private helpers
    }
}
