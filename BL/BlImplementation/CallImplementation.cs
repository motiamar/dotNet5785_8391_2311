namespace BlImplementation;

using System.Collections.Generic;
using BlApi;
using BO;

internal class CallImplementation : ICall
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    public int[] ArrayStatus()
    {
        throw new NotImplementedException();
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

    public Call? Read(int id)
    {
        throw new NotImplementedException();
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
