using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace MetroDictLib.Helpers
{
	public static class ThemeHelper
	{
#if WINDOWS_PHONE_APP
		private static Color DarkColor = Color.FromArgb(0xFF, 0x1D, 0x1D, 0x1D);
		private static Color LightColor = Color.FromArgb(0xFF, 0xF4, 0xF4, 0xF4);
#else
		private static Color DarkColor = Color.FromArgb(0xFF, 0x1D, 0x1D, 0x1D);
		private static Color LightColor = Color.FromArgb(0xFF, 0xF4, 0xF4, 0xF4);
#endif
		public static bool IsDarkTheme()
		{
			SolidColorBrush brush = (Application.Current.Resources["ApplicationPageBackgroundThemeBrush"] as SolidColorBrush);
			if (brush.Color == DarkColor)
			{
				return true;
			}
			return false;
		}
	}
}
