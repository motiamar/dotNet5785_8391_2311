using BO;
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
/// Interaction logic for CallListWindow.xaml
/// </summary>
public partial class CallListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public CallListWindow(BO.BCallStatus filter = 0)
    {
        StatusFilter = filter;
        InitializeComponent();
        this.Loaded += CallListWindow_Loaded;
    }

    /// <summary>
    /// dependecy property for the call list filter
    /// </summary>
    BO.BCallStatus StatusFilter { get; set; }

    /// <summary>
    /// dependecy property for the call list 
    /// </summary>
    public IEnumerable<BO.CallInList> CallList
    {
        get { return (IEnumerable<BO.CallInList>)GetValue(CallListProperty); }
        set { SetValue(CallListProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CallListProperty =
        DependencyProperty.Register("CallList", typeof(IEnumerable<BO.CallInList>), typeof(CallListWindow), new PropertyMetadata(null));

    /// <summary>
    /// entity to hold the selected call in the list
    /// </summary>
    public BO.CallInList? SelectedCallInList { get; set; }

    /// <summary>
    /// add observer to the call list and load the call list
    /// </summary>
    private void CallListWindow_Loaded(object sender, RoutedEventArgs e)
    {
        s_bl.Call.AddObserver(CallInListObserver);
        CallList = s_bl.Call.ReadAll(BO.CallInListFilter.CallStatus, StatusFilter, null)!;
    }

    /// <summary>
    /// remove observer from the call list
    /// </summary>
    private void CallListWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        s_bl.Call.RemoveObserver(CallInListObserver);
    }

    /// <summary>
    /// update the call list view by observer
    /// </summary>
    private void CallInListObserver()
    {
        CallList = s_bl.Call.ReadAll()!;
    }

    /// <summary>
    /// the sort for the call list, define by the combo box
    /// </summary>
    public BO.CallInListFilter sort { get; set; } = BO.CallInListFilter.Id;

    private void ComboBox_CallInListChange(object sender, SelectionChangedEventArgs e)
    {
        CallList = (sender as ComboBox)!.SelectedIndex switch
        {
            0 => s_bl.Call.ReadAll(BO.CallInListFilter.CallStatus, StatusFilter, BO.CallInListFilter.Id)!,
            1 => s_bl.Call.ReadAll(BO.CallInListFilter.CallStatus, StatusFilter, BO.CallInListFilter.CallId)!,
            2 => s_bl.Call.ReadAll(BO.CallInListFilter.CallStatus, StatusFilter, BO.CallInListFilter.Type)!,
            3 => s_bl.Call.ReadAll(BO.CallInListFilter.CallStatus, StatusFilter, BO.CallInListFilter.CallOpenTime)!,
            4 => s_bl.Call.ReadAll(BO.CallInListFilter.CallStatus, StatusFilter, BO.CallInListFilter.CallMaxCloseTime)!,
            5 => s_bl.Call.ReadAll(BO.CallInListFilter.CallStatus, StatusFilter, BO.CallInListFilter.LastVolunteerName)!,
            6 => s_bl.Call.ReadAll(BO.CallInListFilter.CallStatus, StatusFilter, BO.CallInListFilter.TotalTreatmentTime)!,
            7 => s_bl.Call.ReadAll(BO.CallInListFilter.CallStatus, StatusFilter, BO.CallInListFilter.CallStatus)!,
            8 => s_bl.Call.ReadAll(BO.CallInListFilter.CallStatus, StatusFilter, BO.CallInListFilter.SumOfAssignments)!,
            _ => throw new NotImplementedException(),
        };
    }


    /// <summary>
    /// open the volunteer window when double click on the list in update mode
    /// </summary>
    private void SelectedCallInList_MouseDouble_Click(object sender, MouseButtonEventArgs e)
    {
        if (SelectedCallInList != null)
        {
            new CallWindow(SelectedCallInList.CallId).Show();
        }
    }


    /// <summary>
    /// button to add a new volunteer
    /// </summary>
    private void BtnAddCall_Click(object sender, RoutedEventArgs e)
    {
        new CallWindow().Show();
    } 


    /// <summary>
    /// delete the selected volunteer from the list
    /// </summary>
    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        SelectedCallInList = (sender as Button)!.DataContext as BO.CallInList;
        if (SelectedCallInList == null)
        {
            MessageBox.Show("No Volunteer selected");
            return;
        }
        try
        {
            var result = MessageBox.Show("Are you sure you want to delete this call?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                s_bl.Call.Delete(SelectedCallInList!.CallId);
            }
            else
            {
                MessageBox.Show("Delete canceled");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void BtnEndAss_Click(object sender, RoutedEventArgs e)
    {

    }
}

 

   