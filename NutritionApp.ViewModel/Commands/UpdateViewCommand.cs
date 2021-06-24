using NutritionApp.ViewModel.Enums;
using NutritionApp.ViewModel.ViewModels;
using NutritionApp.ViewModel.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace NutritionApp.ViewModel.Commands
{
    class UpdateViewCommand : ICommand
    {
        private Action<BaseViewModel> _navigate;

        public UpdateViewCommand(Action<BaseViewModel> navigate)
        {
            this._navigate = navigate;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch ((ViewType)parameter)
            {
                case ViewType.Dashboard:
                    _navigate(new DashboardViewModel());
                    break;
                case ViewType.Nutrition:
                    _navigate(new NutritionViewModel());
                    break;
                case ViewType.FoodAPI:
                    _navigate(new FoodAPIViewModel());
                    break;
                default:
                    break;
            }
        }
    }
}
