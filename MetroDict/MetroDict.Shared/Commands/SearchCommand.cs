using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MetroDict.Commands
{
    public class SearchCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            
        }

        public event EventHandler CanExecuteChanged;
    }
}
