using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Activation;
using MetroDict.Commands;

namespace MetroDict.Shared.Data
{
    /// <summary>
    /// view model for main page.
    /// </summary>
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private List<ArticleViewModel> _results;
        private int _selectedIndex;
        private string _selectedTitle;
        private string _selectedBody;

        #region --- commands ---

        public SearchCommand SearchCmd { get; set; }
        public ItemClickedCommand ItemClickedCmd { get; set; }

        #endregion

        #region --- properties --- 

        public string QueryText { get; set; }

        public List<ArticleViewModel> Results
        {
            get { return _results; }
            set { SetField(ref _results, value, "Results"); }
        }

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }

            set
            {
                _selectedIndex = value;
                ShowArticle(_selectedIndex);
            }
        }

        public string SelectedTitle
        {
            get
            {
                return _selectedTitle;
            }

            set
            {
                SetField(ref _selectedTitle, value, "SelectedTitle");
            }
        }

        public string SelectedBody
        {
            get
            {
                return _selectedBody;
            }

            set
            {
                SetField(ref _selectedBody, value, "SelectedBody");
            }
        }

        #endregion

        private void ShowArticle(int selectedIndex)
        {
            if ((selectedIndex >= 0) && (selectedIndex < _results.Count))
            {
                var viewModel = _results.ElementAt(selectedIndex);
                SelectedTitle = viewModel.Title.Trim();
                SelectedBody = viewModel.Body;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainPageViewModel()
	    {
            SearchCmd = new SearchCommand(this);
            ItemClickedCmd = new ItemClickedCommand(this);
            Results = new List<ArticleViewModel>();
	    }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
