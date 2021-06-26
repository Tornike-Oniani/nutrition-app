using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NutritionApp.ViewModel.Classes;
using NutritionApp.ViewModel.Models;

namespace NutritionApp.UserControls
{
    /// <summary>
    /// Interaction logic for NutrientCollection.xaml
    /// </summary>
    public partial class NutrientCollection : UserControl, INotifyPropertyChanged
    {
        // Private attributes
        private string _nutrientName;
        private double _nutrientAmount;
        private bool _isNutrientTextBoxFocused;

        // Dependepncy properties
        private static readonly DependencyProperty HasCaloriesUnitProperty =
            DependencyProperty.Register("HasCaloriesUnit", typeof(bool), typeof(NutrientCollection), new PropertyMetadata(false));

        public bool HasCaloriesUnit 
        {
            get { return (bool)GetValue(HasCaloriesUnitProperty); }
            set { SetValue(HasCaloriesUnitProperty, value); } 
        }

        private static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<Nutrient>), typeof(NutrientCollection), new PropertyMetadata(null));

        public ObservableCollection<Nutrient> ItemsSource
        {
            get { return (ObservableCollection<Nutrient>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Public properties
        public string NutrientName
        {
            get { return _nutrientName; }
            set { _nutrientName = value; OnPropertyChanged("NutrientName"); }
        }
        public double NutrientAmount
        {
            get { return _nutrientAmount; }
            set { _nutrientAmount = value; OnPropertyChanged("NutrientAmount"); }
        }
        public string Unit { get; set; }
        public List<string> AvailableUnits { get; set; }
        public bool IsNutrientTextBoxFocused
        {
            get { return _isNutrientTextBoxFocused; }
            set { _isNutrientTextBoxFocused = value; OnPropertyChanged("IsNutrientTextBoxFocused"); }
        }


        // Commands
        public ICommand AddCommand { get; set; }

        // Constructor
        public NutrientCollection()
        {
            InitializeComponent();

            IsNutrientTextBoxFocused = false;
            if (HasCaloriesUnit)
                AvailableUnits = new List<string>() { "mcg", "mg", "g", "" };
            else
                AvailableUnits = new List<string>() { "mcg", "mg", "g" };

            Unit = AvailableUnits[0];
            AddCommand = new RelayCommand(Add);
        }

        // Command actions
        public void Add(object input = null)
        {
            ItemsSource.Add(new Nutrient() { Nutrient = NutrientName.ToLower(), Amount = NutrientAmount, Unit = Unit });
            NutrientName = null;
            NutrientAmount = 0;
            IsNutrientTextBoxFocused = false;
            IsNutrientTextBoxFocused = true;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
