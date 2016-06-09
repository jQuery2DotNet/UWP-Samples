using GalaSoft.MvvmLight;
using Hamburger_Menu.Models;
using Hamburger_Menu.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Hamburger_Menu.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            MenuItems = new ObservableCollection<MenuItem>(GetMenuItems());
            SelectedMenuItem = MenuItems.FirstOrDefault();
        }

        public ObservableCollection<MenuItem> MenuItems { get; set; }

        private MenuItem selectedMenuItem;

        public MenuItem SelectedMenuItem
        {
            get { return selectedMenuItem; }
            set { selectedMenuItem = value; RaisePropertyChanged(); }
        }

        private List<MenuItem> GetMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            menuItems.Add(new MenuItem() { Title = "Home", SymbolIcon = Symbol.Home, NavigateTo = typeof(Home) });
            menuItems.Add(new MenuItem() { Title = "Favourite", SymbolIcon = Symbol.OutlineStar, NavigateTo = typeof(Favourite) });
            menuItems.Add(new MenuItem() { Title = "Map", SymbolIcon = Symbol.Map, NavigateTo = typeof(Map) });
            menuItems.Add(new MenuItem() { Title = "Video", SymbolIcon = Symbol.Video, NavigateTo = typeof(Video) });
            menuItems.Add(new MenuItem() { Title = "Download", SymbolIcon = Symbol.Download, NavigateTo = typeof(Download) });

            return menuItems;
        }
    }
}
