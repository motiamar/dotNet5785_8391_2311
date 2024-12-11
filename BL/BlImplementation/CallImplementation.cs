namespace BlImplementation;
using System.Collections.Generic;
using BlApi;
using BO;

internal class CallImplementation : ICall
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// return an array that contain the number of calls in each status
    /// </summary>
    public int[] ArrayStatus()
    {
        var grouped = from call in _dal.Call.ReadAll()
                      group call by call.TypeCall into callGroup
                      select new { Status = callGroup.Key, Count = callGroup.Count() };

        // Initialize an array with the size of the enum
        int[] statusCounts = new int[Enum.GetValues(typeof(BCallStatus)).Length];

        // Populate the array based on the grouped data
        foreach (var item in grouped)
        {
            statusCounts[(int)item.Status] = item.Count;
        }
        return statusCounts;
    }

    public void CanceleAssignment(int volunteerId, int assignmentId)
    {
        throw new NotImplementedException();
    }

    public void ChooseCall(int volunteerId, int CallId)
    {
        throw new NotImplementedException();
    }

    public void Create(Call call)
    {
        throw new NotImplementedException();
    }

    public void Delete(int callId)
    {
        throw new NotImplementedException();
    }

    public void EndAssignment(int volunteerId, int assignmentId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ClosedCallInList> GetCloseCallList(int volunteerId, BTypeCalls? filter = null, CloseCallInListFilter? sort = null)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OpenCallInList> GetOpenCallList(int volunteerId, BTypeCalls? filter = null, OpenCallInListFilter? sort = null)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// return a spasific call with the given id if the call doesn't exist return null
    /// </summary>
    public Call? Read(int id)
    {
        return Helpers.CallManager.GetSpasificCall(id);
    }

    public IEnumerable<CallInList> ReadAll(CallInListFilter? filter = null, object? value = null, CallInListFilter? sort = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Call change)
    {
        throw new NotImplementedException();
    }
}
