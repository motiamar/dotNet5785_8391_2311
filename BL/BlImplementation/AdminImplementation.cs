namespace BlImplementation;
using System;
using BlApi;
using BO;
using DalApi;
using Helpers;

internal class AdminImplementation : IAdmin
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// return the corrent clock system
    /// </summary>
    public DateTime GetClock()
    {
        return Helpers.AdminManager.Now;
    }

    /// <summary>
    /// advance the clock bt the unit time that given
    /// </summary>
    public void ForwordClock(TimeUnit unit)
    {
        DateTime newClock;
        switch (unit)
        {
            case TimeUnit.Minute:
                newClock = AdminManager.Now.AddMinutes(1);
                break;
            case TimeUnit.Hour:
                newClock = AdminManager.Now.AddHours(1);
                break;
            case TimeUnit.Day:
                newClock = AdminManager.Now.AddDays(1);
                break;
            case TimeUnit.Month:
                newClock = AdminManager.Now.AddMonths(1);
                break;
            case TimeUnit.Year:
                newClock = AdminManager.Now.AddYears(1);
                break;  
                default:
                throw new ArgumentOutOfRangeException(nameof(unit), unit, null);
        }
        Helpers.AdminManager.UpdateClock(newClock);
    }

    /// <summary>
    /// return the value of the RiskRange
    /// </summary>
    public TimeSpan GetMaxRange()
    {
        return Helpers.AdminManager.MaxRange;
    }

    /// <summary>
    /// set the RiskRane by new value
    /// </summary>
    public void SetMaxRange(TimeSpan maxRange)
    {
        Helpers.AdminManager.MaxRange = maxRange;
    }

    /// <summary>
    /// Reset all the data base
    /// </summary>
    public void ResetDB()
    {
        _dal.ResetDB();
        AdminManager.UpdateClock(AdminManager.Now);
        AdminManager.MaxRange = AdminManager.MaxRange;
    }

    /// <summary>
    /// initialize all the data base
    /// </summary>
    public void InitializeDB()
    {
        DalTest.Initialization.Do();
        AdminManager.UpdateClock(AdminManager.Now);
        AdminManager.MaxRange = AdminManager.MaxRange;
    }

    /// <summary>
    /// FUNC to add and remove observer to the config and ckock update event
    /// </summary>
    public void AddConfigObserver(Action configObserver)=> AdminManager.ConfigUpdatedObservers += configObserver;

    public void RemoveConfigObserver(Action configObserver) => AdminManager.ConfigUpdatedObservers -= configObserver;

    public void AddClockObserver(Action clockObserver) => AdminManager.ClockUpdatedObservers += clockObserver;

    public void RemoveClockObserver(Action clockObserver) => AdminManager.ClockUpdatedObservers -= clockObserver;
}
