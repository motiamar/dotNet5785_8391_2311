namespace BlImplementation;
using System;
using System.Collections.Generic;
using BlApi;
using BO;
using DalApi;
using DO;
using static BO.Exceptions;

internal class CallImplementation : BlApi.ICall
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// func that return array filed with the amount of calls exist, orederd by compering the index to the enum size
    /// </summary>
    public int[] ArrayStatus()
    {
        try
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
        catch (DO.DalXMLFileLoadCreateException ex)
        {
            throw new BlCantLoadException($"Failed to get call status array: { ex }");
        }    
    }


    /// <summary>
    /// a func to create a filter and sorted list and return the list 
    /// </summary>
    /// <param name="filter"> if it exist, filter the list by the BTypeCalls prameter </param>
    /// <param name="value"> if it exist, filter the list by the value object</param>
    /// <param name="sort"> sorted the list by the BCallStatus prameter if it exist </param>
    public IEnumerable<CallInList> ReadAll(CallInListFilter? sort = null, object? value = null, CallInListFilter? filter = null)
    {
        try
        {
            IEnumerable<CallInList> callsInList = Helpers.CallManager.GetAllCallInList();
            string Filed = sort.ToString()!;
            var property = typeof(BO.CallInList).GetProperty(Filed);
            string Filed2 = filter.ToString()!;
            var property2 = typeof(BO.CallInList).GetProperty(Filed2);
            if (Filed is not null)
                callsInList.Where(c => property!.GetValue(c)! == value);
            return filter == null ? callsInList.OrderBy(c => c.Id) : callsInList.OrderBy(c => property2!.GetValue(c));
        }
        catch (DO.DalXMLFileLoadCreateException ex)
        {
            throw new BlCantLoadException($"Failed to get call list: {ex}");
        }
    }


    /// <summary>
    /// create a BO.Call entity if the id exist in the data base 
    /// </summary>
    /// <param name="id"> get the id of the call</param>
    /// <returns> return the copy of the entity by BO.call entity if it exist</returns>
    public BO.Call? Read(int id)
    {
        try
        {
            var call = _dal.Call.Read(id);

            if (call is null)
                throw new DO.DalDoesNotExistException($"call with the id : {id} doesn't exist");
            return new BO.Call
            {
                Id = call.Id,
                Type = (BTypeCalls)call.TypeCall,
                Description = call.VerbalDecription,
                CallAddress = call.FullAddressOfTheCall,
                Latitude = call.Latitude,
                Longitude = call.Longitude,
                CallOpenTime = call.OpeningCallTime,
                CallMaxCloseTime = call.MaxEndingCallTime,
                CallStatus = Helpers.CallManager.GetStatus(call),
                callAssignInLists = Helpers.CallManager.GetCallAssignInList(id)
            };

        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"Failed to get call with id {id}: {ex}");
        }
        catch (DO.DalXMLFileLoadCreateException ex)
        {
            throw new BlCantLoadException($"Failed to get call with id {id}: {ex}");
        }
    }


    /// <summary>
    /// func to Update an exist call by the Manager or the self call
    /// </summary>
    /// <param name="change">the new entity we want to switch to</param>
    public void Update(BO.Call change)
    {
        try
        {
            Helpers.CallManager.CallChek(change);
            var call = _dal.Call.Read(change.Id);
            if (call is null)
                throw new DO.DalDoesNotExistException($"call with the id : {change.Id} doesn't exist");
            double latitude = Helpers.Tools.GetLatitudeFromAddress(change.CallAddress);
            double longitude = Helpers.Tools.GetLongitudeFromAddress(change.CallAddress);
            DO.Call updateCall = new DO.Call
            {
                Id = change.Id,
                TypeCall = (DO.TypeCalls)change.Type,
                VerbalDecription = change.Description,
                FullAddressOfTheCall = change.CallAddress,
                Latitude = latitude,
                Longitude = longitude,
                OpeningCallTime = change.CallOpenTime,
                MaxEndingCallTime = change.CallMaxCloseTime
            };
            _dal.Call.Update(updateCall);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"Failed to update call with id {change.Id}: {ex}");
        }
    }

    public void Delete(int callId)
    {
        try
        {
            DO.Call call = _dal.Call.Read(callId)!;  להמשיך מפה
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"Failed to delete call with id {callId}: {ex}");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void CanceleAssignment(int volunteerId, int assignmentId)
    {
        throw new NotImplementedException();
    }

    public void ChooseCall(int volunteerId, int CallId)
    {
        throw new NotImplementedException();
    }

    public void Create(BO.Call call)
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

    


    
}
