using NutritionApp.ViewModel.Classes;
using NutritionApp.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace NutritionApp.ViewModel.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        //@"C:\Users\Lejan\Desktop\nutritionPy\repo.json", Path.Combine(Environment.CurrentDirectory, "repo.json")
        //    @"C:\Users\Lejan\Desktop\nutritionPy\recommendedValues.csv");

        // Private attributes
        private FoodAnalyzer FA = new FoodAnalyzer(
            Path.Combine(Environment.CurrentDirectory, "repo.json"),
            Path.Combine(Environment.CurrentDirectory, "recommendedValues.csv"));
        private List<GainedNutrient> _stats;
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
        public List<GainedNutrient> Stats
        {
            get { return _stats; }
            set { _stats = value; OnPropertyChanged("Stats"); }
        }
        public bool IsEntryFocused
        {
            get { return _isEntryFocused; }
            set { _isEntryFocused = value; OnPropertyChanged("IsEntryFocused"); }
        }

        // Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public FoodElement SelectedFoodElement { get; set; }

        // Constructor
        public MainWindowViewModel()
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

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}

/*
    TODO:
    - Set background to ORANGERED
    - Get food nutrients from API
    - Improve UI (Progress bars, food name labels, empty foodname after add...)
    - Import food history from file
    - Save nutrition stat and food plan to a file
    - food name to lower..

    NICE TO HAVE:
    - Day/Week schedule
    - offer suggestions in food name

*/