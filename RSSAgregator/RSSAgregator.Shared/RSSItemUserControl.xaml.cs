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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RSSAgregator
{
    public sealed partial class RSSItemUserControl : UserControl
    {
        public RSSItemUserControl(string Title, string Description, string Category, string PubDate)
        {
            this.InitializeComponent();

            TextBlockTitle.Text = Title;
            TextBlockDescription.Text = Description;
            TextBlockCategory.Text = Category;
            TextBlockPubDate.Text = PubDate;
        }
    }
}
