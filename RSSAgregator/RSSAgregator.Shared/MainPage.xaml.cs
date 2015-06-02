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
using Windows.Web.Syndication;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RSSAgregator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainViewModel mainVM;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            mainVM = new MainViewModel();
            TextBlockMessage.Text = "loading...";
        }

        private async void LoadContent()
        {
            await mainVM.ListBookmarks();
            await ConfigureView();
        }

        public async Task ConfigureView()
        {
            Pivot pivot = new Pivot();
            pivot.Title = "WP RSS";

            if (mainVM.BookmarksList.Count == 0)
            {
                TextBlockMessage.Text = "Go to settings to add your first RSS";
                return;
            }

            foreach (Bookmarks item in mainVM.BookmarksList)
            {
                PivotItem pivotItem = new PivotItem();
                pivotItem.Header = item.Name;
                await mainVM.GetFeed(item.Url);
                
                ScrollViewer scrollViewer = new ScrollViewer();
                StackPanel stackPanel = new StackPanel();

                foreach (RSSItem rss in mainVM.rss)
                {
                    RSSItemUserControl rssItemUC = new RSSItemUserControl(rss.title, rss.description, rss.category, rss.pubDate);
                    stackPanel.Children.Add(rssItemUC);
                }

                scrollViewer.Content = stackPanel;
                pivotItem.Content = scrollViewer;
                pivot.Items.Add(pivotItem);
            }

            TextBlockMessage.Visibility = Visibility.Collapsed;
            GridMain.Children.Clear();
            GridMain.Children.Add(pivot);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadContent();
        }

        private void AppBarButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private void AppBarButtonAbout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        
    }
}
