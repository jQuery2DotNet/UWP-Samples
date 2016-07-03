using FlipViewDemo.Model;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace FlipViewDemo
{
    public sealed partial class MainPage : Page
    {
        public List<SampleItem> flipViewData { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            flipViewData = new List<SampleItem>();
            flipViewData.Add(new SampleItem() { Name = "Item 1", Image = "https://source.unsplash.com/category/food/1600x750" });
            flipViewData.Add(new SampleItem() { Name = "Item 2", Image = "https://source.unsplash.com/category/nature/1600x750" });
            flipViewData.Add(new SampleItem() { Name = "Item 3", Image = "https://source.unsplash.com/category/buildings/1600x750" });
            flipViewData.Add(new SampleItem() { Name = "Item 4", Image = "https://source.unsplash.com/category/technology/1600x750" });
            flipView1.ItemsSource = flipViewData;
        }
    }
}
