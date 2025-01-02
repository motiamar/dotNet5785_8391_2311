using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
/// Interaction logic for MainAdminWindow.xaml
/// </summary>
public partial class MainAdminWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public MainAdminWindow(int managerId)
    {
        ManagerID = managerId;
        InitializeComponent();
        this.Loaded += MainAdminWindowLoaded;
        this.Closing += MainAdminWindow_Closing;
    }
    int ManagerID { get; set; }

    /// <summary>
    /// dependecy property for the clock 
    /// </summary>
    public DateTime CurrentTime
    {
        get { return (DateTime)GetValue(CurrentTimeProperty); }
        set { SetValue(CurrentTimeProperty, value); }
    }

    public static readonly DependencyProperty CurrentTimeProperty =
        DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainAdminWindow));

    /// <summary>
    /// dependecy property for the risk range 
    /// </summary>
    public TimeSpan MaxRange
    {
        get { return (TimeSpan)GetValue(MaxRangeProperty); }
        set { SetValue(MaxRangeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MaxRange.
    public static readonly DependencyProperty MaxRangeProperty =
        DependencyProperty.Register("MaxRange", typeof(TimeSpan), typeof(MainAdminWindow));

    /// <summary>
    /// button to set a new value to the risk range
    /// </summary>
    private void BtnSetMaxRange_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.SetMaxRange(MaxRange);
        MessageBox.Show("The range has been updated");
    }

    /// <summary>
    /// add one minute to the clock
    /// </summary>
    private void BtnAddOneMinute_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ForwordClock(BO.TimeUnit.Minute);
    }

    /// <summary>
    /// add one hour to the clock
    /// </summary>
    private void BtnAddOneHour_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ForwordClock(BO.TimeUnit.Hour);
    }

    /// <summary>
    /// add one day to the clock
    /// </summary>
    private void BtnAddOneDay_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ForwordClock(BO.TimeUnit.Day);
    }

    /// <summary>
    /// add one month to the clock
    /// </summary>
    private void BtnAddOneMonth_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ForwordClock(BO.TimeUnit.Month);

    }
    /// <summary>
    /// add one year to the clock
    /// </summary>
    private void BtnAddOneYear_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ForwordClock(BO.TimeUnit.Year);
    }


    /// <summary>
    /// observer for the clock to update the screen
    /// </summary>
    private void ClockObserver()
    {
        CurrentTime = s_bl.Admin.GetClock();
    }

    private void ConfigObserver()
    {
        MaxRange = s_bl.Admin.GetMaxRange();
    }

    private void CallsObserver()
    {
        Btn_calls_Click();
    }
    /// <summary>
    /// open screen event and initialization
    /// </summary>
    private void MainAdminWindowLoaded(object sendor, RoutedEventArgs e)
    {
        Btn_calls_Click();
        CurrentTime = s_bl.Admin.GetClock();
        MaxRange = s_bl.Admin.GetMaxRange();
        s_bl.Admin.AddClockObserver(ClockObserver);
        s_bl.Admin.AddConfigObserver(configObserver: ConfigObserver);
        s_bl.Call.AddObserver(CallsObserver);
    }

    /// <summary>
    /// close screen event and remove observers
    /// </summary>
    private void MainAdminWindow_Closing(object? sender, CancelEventArgs e)
    {
        s_bl.Admin.RemoveClockObserver(ClockObserver);
        s_bl.Admin.RemoveConfigObserver(ConfigObserver);
        s_bl.Call.RemoveObserver(CallsObserver);
    }

    /// <summary>
    /// botton to open the volunteers screen
    /// </summary>
    private void BtnHandleVolunteers_Click(object sender, RoutedEventArgs e)
    {
        new VolunteerListWindow().Show();
    }

    /// <summary>
    /// botton to open the calls screen
    /// </summary>
    private void BtnHandleCalls_Click(object sender, RoutedEventArgs e)
    {
        new CallListWindow().Show();
    }

    /// <summary>
    /// botton to initionalize the system
    /// </summary>
    private void BtnIntialization_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Are you sure you want to initionliz the system?", "confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window != this)
                    {
                        window.Close();
                    }
                }
                s_bl.Admin.InitializeDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("The initiliz has sucssesfull");
            }
        }
    }

    /// <summary>
    /// botton to reset the system
    /// </summary>
    private void BtnReset_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Are you sure you want to reset the system?", "confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window != this)
                    {
                        window.Close();
                    }
                }
                s_bl.Admin.ResetDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("The reset has sucssesfull");
            }
        }      
    }

    /// <summary>
    /// create buttons for each status of the calls
    /// </summary>
    private void Btn_calls_Click()
    {
        try
        {
            int[] callsStatus = s_bl.Call.ArrayStatus();
            string[] statusNames = { "Open", "In_treatment", "Closed", "Expired", "Open_in_risk", "In_treatment_in_risk" };

            StatusButtonsPanel.Children.Clear(); // clear the panel

            for (int i = 0; i < callsStatus.Length; i++)
            {
                Button statusButton = new Button
                {
                    Content = $"{statusNames[i]} ({callsStatus[i]})", // content of the button
                    Tag = i,
                    Margin = new Thickness(2),
                    Padding = new Thickness(5),
                    Background = Brushes.LightBlue,
                    FontSize = 14
                };
                statusButton.Click += StatusButton_Click;
                StatusButtonsPanel.Children.Add(statusButton);
            }
        }catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
       
    }


    /// <summary>
    /// event to open a new screen with the selected status
    /// </summary>
    private void StatusButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            int status = (int)button.Tag; // return the status selected
            BO.BCallStatus filter = (BO.BCallStatus)status;
            new CallListWindow(filter, ManagerID).Show(); 
        }
    }
}