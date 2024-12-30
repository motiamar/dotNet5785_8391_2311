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
            UserHistoryCalls = s_bl.Call.Read(id)!;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }


    /// <summary>
    /// dependecy property for the call list
    /// </summary>
    public BO.Call UserHistoryCalls
    {
        get { return (BO.Call)GetValue(UserHistoryCallsProperty); }
        set { SetValue(UserHistoryCallsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for UserHistoryCalls.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty UserHistoryCallsProperty =
        DependencyProperty.Register("UserHistoryCalls", typeof(BO.Call), typeof(CallHistoryWindow), new PropertyMetadata(null));



    /// <summary>
    /// dependecy property for the call list filter
    /// </summary>
    public IEnumerable<ClosedCallInList> CloseCallList
    {
        get { return (IEnumerable<ClosedCallInList>)GetValue(CloseCallListProperty); }
        set { SetValue(CloseCallListProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CloseCallListProperty =
        DependencyProperty.Register("MyProperty", typeof(IEnumerable<ClosedCallInList>), typeof(CallHistoryWindow), new PropertyMetadata(null));

    /// <summary>
    /// add observer to the call list and load the call list
    /// </summary>
    private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        CloseCallList = (sender as ComboBox)!.SelectedIndex switch
        {
            0 => s_bl.Call.GetCloseCallList(UserHistoryCalls.Id, BO.BTypeCalls.Medical_situation, BO.CloseCallInListFilter.Id),
            1 => s_bl.Call.GetCloseCallList(UserHistoryCalls.Id, BO.BTypeCalls.Car_accident, BO.CloseCallInListFilter.Id),
            2 => s_bl.Call.GetCloseCallList(UserHistoryCalls.Id, BO.BTypeCalls.Fall_from_hight, BO.CloseCallInListFilter.Id),
            3 => s_bl.Call.GetCloseCallList(UserHistoryCalls.Id, BO.BTypeCalls.Violent_event, BO.CloseCallInListFilter.Id),
            4 => s_bl.Call.GetCloseCallList(UserHistoryCalls.Id, BO.BTypeCalls.Domestic_violent, BO.CloseCallInListFilter.Id),
            5 => s_bl.Call.GetCloseCallList(UserHistoryCalls.Id, BO.BTypeCalls.None, BO.CloseCallInListFilter.Id),
            _ => throw new NotImplementedException(),
        };
    }
}

