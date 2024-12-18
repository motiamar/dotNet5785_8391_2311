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
namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    static readonly BlApi.IBl bl = BlApi.Factory.Get();


    public MainWindow()
    {
        InitializeComponent();
        CurrentTime = bl.Admin.GetClock();
    }


    public DateTime CurrentTime
    {
        get { return (DateTime)GetValue(CurrentTimeProperty); }
        set { SetValue(CurrentTimeProperty, value); }
    }

    public static readonly DependencyProperty CurrentTimeProperty =
        DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow));

    /// <summary>
    /// add one minute to the clock
    /// </summary>
    private void BtnAddOneMinute_Click(object sender, RoutedEventArgs e)
    {
        bl.Admin.ForwordClock(BO.TimeUnit.Minute);
    }

    /// <summary>
    /// add one hour to the clock
    /// </summary>
    private void BtnAddOneHour_Click(object sender, RoutedEventArgs e)
    {
        bl.Admin.ForwordClock(BO.TimeUnit.Hour);
    }

    /// <summary>
    /// add one day to the clock
    /// </summary>
    private void BtnAddOneDay_Click(object sender, RoutedEventArgs e)
    {
        bl.Admin.ForwordClock(BO.TimeUnit.Day);
    }

    /// <summary>
    /// add one month to the clock
    /// </summary>
    private void BtnAddOneMonth_Click(object sender, RoutedEventArgs e)
    {
        bl.Admin.ForwordClock(BO.TimeUnit.Month);

    }
    /// <summary>
    /// add one year to the clock
    /// </summary>
    private void BtnAddOneYear_Click(object sender, RoutedEventArgs e)
    {
        bl.Admin.ForwordClock(BO.TimeUnit.Year);
    }



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
        bl.Admin.SetMaxRange(MaxRange);
    }
}