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
        this.DataContext = this; // Ensure binding context is set correctly
    }


    /// <summary>
    /// Dependency property for CurrentID
    /// </summary>
    public int CurrentID
    {
        get { return (int)GetValue(CurrentIDProperty); }
        set { SetValue(CurrentIDProperty, value); }
    }

    public static readonly DependencyProperty CurrentIDProperty =
        DependencyProperty.Register("CurrentID", typeof(int), typeof(MainWindow), new PropertyMetadata(null));


    /// <summary>
    /// Dependency property for CurrentPassword
    /// </summary>
    public string CurrentPassword
    {
        get { return (string)GetValue(CurrentPasswordProperty); }
        set { SetValue(CurrentPasswordProperty, value); }
    }

    public static readonly DependencyProperty CurrentPasswordProperty =
        DependencyProperty.Register("CurrentPassword", typeof(string), typeof(MainWindow), new PropertyMetadata(null));


    /// <summary>
    /// Dependency property for CurrentUser
    /// </summary>
    public BO.Volunteer? CurrentUser
    {
        get { return (BO.Volunteer?)GetValue(CurrentUserProperty); }
        set { SetValue(CurrentUserProperty, value); }
    }

    public static readonly DependencyProperty CurrentUserProperty =
        DependencyProperty.Register("CurrentUser", typeof(BO.Volunteer), typeof(MainWindow), new PropertyMetadata(null));


    /// <summary>
    /// Event handler for password change in PasswordBox
    /// </summary>
    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        // Update CurrentPassword when password changes
        CurrentPassword = ((PasswordBox)sender).Password;
    }


    /// <summary>
    /// Event handler when CheckBox is checked (show password as plain text)
    /// </summary>
    private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        // Show TextBoxPassword and hide PasswordBox
        PasswordBox.Visibility = Visibility.Collapsed;
        TextBoxPassword.Visibility = Visibility.Visible;
        TextBoxPassword.Text = PasswordBox.Password;
    }


    /// <summary>
    /// Event handler when CheckBox is unchecked (hide password as plain text)
    /// </summary>
    private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        // Show PasswordBox and hide TextBoxPassword
        PasswordBox.Visibility = Visibility.Visible;
        TextBoxPassword.Visibility = Visibility.Collapsed;
        PasswordBox.Password = TextBoxPassword.Text;
    }

    /// <summary>
    /// Button click for logging in
    /// </summary>
    private void BtnLogIn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Attempt to get the user from the backend
            CurrentUser = s_bl.Volunteer.Read(CurrentID);

            // Check if the entered password matches
            if (CurrentPassword != CurrentUser!.Password)
            {
                throw new Exception("Password is incorrect");
            }

            // Handle user roles
            switch (CurrentUser.role)
            {
                case BRoles.Volunteer:
                    new VolunteerUserWindow(CurrentID).Show();
                    break;

                case BRoles.Manager:
                    MessageBoxResult result = MessageBox.Show("Do you want to enter the Manager screen?", "Manager Login", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        new MainAdminWindow().Show();

                    }
                    else
                    {
                        new VolunteerUserWindow(CurrentID).Show();
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
}
