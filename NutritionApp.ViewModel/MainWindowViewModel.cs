using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace NutritionApp.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FoodElement> FoodElements { get; set; }
        //private FoodAnalyzer FA = new FoodAnalyzer(@"C:\Users\Lejan\Desktop\nutritionPy\repo.json", 
        //    @"C:\Users\Lejan\Desktop\nutritionPy\recommendedValues.csv");
        public string FoodName { get; set; }
        public string Amount { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public FoodElement SelectedFoodElement { get; set; }
        private string _calc;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }


        public string Calc
        {
            get { return _calc; }
            set { _calc = value; OnPropertyChanged("Calc");}
        }

        public MainWindowViewModel()
        {
            FoodElements = new ObservableCollection<FoodElement>();
            AddCommand = new RelayCommand(add);
            DeleteCommand = new RelayCommand(delete);
        }
        public void add(object input = null)
        {
            //if (!FA.checkFood(FoodName)) return; // check if foodname exists
            //FoodElement thisElement = new FoodElement() { Name = FoodName, Amount = FA.getAmountInGrams(FoodName,Amount) };
            //if (thisElement.Amount == -1) return;
            //FoodElements.Add(thisElement);
            //FA.foodHistory.Add(thisElement);
            //FA.generateStats();
            //Calc = FA.generateStatString();
        }

        public void delete(object input = null)
        {
            //if (SelectedFoodElement == null) return;
            //FoodElements.Remove(SelectedFoodElement);
            //FA.foodHistory.Remove(SelectedFoodElement);
            //FA.generateStats();
            //Calc = FA.generateStatString();
        }
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