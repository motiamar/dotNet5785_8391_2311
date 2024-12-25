using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL;
using BO;
using PL.Admin;

/// <summary>
/// Interaction logic for LoginToTheSystem.xaml
/// </summary>
public partial class LoginToTheSystem : Window
{

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public LoginToTheSystem()
    {
        InitializeComponent();
        this.Loaded += LoginButton_Click;
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        string userType = ((ComboBoxItem)UserTypeComboBox.SelectedItem)?.Content.ToString();
        string userId = UserIdTextBox.Text;
        string password = PasswordBox.Password;

        if (string.IsNullOrEmpty(userType) || string.IsNullOrEmpty(userId))
        {
            MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (userType == "Volunteer")
        {
            // Navigate to VolunteerListWindow
            VolunteerListWindow volunteerListWindow = new VolunteerListWindow();
            volunteerListWindow.Show();
        }
        else if (userType == "Manager")
        {
            // Allow manager to choose between Admin or Volunteer list screen
            MessageBoxResult result = MessageBox.Show("Do you want to enter the Manager screen?",
                                                      "Manager Login",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Navigate to MainAdminWindow (Manager screen)
                MainAdminWindow mainAdminWindow = new MainAdminWindow();
                mainAdminWindow.Show();
            }
            else
            {
                // Navigate to VolunteerListWindow
                VolunteerListWindow volunteerListWindow = new VolunteerListWindow();
                volunteerListWindow.Show();

            }
        }

            // Clear inputs for next login
            UserTypeComboBox.SelectedIndex = -1;
        UserIdTextBox.Clear();
        PasswordBox.Clear();
    }
}
