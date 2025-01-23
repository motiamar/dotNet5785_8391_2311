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
using System.Windows.Threading;

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
    public BO.BTypeCalls? filter
    {
        get { return (BO.BTypeCalls)GetValue(FilterProperty); }
        set { SetValue(FilterProperty, value); }
    }
    public static readonly DependencyProperty FilterProperty =
        DependencyProperty.Register("filter", typeof(BO.BTypeCalls), typeof(VolunteerListWindow), new PropertyMetadata(null));

    public BO.VollInListFilter? sort
    {
        get { return (BO.VollInListFilter)GetValue(sortProperty); }
        set { SetValue(sortProperty, value); }
    }
    public static readonly DependencyProperty sortProperty =
        DependencyProperty.Register("sort", typeof(BO.VollInListFilter), typeof(VolunteerListWindow), new PropertyMetadata(null));


    /// <summary>
    /// the filter for the volunteer list, to filter by the call type always
    /// </summary>
    public BO.VollInListFilter? filter2 = VollInListFilter.CorrentCallType;

    /// <summary>
    /// update the volunteer list view by the filter sorting
    /// </summary>
    public void ComboBox_VolunteerListChange(object sender, SelectionChangedEventArgs e)
    {
        VolunteerList = s_bl.Volunteer.ReadAllScreen(sort, filter2, filter)!;
    }

    /// <summary>
    /// observer for the list
    /// </summary>
    private volatile DispatcherOperation? _observerOperation = null; //stage 7
    private void VolunteerListObserver()
    {
        if (_observerOperation is null || _observerOperation.Status == DispatcherOperationStatus.Completed)
            _observerOperation = Dispatcher.BeginInvoke(() =>
            {
                VolunteerList = s_bl.Volunteer.ReadAllScreen(sort, filter2, filter)!;
            });
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

    /// <summary>
    /// entity to hold the selected volunteer in the list
    /// </summary>
    public BO.VolunteerInList? SelectedVolunteerInList { get; set; }

    /// <summary>
    /// button to add a new volunteer
    /// </summary>
    public void BtnAddVolunteer_Click(object sender, RoutedEventArgs e)
    {
        new VolunteerWindow().Show();
    }

    /// <summary>
    /// open the volunteer window when double click on the list in update mode
    /// </summary>
    public void SelectedVolunteerInList_MouseDouble_Click(object sender, MouseButtonEventArgs e)
    {
        if (SelectedVolunteerInList != null)
        {
            new VolunteerWindow(SelectedVolunteerInList.Id).Show();
        }
    }

    /// <summary>
    /// delete the selected volunteer from the list
    /// </summary>
    public void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        SelectedVolunteerInList = (sender as Button)!.DataContext as BO.VolunteerInList;
        if (SelectedVolunteerInList == null)
        {
            MessageBox.Show("No Volunteer selected", "Information",MessageBoxButton.OK,MessageBoxImage.Information);
            return;
        }
        try
        {
            var result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                s_bl.Volunteer.Delete(SelectedVolunteerInList!.Id);
            }
            else
            {
                MessageBox.Show("Delete canceled", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }

}


