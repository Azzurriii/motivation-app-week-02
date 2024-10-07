using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class RandomQuotesPage : Window
    {
        private List<string> imagePaths;
        private Random random;
        private int currentIndex;
        public RandomQuotesPage()
        {
            this.InitializeComponent();
            random = new Random();
            currentIndex = 0;
            LoadImages();
            ShowImage();
        }

        private void LoadImages()
        {
            string imageDirectory = Path.Combine(AppContext.BaseDirectory, "Assets", "Images");

            if (Directory.Exists(imageDirectory))
            {
                imagePaths = new List<string>(Directory.GetFiles(imageDirectory, "*.*", SearchOption.TopDirectoryOnly));
            }
        }

        private void ShowRandomImage()
        {
            if (imagePaths.Count > 0)
            {
                int index = random.Next(imagePaths.Count);
                ShowImageAtIndex(index);
            }
        }

        private void ShowImage()
        {
            if (imagePaths.Count > 0)
            {
                ShowImageAtIndex(currentIndex);
                currentIndex = (currentIndex + 1) % imagePaths.Count;
            }
        }

        private void ShowImageAtIndex(int index)
        {
            string selectedImagePath = imagePaths[index];

            BitmapImage bitmapImage = new BitmapImage(new Uri(selectedImagePath));
            DisplayedImage.Source = bitmapImage;
        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            ShowRandomImage();
        }

        private void ReturnDashBoard_Click(object sender, RoutedEventArgs e)
        {
            var dashBoard = new DashBoard();
            dashBoard.Activate();
            this.Close();
        }
    }
}
