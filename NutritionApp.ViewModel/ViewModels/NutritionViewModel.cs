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
        private StatsCalculator SA = new StatsCalculator();
        private List<NutrientGainedAndRecommended> _stats;
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
        public List<NutrientGainedAndRecommended> Stats
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
            Stats = SA.generateStats();

            FoodElements = new ObservableCollection<FoodElement>();
            AddCommand = new RelayCommand(Add);
            DeleteCommand = new RelayCommand(Delete);
        }

        // Command actions
        public void Add(object input = null)
        {
            if (!SA.TryAddFood(FoodName, Amount)) { return; }

            // Clean up for next entry
            FoodName = null;
            Amount = null;
            IsEntryFocused = false;
            IsEntryFocused = true;
        }
        public void Delete(object input = null)
        {
            if (SelectedFoodElement == null) return;

            SA.RemoveFood(SelectedFoodElement);
            Stats = SA.generateStats();
        }
    }
}
