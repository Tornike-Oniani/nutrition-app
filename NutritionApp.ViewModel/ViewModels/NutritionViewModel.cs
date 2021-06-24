using NutritionApp.ViewModel.Classes;
using NutritionApp.ViewModel.Models;
using NutritionApp.ViewModel.Repositories.Base;
using NutritionApp.ViewModel.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace NutritionApp.ViewModel.ViewModels
{
    public class NutritionViewModel : BaseViewModel
    {
        private FoodAnalyzer FA = new FoodAnalyzer();
        private List<Nutrient> _stats;
        private bool _isEntryFocused;
        private string _foodName;
        private string _amount;

        // Public properties
        public ObservableCollection<FoodElement> FoodElements { get; set; }
        public string FoodName
        {
            get { return _foodName; }
            set { _foodName = value; OnPropertyChanged("FoodName"); }
        }
        public string Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged("Amount"); }
        }
        public List<Nutrient> Stats
        {
            get { return _stats; }
            set { _stats = value; OnPropertyChanged("Stats"); }
        }
        public bool IsEntryFocused
        {
            get { return _isEntryFocused; }
            set { _isEntryFocused = value; OnPropertyChanged("IsEntryFocused"); }
        }
        public FoodElement SelectedFoodElement { get; set; }

        // Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        // Constructor
        public NutritionViewModel()
        {
            IsEntryFocused = true;
            FA.generateStats();
            Stats = FA.getStats();

            FoodElements = new ObservableCollection<FoodElement>();
            AddCommand = new RelayCommand(Add);
            DeleteCommand = new RelayCommand(Delete);
        }

        // Command actions
        public void Add(object input = null)
        {
            if (!FA.checkFood(FoodName)) return; // check if foodname exists
            FoodElement thisElement = new FoodElement() { Name = FoodName, Amount = FA.getAmountInGrams(FoodName, Amount) };
            if (thisElement.Amount == -1) return;
            FA.AddFoodToHistory(thisElement);
            FoodElements.Add(thisElement);
            FA.generateStats();
            Stats = FA.getStats();

            // Clean up for next entry
            FoodName = null;
            Amount = null;
            IsEntryFocused = false;
            IsEntryFocused = true;
        }
        public void Delete(object input = null)
        {
            if (SelectedFoodElement == null) return;
            FA.RemoveFoodFromHistory(SelectedFoodElement);
            FoodElements.Remove(SelectedFoodElement);
            FA.generateStats();
            Stats = FA.getStats();
        }
    }

    /*
        TODO:
        - Get food nutrients from API
        - Improve UI (Progress bars, food name labels, empty foodname after add...)
        - Import food history from file
        - Save nutrition stat and food plan to a file
        - food name to lower..

        NICE TO HAVE:
        - Day/Week schedule
        - offer suggestions in food name
    */
}
