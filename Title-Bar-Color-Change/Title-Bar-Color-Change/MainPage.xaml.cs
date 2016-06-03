using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Title_Bar_Color_Change
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SetTitleBarBackground();
        }

        private void SetTitleBarBackground()
        {
            // Get the instance of the Title Bar
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            // Set the color of the Title Bar content
            titleBar.BackgroundColor = Colors.Orange;
            titleBar.ForegroundColor = Colors.Green;

            // Set the color of the Title Bar buttons
            titleBar.ButtonBackgroundColor = Colors.Orange;
            titleBar.ButtonForegroundColor = Colors.Green;
        }
    }
}
