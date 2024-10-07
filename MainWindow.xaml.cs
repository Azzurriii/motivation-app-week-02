using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;

namespace MotivationTestApp;

public partial class MainWindow : Window
{
    ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

    public MainWindow()
    {
        this.InitializeComponent();
        LoadSavedLogin();
    }

    private void LoadSavedLogin()
    {
        if (localSettings.Values.ContainsKey("username") && localSettings.Values["username"] != null)
        {
            UsernameTextBox.Text = localSettings.Values["username"].ToString();

            if (localSettings.Values.ContainsKey("password") && localSettings.Values.ContainsKey("entropy"))
            {
                try
                {
                    var encryptedPasswordInBase64 = localSettings.Values["password"].ToString();
                    var entropyInBase64 = localSettings.Values["entropy"].ToString();

                    var encryptedPasswordInBytes = Convert.FromBase64String(encryptedPasswordInBase64);
                    var entropyInBytes = Convert.FromBase64String(entropyInBase64);

                    var passwordInBytes = ProtectedData.Unprotect(
                        encryptedPasswordInBytes,
                        entropyInBytes,
                        DataProtectionScope.CurrentUser);

                    var password = Encoding.UTF8.GetString(passwordInBytes);
                    PasswordBox.Password = password;
                }
                catch (Exception ex)
                {
                    PasswordBox.Password = "";
                    System.Diagnostics.Debug.WriteLine($"Error decrypting password: {ex.Message}");
                }
            }
        }
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var username = UsernameTextBox.Text;
        var password = PasswordBox.Password;

        if (username == "admin" && password == "123")
        {
            if (RememberMeCheckBox.IsChecked == true)
            {
                SaveLoginData(username, password);
            }

            var mainWindow = new DashBoard();
            mainWindow.Activate();
            this.Close();
        }
        else
        {
            var dialog = new ContentDialog
            {
                Title = "Login Failed",
                Content = "Invalid username or password",
                CloseButtonText = "Ok",
                XamlRoot = this.Content.XamlRoot
            };
            dialog.ShowAsync();
        }
    }

    private void SaveLoginData(string username, string password)
    {
        byte[] entropy = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(entropy);
        }

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] encryptedPassword = ProtectedData.Protect(
            passwordBytes,
            entropy,
            DataProtectionScope.CurrentUser);

        localSettings.Values["username"] = username;
        localSettings.Values["password"] = Convert.ToBase64String(encryptedPassword);
        localSettings.Values["entropy"] = Convert.ToBase64String(entropy);
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Exit();
    }
}