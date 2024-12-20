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
using BO;

/// <summary>
/// Interaction logic for VolunteerListWindow.xaml
/// </summary>
public partial class VolunteerListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public VolunteerListWindow()
    {
        InitializeComponent();
        this.Loaded += VolunteerListWindow_Loaded;
        this.Closing += VolunteerListWindow_Closing!;
    }

    /// <summary>
    /// dependecy property for the volunteer list 
    /// </summary>
    public IEnumerable<BO.VolunteerInList> VolunteerList
    {
        get { return (IEnumerable<BO.VolunteerInList>)GetValue(VolunteerListProperty); }
        set { SetValue(VolunteerListProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty VolunteerListProperty =
        DependencyProperty.Register("VolunteerList", typeof(IEnumerable<BO.VolunteerInList>), typeof(VolunteerListWindow), new PropertyMetadata(null));


    /// <summary>
    /// the filter for the volunteer list, define by the combo box
    /// </summary>
    public BO.VollInListFilter Filter { get; set; } = BO.VollInListFilter.Id;

    /// <summary>
    /// update the volunteer list view by the filter sorting
    /// </summary>
    private void ComboBox_VolunteerListChange(object sender, SelectionChangedEventArgs e)
    {
        VolunteerList = (sender as ComboBox)!.SelectedIndex switch
        {
            0 => s_bl.Volunteer.ReadAll()!,
            1 => s_bl.Volunteer.ReadAll(null, VollInListFilter.FullName)!,
            2 => s_bl.Volunteer.ReadAll(null, VollInListFilter.Active)!,
            3 => s_bl.Volunteer.ReadAll(null, VollInListFilter.TreatedCalls)!,
            4 => s_bl.Volunteer.ReadAll(null, VollInListFilter.CanceledCalls)!,
            5 => s_bl.Volunteer.ReadAll(null, VollInListFilter.ExpiredCalls)!,
            6 => s_bl.Volunteer.ReadAll(null, VollInListFilter.CorrentCallId)!,
            7 => s_bl.Volunteer.ReadAll(null, VollInListFilter.CorrentCallType)!,
            _ => throw new NotImplementedException(),
        };       
    }

    /// <summary>
    /// observer for the list
    /// </summary>
    private void VolunteerListObserver()
    {
        VolunteerList = s_bl.Volunteer.ReadAll()!;
    }
   

    /// <summary>
    /// open the volunteer window and update the list
    /// </summary>
    private void VolunteerListWindow_Loaded(object sender, RoutedEventArgs e)
    {       
        s_bl.Volunteer.AddObserver(VolunteerListObserver);
        VolunteerList = s_bl.Volunteer.ReadAll()!;
    }

    /// <summary>
    /// remove the observer
    /// </summary>
    private void VolunteerListWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        s_bl.Volunteer.RemoveObserver(VolunteerListObserver);
    }
}


