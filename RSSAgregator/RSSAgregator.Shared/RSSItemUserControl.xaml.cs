using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RSSAgregator
{
    public sealed partial class RSSItemUserControl : UserControl
    {
        public RSSItemUserControl(string Title, string Description, string Category, string PubDate)
        {
            this.InitializeComponent();

            TextBlockTitle.Text = Title;
            TextBlockDescription.Text = Regex.Replace(Description, "<.*?>", string.Empty);
            TextBlockCategory.Text = Category;
            TextBlockPubDate.Text = this.FormatDateString(PubDate);
        }

        private string FormatDateString(string date)
        {
            var dateArray = date.Split(' ');
            return string.Concat(dateArray[0], " ", dateArray[1], " ", dateArray[2], " ", dateArray[3]);
        }
    }
}
