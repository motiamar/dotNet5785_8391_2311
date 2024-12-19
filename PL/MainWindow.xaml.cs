using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.Loaded += MainWindowLoaded;
        this.Closing += MainWindow_Closing;


    }

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
   

    /// <summary>
    /// dependecy property for the clock 
    /// </summary>
    public DateTime CurrentTime
    {
        get { return (DateTime)GetValue(CurrentTimeProperty); }
        set { SetValue(CurrentTimeProperty, value); }
    }

    public static readonly DependencyProperty CurrentTimeProperty =
        DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow));

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
        DependencyProperty.Register("MaxRange", typeof(TimeSpan), typeof(MainWindow));


    private void BtnSetMaxRange(object sender, RoutedEventArgs e)
    {
        s_bl.Admin.SetMaxRange(MaxRange);
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

    /// <summary>
    /// open screen event and initialization
    /// </summary>
    private void MainWindowLoaded(object sendor, RoutedEventArgs e)
    {
        CurrentTime = s_bl.Admin.GetClock();
        MaxRange = s_bl.Admin.GetMaxRange();
        s_bl.Admin.AddClockObserver(ClockObserver);
        s_bl.Admin.AddConfigObserver(ConfigObserver);
    }

    private void MainWindow_Closing(object? sender, CancelEventArgs e)
    {
        s_bl.Admin.RemoveClockObserver(ClockObserver);
        s_bl.Admin.RemoveConfigObserver(ConfigObserver);
    }
    

    
}