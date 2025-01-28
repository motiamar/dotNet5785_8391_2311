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
        flag = "Hidden";
    }



    public string flag
    {
        get { return (string)GetValue(flagProperty); }
        set { SetValue(flagProperty, value); }
    }

    // Using a DependencyProperty as the backing store for flag.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty flagProperty =
        DependencyProperty.Register("flag", typeof(string), typeof(MainWindow), new PropertyMetadata(null));


    /// <summary>
    /// Dependency property for CurrentID
    /// </summary>
    private int CurrentID
    {
        get { return (int)GetValue(CurrentIDProperty); }
        set { SetValue(CurrentIDProperty, value); }
    }

    private static readonly DependencyProperty CurrentIDProperty =
        DependencyProperty.Register("CurrentID", typeof(int), typeof(MainWindow), new PropertyMetadata(null));


    /// <summary>
    /// Dependency property for CurrentPassword
    /// </summary>
    private string CurrentPassword
    {
        get { return (string)GetValue(CurrentPasswordProperty); }
        set { SetValue(CurrentPasswordProperty, value); }
    }

    private static readonly DependencyProperty CurrentPasswordProperty =
        DependencyProperty.Register("CurrentPassword", typeof(string), typeof(MainWindow), new PropertyMetadata(null));


    /// <summary>
    /// Dependency property for CurrentUser
    /// </summary>
    private BO.Volunteer? CurrentUser
    {
        get { return (BO.Volunteer?)GetValue(CurrentUserProperty); }
        set { SetValue(CurrentUserProperty, value); }
    }

    private static readonly DependencyProperty CurrentUserProperty =
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
                    flag = "Visible";
                    break;

                default:
                    throw new Exception("Role is not defined");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    /// <summary>
    /// Dependency property for chek
    /// </summary>
    public string chek
    {
        get { return (string)GetValue(chekProperty); }
        set { SetValue(chekProperty, value); }
    }

    // Using a DependencyProperty as the backing store for chek.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty chekProperty =
        DependencyProperty.Register("chek", typeof(string), typeof(MainWindow), new PropertyMetadata(null));

    /// <summary>
    /// Event handler for checkbox checked to hide the passwordBox end show the textBox
    /// </summary>
    private void CheckBox_Checked(object sender, RoutedEventArgs e)
    {
        chek = "Hidden";
    }

    /// <summary>
    /// Event handler for checkbox unchecked to show the passwordBox end hide the textBox
    /// </summary>
    private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        chek = "Visible";
    }

    private void Btn_manager_login_Click(object sender, RoutedEventArgs e)
    {
        new MainAdminWindow(CurrentID).Show();
        flag = "Hidden";
    }

    private void Btn_volunteer_login_Click(object sender, RoutedEventArgs e)
    {
        new VolunteerUserWindow(CurrentID).Show();
        flag = "Hidden";
    }
};

