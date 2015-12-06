using System;
using System.Collections.Generic;
using System.Linq;
using MetroDict.Commands;

namespace MetroDict.Shared.Data
{
    /// <summary>
    /// view model for main page.
    /// </summary>
    public class MainPageViewModel
	{
        #region --- commands ---

        public SearchCommand SearchCmd { get; set; }

	    #endregion

        public string QueryText { get; set; }
	    public List<ArticleViewModel> Results { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
	    public MainPageViewModel()
	    {
            SearchCmd = new SearchCommand(this);
            Results = new List<ArticleViewModel>();
	    }
	}
}
