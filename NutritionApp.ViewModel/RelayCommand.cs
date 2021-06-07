using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace NutritionApp.ViewModel
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;

        public RelayCommand(Action<object> execute)
        {
            if (execute == null)
                throw new NullReferenceException("execute can't be null");

            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
