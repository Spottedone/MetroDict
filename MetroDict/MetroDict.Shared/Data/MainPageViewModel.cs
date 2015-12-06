using System;
using System.Collections.Generic;
using System.Linq;
using MetroDict.Commands;

namespace MetroDict.Shared.Data
{
    /// <summary>
    /// view model for main page.
    /// </summary>
	internal class MainPageViewModel
	{
        #region --- commands ---

	    public readonly SearchCommand SearchCommand;

	    #endregion

        public string QueryText { get; set; }
	    public List<ArticleViewModel> Results { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
	    public MainPageViewModel()
	    {
            SearchCommand = new SearchCommand();
	    }
	}
}
