using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MotivationTestApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashBoard : Window
    {
        public DashBoard()
        {
            this.InitializeComponent();
        }

        private void RandomQuotesButton_Click(object sender, RoutedEventArgs e)
        {
            var randomQuotesWindow = new RandomQuotesPage();
            randomQuotesWindow.Activate();
            this.Close();
        }

        private void QuotesTestButton_Click(object sender, RoutedEventArgs e)
        {
            var quotesTestWindow = new QuotesTestPage();
            quotesTestWindow.Activate();
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Activate();
            this.Close();
        }
    }

    
}
