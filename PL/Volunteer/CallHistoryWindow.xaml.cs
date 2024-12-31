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
        InitializeComponent();
        try
        {
            UserHistoryCalls = s_bl.Call.GetCloseCallList(id);
            UserID = id;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        this.Loaded += CallHistoryWindow_Loaded;
        this.Closed += CallHistoryWindow_Closed!;
    }

    /// <summary>
    /// entity to hold the selected call in the list
    /// </summary>
    public BO.ClosedCallInList? SelectedClosedCallInList { get; set; }

    /// <summary>
    /// user id to filter the call list
    /// </summary>
    public int UserID
    {
        get { return (int)GetValue(UserIDProperty); }
        set { SetValue(UserIDProperty, value); }
    }

    // Using a DependencyProperty as the backing store for UserID.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty UserIDProperty =
        DependencyProperty.Register("UserID", typeof(int), typeof(CallHistoryWindow), new PropertyMetadata(null));


    /// <summary>
    /// close the observer
    /// </summary>
    public void CallHistoryWindow_Closed(object sender, EventArgs e)
    {
        s_bl.Call.RemoveObserver(CallInListObserver);
    }

    /// <summary>
    /// loaded the call list
    /// </summary>
    public void CallHistoryWindow_Loaded(object sender, RoutedEventArgs e)
    {
        s_bl.Call.AddObserver(CallInListObserver);
    }

    /// <summary>
    /// observer for the closed call list of the volunteer
    /// </summary>
    public void CallInListObserver()
    {
        UserHistoryCalls = s_bl.Call.GetCloseCallList(UserID);
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
        DependencyProperty.Register("MyProperty", typeof(IEnumerable<BO.ClosedCallInList>), typeof(CallHistoryWindow), new PropertyMetadata(null));

    /// <summary>
    /// add observer to the call list and load the call list
    /// </summary>
    private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        UserHistoryCalls = (sender as ComboBox)!.SelectedIndex switch
        {
            0 => s_bl.Call.GetCloseCallList(UserID,null,BO.CloseCallInListFilter.Id)!,
            1 => s_bl.Call.GetCloseCallList(UserID, null, BO.CloseCallInListFilter.Type)!,
            2 => s_bl.Call.GetCloseCallList(UserID, null, BO.CloseCallInListFilter.CallAddress)!,
            3 => s_bl.Call.GetCloseCallList(UserID, null, BO.CloseCallInListFilter.CallOpenTime)!,
            4 => s_bl.Call.GetCloseCallList(UserID, null, BO.CloseCallInListFilter.CallEnterTime)!,
            5 => s_bl.Call.GetCloseCallList(UserID, null, BO.CloseCallInListFilter.CallCloseTime)!,
            6 => s_bl.Call.GetCloseCallList(UserID, null, BO.CloseCallInListFilter.EndKind)!,
            _ => throw new NotImplementedException(),
        };
    }

}