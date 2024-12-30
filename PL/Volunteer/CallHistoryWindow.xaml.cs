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
            UserHistoryCalls = 
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }



    public BO.ClosedCallInList UserHistoryCalls
    {
        get { return (BO.ClosedCallInList)GetValue(UserHistoryCallsProperty); }
        set { SetValue(UserHistoryCallsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for UserHistoryCalls.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty UserHistoryCallsProperty =
        DependencyProperty.Register("UserHistoryCalls", typeof(BO.ClosedCallInList), typeof(CallHistoryWindow), new PropertyMetadata(null));



    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}

