using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using GalaSoft.MvvmLight.Command;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MessageDialog
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

        private async void Showbutton_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Popups.MessageDialog showDialog = new Windows.UI.Popups.MessageDialog("UWP message dialog without title - Windows 10");
            showDialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            showDialog.Commands.Add(new UICommand("No") { Id = 1 });
            showDialog.DefaultCommandIndex = 0;
            showDialog.CancelCommandIndex = 1;
            var result = await showDialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                //do your task
            }
            else
            {
                //skip your task
            }
        }

        private async void ButtonShowMessageDialog_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new Windows.UI.Popups.MessageDialog(
                        "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                        "Lorem Ipsum has been the industry's standard dummy text.",
                        "Three Buttons Message Dialog ");

            dialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new UICommand("No") { Id = 1 });

            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                // Adding a 3rd command will crash the app when running on Mobile !!
                dialog.Commands.Add(new UICommand("Maybe later") { Id = 2 });
            }

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            (sender as Button).Content = $"Result: {result.Label} ({result.Id})";
        }

        private async void ButtonShowContentDialog1_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var dialog = new ContentDialog()
            {
                Title = "Dark Theme Content Dialog",
                RequestedTheme = ElementTheme.Dark,
                //FullSizeDesired = true,
                MaxWidth = this.ActualWidth // Required for Mobile!
            };

            // Setup Content
            var panel = new StackPanel();

            panel.Children.Add(new TextBlock
            {
                Text = "It is a long established fact that a reader will be distracted by the readable. " +
                                "Content of a page when looking at its layout.",
                TextWrapping = TextWrapping.Wrap,
            });

            var cb = new CheckBox
            {
                Content = "Agree"
            };

            cb.SetBinding(CheckBox.IsCheckedProperty, new Binding
            {
                Source = dialog,
                Path = new PropertyPath("IsPrimaryButtonEnabled"),
                Mode = BindingMode.TwoWay,
            });

            panel.Children.Add(cb);
            dialog.Content = panel;

            // Add Buttons
            dialog.PrimaryButtonText = "OK";
            dialog.IsPrimaryButtonEnabled = false;
            dialog.PrimaryButtonClick += delegate
            {
                btn.Content = "Result: OK";
            };

            dialog.SecondaryButtonText = "Cancel";
            dialog.SecondaryButtonClick += delegate
            {
                btn.Content = "Result: Cancel";
            };

            // Show Dialog
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.None)
            {
                btn.Content = "Result: NONE";
            }
        }

        private async void ButtonShowContentDialog2_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var dialog = new ContentDialog()
            {
                Title = "Full Size Desired Content Dialog",
                FullSizeDesired = true,
                MaxWidth = this.ActualWidth // Required for Mobile!
            };

            var panel = new StackPanel();

            panel.Children.Add(new TextBlock
            {
                Text = "Contrary to popular belief, Lorem Ipsum is not simply random text. " +
                        "It has roots in a piece of classical Latin literature from 45 BC.",
                TextWrapping = TextWrapping.Wrap,
            });

            var cb = new CheckBox
            {
                Content = "Agree"
            };
            panel.Children.Add(cb);
            dialog.Content = panel;

            // The CanExecute of the Command does not enable/disable the button :-(
            dialog.PrimaryButtonText = "OK";
            var cmd = new RelayCommand(() =>
            {
                btn.Content = "Result: OK";
            }, () => cb.IsChecked ?? false);

            dialog.PrimaryButtonCommand = cmd;

            dialog.SecondaryButtonText = "Cancel";
            dialog.SecondaryButtonCommand = new RelayCommand(() =>
            {
                btn.Content = "Result: Cancel";
            });

            cb.Click += delegate
            {
                cmd.RaiseCanExecuteChanged();
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.None)
            {
                btn.Content = "Result: NONE";
            }
        }

        private async void ButtonShowContentDialog3_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var result = await MyContentDialog.ShowAsync();
            btn.Content = "Result: " + result;
        }

        private async void ButtonShowContentDialog4_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            // Creates the password box
            var passwordBox = new PasswordBox { IsPasswordRevealButtonEnabled = true, Margin = new Thickness(5) };

            // Creates the StackPanel with the content
            var contentPanel = new StackPanel();
            contentPanel.Children.Add(new TextBlock
            {
                Text = "Insert your password to access the application",
                Margin = new Thickness(5),
                TextWrapping = TextWrapping.WrapWholeWords
            });
            contentPanel.Children.Add(passwordBox);

            // Creates the password dialog
            ContentDialog _passwordDialog = new ContentDialog
            {
                PrimaryButtonText = "Accept",
                IsPrimaryButtonEnabled = false,
                SecondaryButtonText = "Exit",
                Title = "Authentication",
                Content = contentPanel
            };

            // Report that the dialog has been opened to avoid overlapping
            _passwordDialog.Opened += delegate
            {
                // HACK - opacity set to 0 to avoid seeing behind dialog
                Window.Current.Content.Opacity = 0;
            };

            // Report that the dialog has been closed to enable it again
            _passwordDialog.Closed += delegate
            {
                // HACK - opacity set to 1 to reset the window to original options
                Window.Current.Content.Opacity = 1;
            };

            // Clear inserted password for next logins
            _passwordDialog.PrimaryButtonClick += delegate
            {
                // ... login ...
            };

            // Close the app if the user doesn't insert the password
            _passwordDialog.SecondaryButtonClick += delegate { Application.Current.Exit(); };

            // Set the binding to enable/disable the accept button 

            _passwordDialog.SetBinding(ContentDialog.IsPrimaryButtonEnabledProperty, new Binding
            {
                Source = passwordBox,
                Path = new PropertyPath("Password"),
                Mode = BindingMode.TwoWay
            });

            var result = await _passwordDialog.ShowAsync();
            btn.Content = "Result: " + result;
        }
    }
}
