using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppBar
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

        private void LikeButton_Checked(object sender, RoutedEventArgs e)
        {
            LikeButton.Icon = new SymbolIcon(Symbol.SolidStar);
            SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Gold);
            LikeButton.Icon.Foreground = brush;
        }

        private void LikeButton_Unchecked(object sender, RoutedEventArgs e)
        {
            LikeButton.Icon = new SymbolIcon(Symbol.OutlineStar);
            SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Black);
            LikeButton.Icon.Foreground = brush;
        }

        private void Play_Checked(object sender, RoutedEventArgs e)
        {
            Play.Icon = new SymbolIcon(Symbol.Pause);
            Play.Label = "Pause";
        }

        private void Play_Unchecked(object sender, RoutedEventArgs e)
        {
            Play.Icon = new SymbolIcon(Symbol.Play);
            Play.Label = "Play";
        }

        private void Volume_Checked(object sender, RoutedEventArgs e)
        {
            Volume.Icon = new SymbolIcon(Symbol.Mute);
            Volume.Label = "Mute";
        }

        private void Volume_Unchecked(object sender, RoutedEventArgs e)
        {
            Volume.Icon = new SymbolIcon(Symbol.Volume);
            Volume.Label = "Volume";
        }
    }
}
