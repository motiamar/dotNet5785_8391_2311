using BO;
using PL.Admin;
using System;
using System.Collections.Generic;
using System.Globalization;
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
namespace PL.Volunteer;

/// <summary>
/// Interaction logic for VolunteerUserWindow.xaml
/// </summary>
public partial class VolunteerUserWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public VolunteerUserWindow(int id = 0)
    {

        InitializeComponent();
        try
        {
            CurrentVolunteerUser = s_bl.Volunteer.Read(id)!;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        this.Loaded += VolunteerUserWindow_Loaded;
        this.Closing += VolunteerUserWindow_Closing!;
    }


    /// <summary>
    /// screen loaded
    /// </summary>
    public void VolunteerUserWindow_Loaded(object sender, RoutedEventArgs e) 
    {
        s_bl.Volunteer.AddObserver(CurrentVolunteerUser.Id, VolunteerUserObserver);
    }

    /// <summary>
    /// screen closing
    /// </summary>
    public void VolunteerUserWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        s_bl.Volunteer.RemoveObserver(CurrentVolunteerUser.Id, VolunteerUserObserver);
    }

    /// <summary>
    /// observer for the current volunteer
    /// </summary>
    public void VolunteerUserObserver()
    {
        CurrentVolunteerUser = s_bl.Volunteer.Read(CurrentVolunteerUser.Id)!;
    }

    /// <summary>
    /// dependeci object for the current volunteer
    /// </summary>
    public BO.Volunteer CurrentVolunteerUser
    {
        get { return (BO.Volunteer)GetValue(CurrentVolunteerUserProperty); }
        set { SetValue(CurrentVolunteerUserProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CurrentVolunteer.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentVolunteerUserProperty =
        DependencyProperty.Register("CurrentVolunteerUser", typeof(BO.Volunteer), typeof(VolunteerUserWindow), new PropertyMetadata(null));
    /// <summary>
    /// dependeci object for the current role
    /// </summary>
    public BO.BRoles Role
    {
        get { return (BO.BRoles)GetValue(RoleProperty); }
        set { SetValue(RoleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Role.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RoleProperty =
        DependencyProperty.Register("Role", typeof(BO.BRoles), typeof(VolunteerUserWindow), new PropertyMetadata(null));

    /// <summary>
    /// button for the update the volunteer
    /// </summary>
    private void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            s_bl.Volunteer.Update(CurrentVolunteerUser!.Id, CurrentVolunteerUser);
            MessageBox.Show("the Volunteer has updeted sucsesfuly");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// button to show the call history
    /// </summary>
    private void BtnCallHistory_Click(object sender, RoutedEventArgs e)
    {
        new CallHistoryWindow(CurrentVolunteerUser.Id).Show();
    }

    /// <summary>
    /// button to choose a call
    /// </summary>
    private void BtnChooseCall_Click(object sender, RoutedEventArgs e)
    {
        new ChooseCallWindow(CurrentVolunteerUser).Show();
    }

    /// <summary>
    /// button to end the call
    /// </summary>
    private void BtnEndCall_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.Call call = s_bl.Call.Read(CurrentVolunteerUser.CorrentCall!.Id)!;
            if(call == null)
            {
                MessageBox.Show("There is no call in progress");
                return;
            }
            s_bl.Call.EndAssignment(CurrentVolunteerUser.Id, CurrentVolunteerUser.CorrentCall!.Id);
            MessageBox.Show("The call has ended sucssesfuly");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);

        }
    }

    /// <summary>
    /// button to cancel the call
    /// </summary>
    private void BtnCancaleCall_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.Call call = s_bl.Call.Read(CurrentVolunteerUser.CorrentCall!.Id)!;
            if (call == null)
            {
                MessageBox.Show("There is no call in progress");
                return;
            }
            s_bl.Call.CanceleAssignment(CurrentVolunteerUser.Id, CurrentVolunteerUser.CorrentCall!.Id);
            MessageBox.Show("The call has canceled sucssesfuly");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}

