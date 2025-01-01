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
using BO;

namespace PL.Volunteer;

/// <summary>
/// Interaction logic for ChooseCallWindow.xaml
/// </summary>
public partial class ChooseCallWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public ChooseCallWindow(BO.Volunteer volunteer)
    {
        InitializeComponent();
        volunteer_1 = volunteer;
        this.Loaded += ChooseCallWindow_Loaded;
        this.Closed += ChooseCallWindow_Closed!;
    }

    /// <summary>
    /// the observer that get called when the call list get updated
    /// </summary>
    public void ChooseCallWindow_Loaded(object sender, RoutedEventArgs e)
    {
        OpenCallInList = s_bl.Call.GetOpenCallList(volunteer_1.Id);
        s_bl.Call.AddObserver(volunteer_1.Id,CallInListObserver);
    }

    /// <summary>
    /// the observer that get called when the call list get updated
    /// </summary>
    public void ChooseCallWindow_Closed(object sender, EventArgs e)
    {
       s_bl.Call.RemoveObserver(volunteer_1.Id, CallInListObserver);
    }

    /// <summary>
    /// the observer that get called when the call list get updated
    /// </summary>
    public void CallInListObserver()
    {
        OpenCallInList = s_bl.Call.GetOpenCallList(volunteer_1.Id, CallTypeFilter, OpenCallInListSort);
    }

    /// <summary>
    /// the volunteer that choose the call
    /// </summary>
    public BO.Volunteer volunteer_1
    {
        get { return (BO.Volunteer)GetValue(volunteer_1Property); }
        set { SetValue(volunteer_1Property, value); }
    }

    // Using a DependencyProperty as the backing store for volunteer_1.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty volunteer_1Property =
        DependencyProperty.Register("volunteer_1", typeof(BO.Volunteer), typeof(ChooseCallWindow), new PropertyMetadata(null));


    /// <summary>
    /// sort the call list by the given parameter
    /// </summary>
    public BO.OpenCallInListFilter? OpenCallInListSort
    {
        get { return (BO.OpenCallInListFilter?)GetValue(OpenCallInListSortProperty); }
        set { SetValue(OpenCallInListSortProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CallInListSort.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OpenCallInListSortProperty =
        DependencyProperty.Register("OpenCallInListSort", typeof(BO.OpenCallInListFilter?), typeof(ChooseCallWindow), new PropertyMetadata(null));

    /// <summary>
    /// filter the call list by the call type
    /// </summary>
    public BO.BTypeCalls? CallTypeFilter
    {
        get { return (BO.BTypeCalls?)GetValue(CallTypeFilterProperty); }
        set { SetValue(CallTypeFilterProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CallTypeFilter.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CallTypeFilterProperty =
        DependencyProperty.Register("CallTypeFilter", typeof(BO.BTypeCalls?), typeof(ChooseCallWindow), new PropertyMetadata(null));

    /// <summary>
    /// the list of the open calls in the system
    /// </summary>
    public IEnumerable<BO.OpenCallInList> OpenCallInList
    {
        get { return (IEnumerable<BO.OpenCallInList>)GetValue(OpenCallInListProperty); }
        set { SetValue(OpenCallInListProperty, value); }
    }
    
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OpenCallInListProperty =
        DependencyProperty.Register("OpenCallInList", typeof(IEnumerable<BO.OpenCallInList>), typeof(ChooseCallWindow), new PropertyMetadata(null));

    private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            OpenCallInList = s_bl.Call.GetOpenCallList(volunteer_1.Id, CallTypeFilter, OpenCallInListSort);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// the call that the volunteer choose
    /// </summary>
    public BO.OpenCallInList? SelectOpenCallInList { get; set; }=null;
    /// <summary>
    /// button to choose the call
    /// </summary>
    private void BtnChoose_Click(object sender, RoutedEventArgs e)
    {
        SelectOpenCallInList = (sender as Button)!.DataContext as BO.OpenCallInList;
        if (SelectOpenCallInList == null)
        {
            MessageBox.Show("No call selected");
            return;
        }
        try
        {
            var result = MessageBox.Show("Are you sure you want to choose this call?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
                s_bl.Call.ChooseCall(volunteer_1.Id, SelectOpenCallInList.Id);
            else
                MessageBox.Show("Choose canceled");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// text box to show the call description
    /// </summary>
    private void TextBox_TextDescription(object sender, TextChangedEventArgs e)
    {

    }
}


