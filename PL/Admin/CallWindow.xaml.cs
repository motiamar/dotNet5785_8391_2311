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
/// Interaction logic for CallWindow.xaml
/// </summary>
public partial class CallWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public CallWindow(int callId = 0)
    {
        ButtonText = callId == 0 ? "Add" : "Update";
        InitializeComponent();
        try
        {
            if (callId != 0)
            {
                CorrentCall = s_bl.Call.Read(callId);
            }
            else
            {
                CorrentCall = new BO.Call();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

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
        DependencyProperty.Register("ButtonText", typeof(string), typeof(CallWindow), new PropertyMetadata(null));


    /// <summary>
    /// property to hold the call from the data base
    /// </summary>
    public BO.Call? CorrentCall
    {
        get { return (BO.Call?)GetValue(CorrentCallProperty); }
        set { SetValue(CorrentCallProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CorrentVolunteer.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CorrentCallProperty =
        DependencyProperty.Register("CorrentCall", typeof(BO.Call), typeof(CallWindow), new PropertyMetadata(null));

    /// <summary>
    /// add or update the call depent on the button text
    /// </summary>
    private void BtnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (ButtonText == "Add")
            {
                s_bl.Call.Create(CorrentCall!);
                MessageBox.Show("the call has created sucsesfuly");
                this.Close();
            }
            else
            {
                s_bl.Call.Update(CorrentCall!);
                MessageBox.Show("the call has updeted sucsesfuly");
                this.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// property for the ComboBox to set the type of the call
    /// </summary>
    public BO.BTypeCalls Type
    {
        get { return (BO.BTypeCalls)GetValue(TypeProperty); }
        set { SetValue(TypeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Role.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register("Type", typeof(BO.BTypeCalls), typeof(CallWindow), new PropertyMetadata(null));


    /// <summary>
    /// property for the ComboBox to set the type of the call
    /// </summary>
    public BO.BCallStatus Status
    {
        get { return (BO.BCallStatus)GetValue(StatusProperty); }
        set { SetValue(StatusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Role.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register("Status", typeof(BO.BCallStatus), typeof(CallWindow), new PropertyMetadata(null));
}
