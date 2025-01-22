using System.Runtime.CompilerServices;
using BlImplementation;
using BO;
using DalApi;
namespace Helpers;

/// <summary>
/// Internal BL manager for all Application's Clock logic policies
/// </summary>
internal static class AdminManager 
{
    #region Stage 4
    private static readonly DalApi.IDal s_dal = DalApi.Factory.Get; 
    #endregion Stage 4

    #region Stage 5
    internal static event Action? ConfigUpdatedObservers; 
    internal static event Action? ClockUpdatedObservers; 
    #endregion Stage 5

    #region Stage 4
    /// <summary>
    /// Property for providing/setting current configuration variable value for any BL class that may need it
    /// </summary>
    internal static TimeSpan MaxRange
    {
        get => s_dal.Config.RiskRnge;
        set
        {
            s_dal.Config.RiskRnge = value;
            ConfigUpdatedObservers?.Invoke(); 
        }
    }

    /// <summary>
    /// Property for providing current application's clock value for any BL class that may need it
    /// </summary>
    internal static DateTime Now { get => s_dal.Config.Clock; } 

    /// <summary>
    /// Method to reset the DB
    /// </summary>
    internal static void ResetDB() 
    {
        lock (BlMutex) 
        {
            s_dal.ResetDB();
            AdminManager.UpdateClock(AdminManager.Now); //needed since we want the label on Pl to be updated
            AdminManager.MaxRange = AdminManager.MaxRange; // needed to update PL 
        }
    }

    /// <summary>
    /// Method to initialize the DB
    /// </summary>
    internal static void InitializeDB()
    {
        lock (BlMutex) 
        {
            DalTest.Initialization.Do();
            AdminManager.UpdateClock(AdminManager.Now);  // needed since we want the label on Pl to be updated
            AdminManager.MaxRange = AdminManager.MaxRange; // needed for update the PL 
        }
    }


    /// <summary>
    /// Method to perform application's clock from any BL class as may be required
    /// </summary>
    /// <param name="newClock">updated clock value</param>
    internal static void UpdateClock(DateTime newClock) //stage 4-7
    {
        // new Thread(() => { // stage 7 - not sure - still under investigation - see stage 7 instructions after it will be released
        updateClock(newClock);
        // }).Start(); // stage 7 as above
    }

    /// <summary>
    /// task for periodic update of the calls
    /// </summary>
    private static Task? _periodicTask = null;

    /// <summary>
    /// Method to update the clock
    /// </summary>
    private static void updateClock(DateTime newClock) 
    {
        s_dal.Config.Clock = newClock;
        if (_periodicTask is null || _periodicTask.IsCompleted) // make sure that the task - Update calls - is running asincronously
            _periodicTask = Task.Run(() => Helpers.CallManager.UpdateCalls()); 
        ClockUpdatedObservers?.Invoke();
    }
    #endregion Stage 4

    #region Stage 7 base
    /// <summary>    
    /// Mutex to use from BL methods to get mutual exclusion while the simulator is running
    /// </summary>
    internal static readonly object BlMutex = new object(); // BlMutex = s_dal; // This field is actually the same as s_dal - it is defined for readability of locks

    /// <summary>
    /// The thread of the simulator
    /// </summary>
    private static volatile Thread? s_thread;

    /// <summary>
    /// The Interval for clock updating
    /// in minutes by second (default value is 1, will be set on Start())    
    /// </summary>
    private static int s_interval = 1;

    /// <summary>
    /// The flag that signs whether simulator is running
    /// 
    private static volatile bool s_stop = false;

    /// <summary>
    /// Mutex for mutual exclusion while the simulator is running
    /// </summary>
    private static readonly object mutex = new();

    /// <summary>
    /// Method to throw an exception if the simulator is running
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)] //stage 7                                                 
    public static void ThrowOnSimulatorIsRunning()
    {
        if (s_thread is not null)
            throw new BO.Exceptions.BLTemporaryNotAvailableException("Cannot perform the operation since Simulator is running");
    }

    /// <summary>
    /// method to start the simulator
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)] //stage 7   
    internal static void Start(int interval)
    {
        lock (mutex)
            if (s_thread == null)
            {
                s_interval = interval;
                s_stop = false;
                s_thread = new Thread(clockRunner);
                s_thread.Start();
            }
    }

    /// <summary>
    /// maethod to stop the simulator
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)] //stage 7    
    internal static void Stop()
    {
        if (s_thread is not null)
        {
            s_stop = true;
            s_thread.Interrupt(); //awake a sleeping thread
            s_thread.Name = "ClockRunner stopped";
            s_thread = null;
        }
    }

    /// <summary>
    /// The task of the simulation
    /// </summary>
    private static Task? _simulateTask = null;

    /// <summary>
    /// The method that runs the clock
    /// </summary>
    private static void clockRunner()
    {
        while (!s_stop)
        {
            UpdateClock(Now.AddMinutes(s_interval));
            if (_simulateTask is null || _simulateTask.IsCompleted)//stage 7
                _simulateTask = Task.Run(() => Helpers.VolunteerManager.SimulateVolunteerActivity());
            try
            {
                Thread.Sleep(1000); // 1 second
            }
            catch (ThreadInterruptedException) { }
        }
    }
    #endregion Stage 7 base
}