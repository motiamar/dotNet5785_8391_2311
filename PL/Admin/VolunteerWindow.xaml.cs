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

namespace PL.Admin;

/// <summary>
/// Interaction logic for VolunteerWindow.xaml
/// </summary>
public partial class VolunteerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public VolunteerWindow(int id = 0)
    {
        ButtonText = id == 0 ? "Add" : "Update";
        InitializeComponent();
        try
        {
            if (id != 0)
            {
                CorrentVolunteer = s_bl.Volunteer.Read(id);
            }
            else
            {
                CorrentVolunteer = new BO.Volunteer();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }



    /// <summary>
    /// property for the ComboBox to set the role of the volunteer
    /// </summary>
    public BO.BRoles Role
    {
        get { return (BO.BRoles)GetValue(RoleProperty); }
        set { SetValue(RoleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Role.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RoleProperty =
        DependencyProperty.Register("Role", typeof(BO.BRoles), typeof(VolunteerWindow), new PropertyMetadata(null));


    /// <summary>
    /// property for the add / update button  
    /// </summary>
    public string ButtonText
    {
        get { return (string)GetValue(ButtonTextProperty); }
        set { SetValue(ButtonTextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ButtonText.  This enables animation, styling, binding, .
    public static readonly DependencyProperty ButtonTextProperty =
        DependencyProperty.Register("ButtonText", typeof(string), typeof(VolunteerWindow), new PropertyMetadata(null));


    /// <summary>
    /// property to hold the volunteer from the data base
    /// </summary>
    public BO.Volunteer? CorrentVolunteer
    {
        get { return (BO.Volunteer?)GetValue(CorrentVolunteerProperty); }
        set { SetValue(CorrentVolunteerProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CorrentVolunteer.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CorrentVolunteerProperty =
        DependencyProperty.Register("CorrentVolunteer", typeof(BO.Volunteer), typeof(VolunteerWindow), new PropertyMetadata(null));


    /// <summary>
    /// add or update the volunteer depent on the button text
    /// </summary>
    private void BtnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (ButtonText == "Add")
            {
                s_bl.Volunteer.Create(CorrentVolunteer!);
                MessageBox.Show("the Volunteer has created sucsesfuly");
                this.Close();
            }
            else
            {
                s_bl.Volunteer.Update(CorrentVolunteer!.Id, CorrentVolunteer);
                MessageBox.Show("the Volunteer has updeted sucsesfuly");
                this.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
