using MetroDictLib.Helpers;
using System.Text.RegularExpressions;

namespace MetroDict.Shared.Data
{
    public class ArticleViewModel
    {
        private string _body;
        private const string _darkCss = @"<link href=""ms-appx-web:///assets/article_dark.css"" rel=""stylesheet"" />";
		private const string _lightCss = @"<link href=""ms-appx-web:///assets/article_light.css"" rel=""stylesheet"" />";
		private string _search = @"<k>[\w\s]+</k>";

        public string Title { get; set; }
        public string BodyPreview
        {
            get
            {
                return Regex.Replace(_body, _search, "").TruncateWithEllipsis(50);
            }
        }

        /// <summary>
        /// Returns body of an article. injects CSS.
        /// </summary>
        public string Body
        {
            get
            {
                return string.Concat(getCss(), _body);
            }

            set
            {
                _body = value;
            }
        }

		private string getCss()
		{
			if (ThemeHelper.IsDarkTheme())
			{
				return _darkCss;
			}
			return _lightCss;
		}
    }
}
