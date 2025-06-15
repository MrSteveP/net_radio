using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace net_radio
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);

#if ANDROID
            // Get the status bar height in pixels
            var statusBarHeight = 0;
            var context = Android.App.Application.Context;
            int resourceId = context.Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
                statusBarHeight = context.Resources.GetDimensionPixelSize(resourceId);

            // Convert pixels to device-independent units
            var density = context.Resources.DisplayMetrics.Density;
            var statusBarHeightDp = statusBarHeight / density;

            this.Padding = new Thickness(0, statusBarHeightDp, 0, 0);
#endif

            BindingContext = new RadioViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AudioVisualizer.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            AudioVisualizer.Stop();
        }

        private void OnStationChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BindingContext is RadioViewModel vm && e.CurrentSelection.FirstOrDefault() is RadioStation station)
            {
                vm.PlayStationCommand.Execute(station);
            }
        }
    }
}
