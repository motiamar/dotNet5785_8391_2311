using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using PL.Admin;
using PL.Volunteer;
namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// dependecy property for the current ID of the user
    /// </summary>
    public int CurrentID
    {
        get { return (int)GetValue(CurrentIDProperty); }
        set { SetValue(CurrentIDProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentIDProperty =
        DependencyProperty.Register("CurrentID", typeof(int), typeof(MainWindow), new PropertyMetadata(null));


    /// <summary>
    ///  dependecy property for the current password of the user
    /// </summary>
    public string CurrentPassword
    {
        get { return (string)GetValue(CurrentPasswordProperty); }
        set { SetValue(CurrentPasswordProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CurrentPassword.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentPasswordProperty =
        DependencyProperty.Register("CurrentPassword", typeof(string), typeof(MainWindow), new PropertyMetadata(null));




    public BO.Volunteer? CurrentUser
    {
        get { return (BO.Volunteer?)GetValue(CurrentUserProperty); }
        set { SetValue(CurrentUserProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CorrentUser.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentUserProperty =
        DependencyProperty.Register("CorrentUser", typeof(BO.Volunteer), typeof(MainWindow), new PropertyMetadata(null));


    private void BtnLogIn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            CurrentUser = s_bl.Volunteer.Read(CurrentID);
            if (CurrentPassword != CurrentUser!.Password)
            {
                throw new Exception("Password is incorrect");
            }
            switch (CurrentUser.role)
            {
                case BRoles.Volunteer:
                    VolunteerUserWindow VolunteerUserWindow = new();
                    VolunteerUserWindow.Show();
                    break;

                case BRoles.Manager:
                    MessageBoxResult result = MessageBox.Show("Do you want to enter the Manager screen?", "Manager Login", MessageBoxButton.YesNo,  MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        MainAdminWindow mainAdminWindow = new();
                        mainAdminWindow.Show();
                    }
                    else
                    {
                        VolunteerUserWindow VolunteerUserWindow2 = new();
                        VolunteerUserWindow2.Show();
                    }
                    break;

                default:
                    throw new Exception("Role is not defined");
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }


    //private void LoginButton_Click(object sender, RoutedEventArgs e)
    //{
    //    string userType = ((ComboBoxItem)UserTypeComboBox.SelectedItem)?.Content.ToString();
    //    string userId = UserIdTextBox.Text;
    //    string password = PasswordBox.Password;

    //    if (string.IsNullOrEmpty(userType) || string.IsNullOrEmpty(userId))
    //    {
    //        MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    //        return;
    //    }
    //    if (userType == "Volunteer")
    //    {
    //        // Navigate to VolunteerListWindow
    //        VolunteerListWindow volunteerListWindow = new VolunteerListWindow();
    //        volunteerListWindow.Show();
    //    }
    //    else if (userType == "Manager")
    //    {
    //        // Allow manager to choose between Admin or Volunteer list screen
    //        MessageBoxResult result = MessageBox.Show("Do you want to enter the Manager screen?",
    //                                                  "Manager Login",
    //                                                  MessageBoxButton.YesNo,
    //                                                  MessageBoxImage.Question);

    //        if (result == MessageBoxResult.Yes)
    //        {
    //            // Navigate to MainAdminWindow (Manager screen)
    //            MainAdminWindow mainAdminWindow = new MainAdminWindow();
    //            mainAdminWindow.Show();
    //        }
    //        else
    //        {
    //            // Navigate to VolunteerListWindow
    //            VolunteerListWindow volunteerListWindow = new VolunteerListWindow();
    //            volunteerListWindow.Show();

    //        }
    //    }

    //    // Clear inputs for next login
    //    UserTypeComboBox.SelectedIndex = -1;
    //    UserIdTextBox.Clear();
    //    PasswordBox.Clear();
    //}

}