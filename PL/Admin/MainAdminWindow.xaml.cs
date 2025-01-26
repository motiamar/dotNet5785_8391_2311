using BlApi;
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
        simulatorButton = "Start";
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
        UpdateCallStatus();
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
                UpdateCallStatus();
            });
    }
    /// <summary>
    /// open screen event and initialization
    /// </summary>
    private void MainAdminWindowLoaded(object sendor, RoutedEventArgs e)
    {
        UpdateCallStatus();
        CurrentTime = s_bl.Admin.GetClock();
        MaxRange = s_bl.Admin.GetMaxRange();
        s_bl.Admin.AddClockObserver(ClockObserver);
        s_bl.Admin.AddConfigObserver(ConfigObserver);
        s_bl.Call.AddObserver(CallsObserver);
        simulator_flag = false;
    }

    /// <summary>
    /// close screen event and remove observers
    /// </summary>
    private void MainAdminWindow_Closing(object? sender, CancelEventArgs e)
    {
        s_bl.Admin.RemoveClockObserver(ClockObserver);
        s_bl.Admin.RemoveConfigObserver(ConfigObserver);
        s_bl.Call.RemoveObserver(CallsObserver);
        s_bl.Admin.StopSimulator();

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
    /// porperty for the interval of the clock
    /// </summary>
    public int Interval
    {
        get { return (int)GetValue(IntervalProperty); }
        set { SetValue(IntervalProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IntervalProperty =
        DependencyProperty.Register("Interval", typeof(int), typeof(MainAdminWindow), new PropertyMetadata(null));


    /// <summary>
    /// flag to know if the simulator is on
    /// </summary>
    public bool simulator_flag
    {
        get { return (bool)GetValue(simulator_flagProperty); }
        set { SetValue(simulator_flagProperty, value); }
    }

    // Using a DependencyProperty as the backing store for simulator_flag.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty simulator_flagProperty =
        DependencyProperty.Register("simulator_flag", typeof(bool), typeof(MainAdminWindow), new PropertyMetadata(null));

    /// <summary>
    /// button to start or stop the simulator
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnSimulator_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!simulator_flag)
            {
                simulator_flag = true;
                s_bl.Admin.StartSimulator(Interval);
                simulatorButton = "Stop";
            }
            else
            {
                s_bl.Admin.StopSimulator();
                simulator_flag = false;
                simulatorButton = "Start";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }

    /// <summary>
    /// property for the simulator button
    /// </summary>
    public string simulatorButton
    {
        get { return (string)GetValue(simulatorButtonProperty); }
        set { SetValue(simulatorButtonProperty, value); }
    }

    // Using a DependencyProperty as the backing store for simulatorButton.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty simulatorButtonProperty =
        DependencyProperty.Register("simulatorButton", typeof(string), typeof(MainAdminWindow), new PropertyMetadata(null));

    /// <summary>
    /// property for the call status
    /// </summary>
    public int[] Status
    {
        get { return (int[])GetValue(StatusProperty); }
        set { SetValue(StatusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register("Status", typeof(int[]), typeof(MainAdminWindow), new PropertyMetadata(new int[6]));




    /// <summary>
    /// update the call status by the BL layer
    /// </summary>
    public void UpdateCallStatus()
    {
        try
        {
            var status = s_bl.Call.ArrayStatus();
            Status = new int[6] { status[0], status[1], status[2], status[3], status[4], status[5] };

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    /// <summary>
    /// open the call screen filterd by the choosen button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private readonly Dictionary<BO.BCallStatus, CallListWindow> _openWindows = new();
    private void Btn_Calls_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            var tag = button.Tag;
            BO.BCallStatus status;

            // Match the tag to the status
            switch (tag)
            {
                case "Open":
                    status = BO.BCallStatus.Open;
                    break;
                case "In_treatment":
                    status = BO.BCallStatus.In_treatment;
                    break;
                case "Closed":
                    status = BO.BCallStatus.Closed;
                    break;
                case "Expired":
                    status = BO.BCallStatus.Expired;
                    break;
                case "Open_in_risk":
                    status = BO.BCallStatus.Open_in_risk;
                    break;
                case "Treatment_in_risk":
                    status = BO.BCallStatus.In_treatment_in_risk;
                    break;
                default:
                    return;
            }

            // Check if a window for the status already exists
            if (_openWindows.TryGetValue(status, out var existingWindow))
            {
                if (existingWindow.IsVisible)
                {
                    existingWindow.Focus(); 
                    return;
                }
                else
                {
                    _openWindows.Remove(status); 
                }
            }

            // Create and show a new window
            var newWindow = new CallListWindow(status, ManagerID);
            newWindow.Closed += (s, args) => _openWindows.Remove(status); 
            _openWindows[status] = newWindow;
            newWindow.Show();
        }
    }
   
}