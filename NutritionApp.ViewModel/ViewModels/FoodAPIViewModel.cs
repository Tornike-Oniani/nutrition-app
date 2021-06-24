using NutritionApp.ViewModel.Classes;
using NutritionApp.ViewModel.Models;
using NutritionApp.ViewModel.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using LiteDB;
using NutritionApp.ViewModel.Repositories.Base;

namespace NutritionApp.ViewModel.ViewModels
{
    public class FoodAPIViewModel : BaseViewModel
    {
        // Private attributes
        private string _name;
        private double _calories;
        private double _portionWeight;
        private string _nutrientName;
        private string _nutrientAmount;

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
        public string NutrientName
        {
            get { return _nutrientName; }
            set { _nutrientName = value; OnPropertyChanged("NutrientName"); }
        }
        public string NutrientAmount
        {
            get { return _nutrientAmount; }
            set { _nutrientAmount = value; OnPropertyChanged("NutrientAmount"); }
        }
        public string Unit { get; set; }
        public ObservableCollection<NutrientElement> Nutrients { get; set; }
        public List<string> AvailableUnits { get; set; }

        // Commands
        public ICommand AddNutrientCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand PlanCommand { get; set; }

        // Constructor
        public FoodAPIViewModel()
        {
            Nutrients = new ObservableCollection<NutrientElement>();
            AvailableUnits = new List<string>()
            {
                "mg",
                "g"
            };
            Unit = AvailableUnits[0];

            AddNutrientCommand = new RelayCommand(AddNutrient);
            SaveCommand = new RelayCommand(Save);
            PlanCommand = new RelayCommand(Plan);
        }

        // Command actions
        public void AddNutrient(object input = null)
        {
            if (String.IsNullOrWhiteSpace(NutrientName) || String.IsNullOrWhiteSpace(NutrientAmount) || String.IsNullOrWhiteSpace(Unit)) { return; }
            Nutrients.Add(new NutrientElement() { Nutrient = NutrientName, Amount = Double.Parse(NutrientAmount), Unit = Unit});
            NutrientName = null;
            NutrientAmount = null;
        }
        public void Save(object input = null)
        {
            // Persist to database
            List<NutrientElement> nutrition = new List<NutrientElement>();
            foreach (NutrientElement nutrientElement in Nutrients)
            {
                nutrition.Add(nutrientElement);
            }
            new FoodRepo().Save(new FoodInfo() { Name = Name, Calories = Calories, PortionWeight = PortionWeight, Nutrition = nutrition });

            // Prepare for next entry
            Name = null;
            Calories = 0;
            PortionWeight = 0;
            Nutrients.Clear();
        }
        public void Plan(object input = null)
        {
            new FoodRepo().Plan();
        }
    }
}
