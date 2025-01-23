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
using System.Windows.Threading;
namespace PL.Admin;

/// <summary>
/// Interaction logic for CallListWindow.xaml
/// </summary>
public partial class CallListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public CallListWindow(BO.BCallStatus filter = 0 ,int managerId = 0)
    {
        ManagerId = managerId;
        StatusFilter = filter;
        InitializeComponent();
        this.Loaded += CallListWindow_Loaded;
        this.Closing += CallListWindow_Closing!;
    }

    private int ManagerId { get; set; }
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
    private volatile DispatcherOperation? _observerOperation = null; //stage 7
    private void CallInListObserver()
    {
        if (_observerOperation is null || _observerOperation.Status == DispatcherOperationStatus.Completed)
            _observerOperation = Dispatcher.BeginInvoke(() =>
            {
                CallList = s_bl.Call.ReadAll(BO.CallInListFilter.CallStatus, StatusFilter, null)!;
            });        
    }

    /// <summary>
    /// the sort for the call list, define by the combo box
    /// </summary>


    public BO.CallInListFilter sort
    {
        get { return (BO.CallInListFilter)GetValue(sortProperty); }
        set { SetValue(sortProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty sortProperty =
        DependencyProperty.Register("sort", typeof(BO.CallInListFilter), typeof(CallListWindow), new PropertyMetadata(null));

    public void ComboBox_CallInListChange(object sender, SelectionChangedEventArgs e)
    {
        CallList = s_bl.Call.ReadAll(BO.CallInListFilter.CallStatus, StatusFilter, sort);       
    }


    /// <summary>
    /// open the volunteer window when double click on the list in update mode
    /// </summary>
    public void SelectedCallInList_MouseDouble_Click(object sender, MouseButtonEventArgs e)
    {
        if (SelectedCallInList != null)
        {
            new CallWindow(SelectedCallInList.CallId).Show();
        }
    }


    /// <summary>
    /// button to add a new volunteer
    /// </summary>
    public void BtnAddCall_Click(object sender, RoutedEventArgs e)
    {
        new CallWindow().Show();
    }


    /// <summary>
    /// delete the selected volunteer from the list
    /// </summary>
    public void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        SelectedCallInList = (sender as Button)!.DataContext as BO.CallInList;
        if (SelectedCallInList == null)
        {
            MessageBox.Show("No call selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("Delete canceled", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }

    /// <summary>
    /// button to cancale the assignment of the call
    /// </summary>
    public void BtnCancaleAss_Click(object sender, RoutedEventArgs e)
    {
        SelectedCallInList = (sender as Button)!.DataContext as BO.CallInList;
        if (SelectedCallInList == null)
        {
            MessageBox.Show("No call selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        try
        {
            var result = MessageBox.Show("Are you sure you want to cancale the assignmebt to this call?", "Confirm End", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                int id = SelectedCallInList.Id ?? 0;
                s_bl.Call.CanceleAssignment(ManagerId, id);
            }
            else
            {
                MessageBox.Show("the cancle canceled", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

 

   