using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MetroDict
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExtendedSplash : Page
    {
        public string Message
        {
            get { return loadingMessage.Text; }
            set { loadingMessage.Text = value; }
        }

        public ExtendedSplash(SplashScreen splash)
        {
            InitializeComponent();
            //extendedSplashImage.SetValue(Canvas.LeftProperty, splash.ImageLocation.X);
            //extendedSplashImage.SetValue(Canvas.TopProperty, splash.ImageLocation.Y);
            //extendedSplashImage.Height = splash.ImageLocation.Height;
            //extendedSplashImage.Width = splash.ImageLocation.Width;
            // Position the extended splash screen’s progress ring.
            //ProgressRing.SetValue(Canvas.TopProperty, splash.ImageLocation.Y + splash.ImageLocation.Height + 32);
            //ProgressRing.SetValue(Canvas.LeftProperty, splash.ImageLocation.X + (splash.ImageLocation.Width / 2) - 15);
        }

        internal void onSplashScreenDismissed(SplashScreen sender, object e)
        {
            // The splash screen has been dismissed and the extended splash screen is now in view.
        }
    }
}
