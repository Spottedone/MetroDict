using MetroDictLib.Helpers;

namespace MetroDict.Shared.Data
{
    public class ArticleViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }

        public string BodyPreview
        {
            get
            {
                return Body.TruncateWithEllipsis(50);
            }
        }
    }
}
