using System;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MapControl
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

        private async void GeMyLocation_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var geolocator = new Geolocator();
            var position = await geolocator.GetGeopositionAsync();

            var mapLocation = await MapLocationFinder.FindLocationsAtAsync(position.Coordinate.Point);
            if (mapLocation.Status == MapLocationFinderStatus.Success)
            {
                MyLocation.Text = mapLocation?.Locations?[0].Address.FormattedAddress;
            }
            else
            {
                MyLocation.Text = "Not Found";
            }
        }

        private async void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            if (MyMap.Is3DSupported)
            {
                MyMap.Style = MapStyle.Aerial3DWithRoads;
                //MyMap.Style = MapStyle.Terrain;
                MyMap.MapServiceToken = "Your Service Token";

                BasicGeoposition geoPosition = new BasicGeoposition();
                geoPosition.Latitude = 27.175015;
                geoPosition.Longitude = 78.042155;
                // get position
                Geopoint myPoint = new Geopoint(geoPosition);
                //create POI
                MapIcon myPOI = new MapIcon { Location = myPoint, Title = "My Position", NormalizedAnchorPoint = new Point(0.5, 1.0), ZIndex = 0 };
                // Display an image of a MapIcon
                myPOI.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pin.png"));
                // add to map and center it
                MyMap.MapElements.Add(myPOI);
                MyMap.Center = myPoint;
                MyMap.ZoomLevel = 10;

                MapScene mapScene = MapScene.CreateFromLocationAndRadius(new Geopoint(geoPosition), 500, 150, 70);
                await MyMap.TrySetSceneAsync(mapScene);
            }
        }
    }
}
