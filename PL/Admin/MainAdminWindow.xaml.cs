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
using System.Windows.Threading;
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
    public void BtnSetMaxRange_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.SetMaxRange(MaxRange);
        Btn_calls_Click();
        MessageBox.Show("The range has been updated", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    /// <summary>
    /// add one minute to the clock
    /// </summary>
    public void BtnAddOneMinute_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ForwordClock(BO.TimeUnit.Minute);
    }

    /// <summary>
    /// add one hour to the clock
    /// </summary>
    public void BtnAddOneHour_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ForwordClock(BO.TimeUnit.Hour);
    }

    /// <summary>
    /// add one day to the clock
    /// </summary>
    public void BtnAddOneDay_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ForwordClock(BO.TimeUnit.Day);
    }

    /// <summary>
    /// add one month to the clock
    /// </summary>
    public void BtnAddOneMonth_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ForwordClock(BO.TimeUnit.Month);

    }
    /// <summary>
    /// add one year to the clock
    /// </summary>
    public void BtnAddOneYear_Click(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.ForwordClock(BO.TimeUnit.Year);
    }


    /// <summary>
    /// observer for the clock to update the screen
    /// </summary>
    private volatile DispatcherOperation? _observerOperation = null; //stage 7
    private void ClockObserver()
    {
        if (_observerOperation is null || _observerOperation.Status == DispatcherOperationStatus.Completed)
            _observerOperation = Dispatcher.BeginInvoke(() =>
            {
                CurrentTime = s_bl.Admin.GetClock();
            });
    }

    private volatile DispatcherOperation? _observerOperation2 = null; //stage 7
    private void ConfigObserver()
    {
        if (_observerOperation2 is null || _observerOperation2.Status == DispatcherOperationStatus.Completed)
            _observerOperation2 = Dispatcher.BeginInvoke(() =>
            {
                MaxRange = s_bl.Admin.GetMaxRange();
            });
    }

    private volatile DispatcherOperation? _observerOperation3 = null; //stage 7
    private void CallsObserver()
    {
        if (_observerOperation3 is null || _observerOperation3.Status == DispatcherOperationStatus.Completed)
            _observerOperation3 = Dispatcher.BeginInvoke(() =>
            {
                Btn_calls_Click();
            });
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
        s_bl.Admin.AddConfigObserver(ConfigObserver);
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
    /// botton to open the volunteers screen only once at in a time
    /// </summary>
    public int _volunteerListWindowCounter = 0;

    public void BtnHandleVolunteers_Click(object sender, RoutedEventArgs e)
    {
        if (_volunteerListWindowCounter == 0)
        {
            var volunteerListWindow = new VolunteerListWindow();
            _volunteerListWindowCounter++;
            volunteerListWindow.Closed += (s, args) => _volunteerListWindowCounter--;
            volunteerListWindow.Show();
        }
        else
        {
            MessageBox.Show("The Volunteers List window is already open.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }


    /// <summary>
    /// botton to initionalize the system
    /// </summary>
    public void BtnIntialization_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Are you sure you want to initionliz the system?", "confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window != this && window.GetType() != typeof(MainWindow))
                    {
                        window.Close();
                    }
                }
                s_bl.Admin.InitializeDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("The initiliz has sucssesfull", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
    }

    /// <summary>
    /// botton to reset the system
    /// </summary>
    public void BtnReset_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Are you sure you want to reset the system?", "confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window != this && window.GetType() != typeof(MainWindow))
                    {
                        window.Close();
                    }
                }
                s_bl.Admin.ResetDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                Mouse.OverrideCursor = null; 
                MessageBox.Show("The reset has sucssesfull", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }      
    }

    /// <summary>
    /// create buttons for each status of the calls
    /// </summary>
    public void Btn_calls_Click()
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
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }


    /// <summary>
    /// event to open a new screen with the selected status only once at in a time
    /// </summary>
    public readonly Dictionary<BO.BCallStatus, CallListWindow> _openWindows = new();

    public void StatusButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            int status = (int)button.Tag; // Extract the status
            BO.BCallStatus filter = (BO.BCallStatus)status;
            if (_openWindows.TryGetValue(filter, out var existingWindow))
            {
                if (existingWindow.IsVisible)
                {
                    existingWindow.Focus(); // give focus to the existing window
                    return;
                }
                else
                {
                    _openWindows.Remove(filter); 
                }
            }
            // create a new window
            var newWindow = new CallListWindow(filter, ManagerID);
            newWindow.Closed += (s, args) => _openWindows.Remove(filter); 
            _openWindows[filter] = newWindow;
            newWindow.Show();
        }
    }
}