namespace BlImplementation;
using System;
using BlApi;
using BO;

internal class AdminImplementation : IAdmin
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    public void ForwordClock(TimeUnit unit)
    {
        throw new NotImplementedException();
    }

    public DateTime GetClock()
    {
        throw new NotImplementedException();
    }

    public int GetMaxRange()
    {
        throw new NotImplementedException();
    }

    public void InitializeDB()
    {
        throw new NotImplementedException();
    }

    public void ResetDB()
    {
        throw new NotImplementedException();
    }

    public void SetMaxRange(TimeSpan maxRange)
    {
        throw new NotImplementedException();
    }
}
