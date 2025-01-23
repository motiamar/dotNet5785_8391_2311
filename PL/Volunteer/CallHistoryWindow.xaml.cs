using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
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
using BO;
namespace PL.Volunteer;

/// <summary>
/// Interaction logic for CallHistoryWindow.xaml
/// </summary>
public partial class CallHistoryWindow : Window
{

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public CallHistoryWindow(int id = 0)
    { 
        UserID = id;
        InitializeComponent();
        UserHistoryCalls = s_bl.Call.GetCloseCallList(UserID, CallTypeFilter, CallInListSort);
        this.Loaded += CallHistoryWindow_Loaded;
        this.Closed += CallHistoryWindow_Closed!;
    }

    /// <summary>
    /// the id of the user that the call list belong to
    /// </summary>
    public int UserID { get; set; }


    /// <summary>
    /// sort the call list by the given parameter
    /// </summary>
    public BO.CloseCallInListFilter? CallInListSort
    {
        get { return (BO.CloseCallInListFilter?)GetValue(CallInListSortProperty); }
        set { SetValue(CallInListSortProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CallInListSort.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CallInListSortProperty =
        DependencyProperty.Register("CallInListSort", typeof(BO.CloseCallInListFilter?), typeof(CallHistoryWindow), new PropertyMetadata(null));



    /// <summary>
    /// Filter the call list by the call type
    /// </summary>
    public BO.BTypeCalls? CallTypeFilter
    {
        get { return (BO.BTypeCalls?)GetValue(CallTypeFilterProperty); }
        set { SetValue(CallTypeFilterProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CallTypeFilter.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CallTypeFilterProperty =
    DependencyProperty.Register("CallTypeFilter", typeof(BO.BTypeCalls?), typeof(CallHistoryWindow), new PropertyMetadata(null));




    /// <summary>
    /// loaded the call list
    /// </summary>
    private void CallHistoryWindow_Loaded(object sender, RoutedEventArgs e)
    {
        UserHistoryCalls = s_bl.Call.GetCloseCallList(UserID);
        s_bl.Call.AddObserver(CallInListObserver);
    }


    /// <summary>
    /// close the observer
    /// </summary>
    private void CallHistoryWindow_Closed(object sender, EventArgs e)
    {
        s_bl.Call.RemoveObserver(CallInListObserver);
    }

    /// <summary>
    /// observer for the closed call list of the volunteer
    /// </summary>
    private volatile DispatcherOperation? _observerOperation = null; //stage 7
    private void CallInListObserver()
    {
        if (_observerOperation is null || _observerOperation.Status == DispatcherOperationStatus.Completed)
            _observerOperation = Dispatcher.BeginInvoke(() =>
            {
                UserHistoryCalls = s_bl.Call.GetCloseCallList(UserID, CallTypeFilter, CallInListSort);

            });
    }


    /// <summary>
    /// dependecy property for the call list filter
    /// </summary>
    public IEnumerable<BO.ClosedCallInList> UserHistoryCalls
    {
        get { return (IEnumerable<BO.ClosedCallInList>)GetValue(UserHistoryCallsProperty); }
        set { SetValue(UserHistoryCallsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty UserHistoryCallsProperty =
        DependencyProperty.Register("UserHistoryCalls", typeof(IEnumerable<BO.ClosedCallInList>), typeof(CallHistoryWindow), new PropertyMetadata(null));

    /// <summary>
    /// add observer to the call list and load the call list
    /// </summary>
    public void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            UserHistoryCalls = s_bl.Call.GetCloseCallList(UserID, CallTypeFilter, CallInListSort);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}