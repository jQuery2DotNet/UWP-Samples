using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Mdl2Tool
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Dictionary<string, string> mdlIconList { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            mdlIconList = GetFieldValues(new Mdl2());
        }

        private Dictionary<string, string> GetFieldValues(object obj)
        {
            return obj.GetType().GetProperties().ToDictionary(f => f.Name, f => (string)f.GetValue(null));
        }

        private void TextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            string searchValue = txtSearch.Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                searchValue = searchValue.ToLower();
                MdlItemsControl.ItemsSource = mdlIconList.Where(p => p.Key.ToLower().Contains(searchValue));
            }
            else
            {
                MdlItemsControl.ItemsSource = mdlIconList;
            }
        }
    }
}
