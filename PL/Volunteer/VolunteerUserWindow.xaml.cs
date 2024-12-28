using BO;
using PL.Admin;
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

    public BO.BRoles Role
    {
        get { return (BO.BRoles)GetValue(RoleProperty); }
        set { SetValue(RoleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Role.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RoleProperty =
        DependencyProperty.Register("Role", typeof(BO.BRoles), typeof(VolunteerUserWindow), new PropertyMetadata(null));

    private void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            s_bl.Volunteer.Update(CurrentVolunteerUser!.Id, CurrentVolunteerUser);
            MessageBox.Show("the Volunteer has updeted sucsesfuly");
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void BtnCallHistory_Click(object sender, RoutedEventArgs e)
    {

    }

    private void BtnChooseCall_Click(object sender, RoutedEventArgs e)
    {

    }

    private void BtnEndCall_Click(object sender, RoutedEventArgs e)
    {

    }

    private void BtnCancaleCall_Click(object sender, RoutedEventArgs e)
    {

    }
}
