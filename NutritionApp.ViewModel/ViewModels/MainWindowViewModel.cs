using NutritionApp.ViewModel.Classes;
using NutritionApp.ViewModel.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace NutritionApp.ViewModel.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Private attributes
        private FoodAnalyzer FA = new FoodAnalyzer(@"C:\Users\Lejan\Desktop\nutritionPy\repo.json",
            @"C:\Users\Lejan\Desktop\nutritionPy\recommendedValues.csv");
        private string _calc;

        // Public properties
        public ObservableCollection<FoodElement> FoodElements { get; set; }
        public string FoodName { get; set; }
        public string Amount { get; set; }
        public string Calc
        {
            get { return _calc; }
            set { _calc = value; OnPropertyChanged("Calc"); }
        }

        // Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public FoodElement SelectedFoodElement { get; set; }

        // Constructor
        public MainWindowViewModel()
        {
            FoodElements = new ObservableCollection<FoodElement>()
            {
                new FoodElement() { Name = "Orange", Amount = 10 },
                new FoodElement() { Name = "Rice", Amount = 100 },
                new FoodElement() { Name = "Apple", Amount = 25.1 },
                new FoodElement() { Name = "Pear", Amount = 5.5 },
            };
            AddCommand = new RelayCommand(add);
            DeleteCommand = new RelayCommand(delete);
        }

        // Command actions
        public void add(object input = null)
        {
            if (!FA.checkFood(FoodName)) return; // check if foodname exists
            FoodElement thisElement = new FoodElement() { Name = FoodName, Amount = FA.getAmountInGrams(FoodName, Amount) };
            if (thisElement.Amount == -1) return;
            FoodElements.Add(thisElement);
            FA.foodHistory.Add(thisElement);
            FA.generateStats();
            Calc = FA.generateStatString();
        }
        public void delete(object input = null)
        {
            if (SelectedFoodElement == null) return;
            FoodElements.Remove(SelectedFoodElement);
            FA.foodHistory.Remove(SelectedFoodElement);
            FA.generateStats();
            Calc = FA.generateStatString();
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