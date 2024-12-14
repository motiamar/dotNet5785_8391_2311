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
        return Helpers.ClockManager.Now;
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
                newClock = ClockManager.Now.AddMinutes(1);
                break;
            case TimeUnit.Hour:
                newClock = ClockManager.Now.AddHours(1);
                break;
            case TimeUnit.Day:
                newClock = ClockManager.Now.AddDays(1);
                break;
            case TimeUnit.Month:
                newClock = ClockManager.Now.AddMonths(1);
                break;
            case TimeUnit.Year:
                newClock = ClockManager.Now.AddYears(1);
                break;  
                default:
                throw new ArgumentOutOfRangeException(nameof(unit), unit, null);
        }
        Helpers.ClockManager.UpdateClock(newClock);
    }

    /// <summary>
    /// return the value of the RiskRange
    /// </summary>
    public TimeSpan GetMaxRange()
    {
        return _dal.Config.RiskRnge;
    }

    /// <summary>
    /// set the RiskRane by new value
    /// </summary>
    public void SetMaxRange(TimeSpan maxRange)
    {
        _dal.Config.RiskRnge = maxRange;
    }

    /// <summary>
    /// Reset all the data base
    /// </summary>
    public void ResetDB()
    {
        _dal.ResetDB();
        ClockManager.UpdateClock(ClockManager.Now);
    }

    /// <summary>
    /// initialize all the data base
    /// </summary>
    public void InitializeDB()
    {
        DalTest.Initialization.Do();
        ClockManager.UpdateClock(ClockManager.Now);
    }
}
