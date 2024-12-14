namespace BlApi;

public interface IAdmin
{
    /// <summary>
    /// return the corrent clock system
    /// </summary>
    DateTime GetClock();

    /// <summary>
    /// advance the clock bt the unit time that given
    /// </summary>
    void ForwordClock (BO.TimeUnit unit);
    
    /// <summary>
    /// return the value of the RiskRange
    /// </summary>
    TimeSpan GetMaxRange();

    /// <summary>
    /// set the RiskRane by new value
    /// </summary>
    void SetMaxRange(TimeSpan maxRange);

    /// <summary>
    /// Reset all the data base
    /// </summary>
    void ResetDB();

    /// <summary>
    /// initialize all the data base
    /// </summary>
    void InitializeDB();
}
