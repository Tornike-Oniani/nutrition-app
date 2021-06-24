using NutritionApp.ViewModel.Classes;
using NutritionApp.ViewModel.Commands;
using NutritionApp.ViewModel.Enums;
using NutritionApp.ViewModel.Models;
using NutritionApp.ViewModel.ViewModels.Base;
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
        private BaseViewModel _selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }


        public ICommand NavigateCommand { get; set; }

        public MainWindowViewModel()
        {
            NavigateCommand = new UpdateViewCommand(Navigate);

            NavigateCommand.Execute(ViewType.Nutrition);
        }

        public void Navigate(BaseViewModel viewModel)
        {
            this.SelectedViewModel = viewModel;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}