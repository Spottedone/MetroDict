using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Xml;
using System.Linq;
using Windows.UI.Xaml;
using MetroDict.Shared.Data;
using MetroDictLib;
using MetroDictLib.Helpers;

namespace MetroDict.Commands
{
    public class SearchCommand : ICommand
    {
        private readonly MainPageViewModel _model;

        public SearchCommand(MainPageViewModel model)
        {
            _model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            App thisApp = (Application.Current as App);
            var queryText = _model.QueryText;

            List<StarDict> dicts = thisApp.Dictionaries;

            List<ArticleViewModel> results = new List<ArticleViewModel>();
            foreach (var dict in dicts)
            {
                var found = dict.GetArticlesContaining(queryText);
                results.AddRange(found.Select(r => new ArticleViewModel { Title = r.Key, Body = r.Value }));
            }
            _model.Results = results;
        }

        public event EventHandler CanExecuteChanged;
    }
}
