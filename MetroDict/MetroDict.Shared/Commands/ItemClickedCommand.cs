using MetroDict.Shared.Data;
using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace MetroDict.Commands
{
    public class ItemClickedCommand : ICommand
    {
        private readonly MainPageViewModel _model;

        public ItemClickedCommand(MainPageViewModel viewModel)
        {
            _model = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ListViewItem clickedItem = (ListViewItem) parameter;
            var ctx = clickedItem.DataContext;
            var articleViewModel = (ctx as ArticleViewModel);
        }

        public event EventHandler CanExecuteChanged;
    }
}
