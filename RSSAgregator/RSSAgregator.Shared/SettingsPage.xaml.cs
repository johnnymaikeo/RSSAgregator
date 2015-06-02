using RSSAgregator.Model;
using RSSAgregator.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

#if WINDOWS_PHONE
using Windows.Phone.UI.Input;
#endif

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RSSAgregator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        BookmarksViewModel vm;

        public SettingsPage()
        {
            this.InitializeComponent();
            this.vm = new BookmarksViewModel();

            #if WINDOWS_PHONE
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            #endif
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            UpdateListView();
        }

        #if WINDOWS_PHONE
        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {

            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
                return;

            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }
        #endif

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TextBoxName.Text) && !String.IsNullOrEmpty(TextBoxUrl.Text))
            {
                if (this.vm.Add(TextBoxName.Text, TextBoxUrl.Text))
                {
                    TextBlockOutputMessage.Text = "Save successfully";
                    UpdateListView();
                }
                else
                {
                    TextBlockOutputMessage.Text = "Error! Please check URL";
                }
            }
            else
            {
                TextBlockOutputMessage.Text = "Error! URL and Name cannot be empty";
            }

        }

        private async void UpdateListView()
        {
            await this.vm.List();
            ListViewBookmarks.ItemsSource = null;
            ListViewBookmarks.ItemsSource = this.vm.BookmarksList;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void StackPanel_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);

            flyoutBase.ShowAt(senderElement);
        }

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var menuFlyoutItem = sender as MenuFlyoutItem;
            if (menuFlyoutItem != null)
            {
                var b = menuFlyoutItem.DataContext as Bookmarks;
                await this.vm.Remove(b);
                this.UpdateListView();
            }
        }
    }
}
