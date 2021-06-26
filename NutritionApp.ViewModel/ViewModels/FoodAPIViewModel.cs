using NutritionApp.ViewModel.Classes;
using NutritionApp.ViewModel.Models;
using NutritionApp.ViewModel.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using NutritionApp.ViewModel.Repositories;
using System.Linq;

namespace NutritionApp.ViewModel.ViewModels
{
    public class FoodAPIViewModel : BaseViewModel
    {
        // Private attributes
        private string _name;
        private double _calories;
        private double _portionWeight;
        private string _dietName;
        private bool _isNameTextboxFocused;

        // Public properties
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }
        public double Calories
        {
            get { return _calories; }
            set { _calories = value; OnPropertyChanged("Calories"); }
        }
        public double PortionWeight
        {
            get { return _portionWeight; }
            set { _portionWeight = value; OnPropertyChanged("PortionWeight"); }
        }
        public ObservableCollection<Nutrient> Nutrients { get; set; }
        public string DietName
        {
            get { return _dietName; }
            set { _dietName = value; OnPropertyChanged("DietName"); }
        }
        public ObservableCollection<Nutrient> DietNutrients { get; set; }
        public bool IsNameTextboxFocused
        {
            get { return _isNameTextboxFocused; }
            set { _isNameTextboxFocused = value; OnPropertyChanged("IsNameTextboxFocused"); }
        }

        // Commands
        public ICommand SaveFoodCommand { get; set; }
        public ICommand SaveDietPlanCommand { get; set; }

        // Constructor
        public FoodAPIViewModel()
        {
            IsNameTextboxFocused = true;
            Nutrients = new ObservableCollection<Nutrient>();
            DietNutrients = new ObservableCollection<Nutrient>();

            SaveFoodCommand = new RelayCommand(SaveFood);
            SaveDietPlanCommand = new RelayCommand(SaveDietPlan);
        }

        // Command actions
        public void SaveFood(object input = null)
        {
            if (String.IsNullOrWhiteSpace(Name) || Calories == 0 || Nutrients.Count == 0) { return; }

            // Persist to database
            new FoodRepo().Save(new FoodInfo() { Name = Name.ToLower(), Calories = Calories, PortionWeight = PortionWeight, Nutrition = Nutrients.ToList() });

            // Prepare for next entry
            Name = null;
            Calories = 0;
            PortionWeight = 0;
            Nutrients.Clear();
            IsNameTextboxFocused = false;
            IsNameTextboxFocused = true;
        }
        public void SaveDietPlan(object input = null)
        {
            if (String.IsNullOrWhiteSpace(DietName) || DietNutrients.Count == 0) { return; }
            new DietPlanRepo().Save(new DietPlan() { Name = DietName, RecommendedNutrients = DietNutrients.ToList() });
            DietName = null;
            DietNutrients.Clear();
        }
    }
}
