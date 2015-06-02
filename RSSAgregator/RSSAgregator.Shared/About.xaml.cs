using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class About : Page
    {
        public About()
        {
            this.InitializeComponent();

            #if WINDOWS_PHONE
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            #endif
        }

        private void AppBarButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
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
    }
}
