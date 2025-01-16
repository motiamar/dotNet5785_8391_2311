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
using DO;

namespace PL.Volunteer;

/// <summary>
/// Interaction logic for ChooseCallWindow.xaml
/// </summary>
public partial class ChooseCallWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public ChooseCallWindow(int volunteerId)
    {
        try
        {
            volunteer_id = volunteerId;
            volunteer = s_bl.Volunteer.Read(volunteer_id)!;
            InitializeComponent();
            this.Loaded += ChooseCallWindow_Loaded;
            this.Closed += ChooseCallWindow_Closed!;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// the observer that get called when the call list get updated
    /// </summary>
    private void ChooseCallWindow_Loaded(object sender, RoutedEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.Wait;
        OpenCallInList = s_bl.Call.GetOpenCallList(volunteer_id);
        s_bl.Call.AddObserver(CallInListObserver);
        Mouse.OverrideCursor = null;
    }

    /// <summary>
    /// the observer that get called when the call list get updated
    /// </summary>
    private void ChooseCallWindow_Closed(object sender, EventArgs e)
    {
       s_bl.Call.RemoveObserver( CallInListObserver);
    }

    /// <summary>
    /// the observer that get called when the call list get updated
    /// </summary>
    private void CallInListObserver()
    {
        OpenCallInList = s_bl.Call.GetOpenCallList(volunteer_id, CallTypeFilter, OpenCallInListSort);
    }

    /// <summary>
    /// the volunteer that choose the call
    /// </summary>
    public int volunteer_id { get; set; }


    /// <summary>
    /// the volunteer that choose the call
    /// </summary>
    public BO.Volunteer volunteer
    {
        get { return (BO.Volunteer)GetValue(volunteerProperty); }
        set { SetValue(volunteerProperty, value); }
    }

    // Using a DependencyProperty as the backing store for volunteer.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty volunteerProperty =
        DependencyProperty.Register("volunteer", typeof(BO.Volunteer), typeof(ChooseCallWindow), new PropertyMetadata(null));



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

    public void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            OpenCallInList = s_bl.Call.GetOpenCallList(volunteer_id, CallTypeFilter, OpenCallInListSort);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// the call that the volunteer choose
    /// </summary>

    public BO.OpenCallInList? SelectOpenCallInList
    {
        get { return (BO.OpenCallInList?)GetValue(SelectOpenCallInListProperty); }
        set { SetValue(SelectOpenCallInListProperty, value); }
    }

    // Using a DependencyProperty as the backing store for SelectOpenCallInList.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SelectOpenCallInListProperty =
        DependencyProperty.Register("SelectOpenCallInList", typeof(BO.OpenCallInList), typeof(ChooseCallWindow), new PropertyMetadata(null));



    /// <summary>
    /// button to choose the call
    /// </summary>
    public void BtnChoose_Click(object sender, RoutedEventArgs e)
    {
        SelectOpenCallInList = (sender as Button)!.DataContext as BO.OpenCallInList;
        if (SelectOpenCallInList == null)
        {
            MessageBox.Show("No call selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        try
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var result = MessageBox.Show("Are you sure you want to choose this call?", "Confirm Choose", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                s_bl.Call.ChooseCall(volunteer_id, SelectOpenCallInList.Id);
                MessageBox.Show("Call chosen successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }             
            else
                MessageBox.Show("Choose canceled", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            Mouse.OverrideCursor = null;
        }
        catch (Exception ex)
        {
            Mouse.OverrideCursor = null;
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// button to update the volunteer address
    /// </summary>
    public void BtnUpdateAddress_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Mouse.OverrideCursor = Cursors.Wait;
            s_bl.Volunteer.Update(volunteer_id, volunteer);
            MessageBox.Show("Address updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            Mouse.OverrideCursor = null;
        }
        catch (Exception ex)
        {
            Mouse.OverrideCursor = null;
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}


