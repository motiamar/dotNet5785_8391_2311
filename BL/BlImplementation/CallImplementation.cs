namespace BlImplementation;
using System;
using System.Collections.Generic;
using BlApi;
using BO;
using DalApi;
using DO;
using Newtonsoft.Json.Linq;
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
                return null;
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

    /// <summary>
    /// Delete the call if it exist and Open or never assignt 
    /// </summary>
    public void Delete(int callId)
    {
        try
        {
            DO.Call call = _dal.Call.Read(callId)!; 
            if (call is null)   
                throw new DO.DalDoesNotExistException($"call with id {callId} does not exist");
            if (Helpers.CallManager.GetStatus(call) == BCallStatus.Open)
                _dal.Call.Delete(callId);
            else
                throw new BlNotAllowException($"Failed to delete call with id {callId}: call is not open");
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"Failed to delete call with id {callId}: {ex}");
        }
    }

    /// <summary>
    /// Add a new call to the data leir if the details are correct
    /// </summary>
    public void Create(BO.Call call)
    {
        try
        {
            Helpers.CallManager.CallChek(call);
            double latitude = Helpers.Tools.GetLatitudeFromAddress(call.CallAddress);
            double longitude = Helpers.Tools.GetLongitudeFromAddress(call.CallAddress);
            DO.Call newCall = new DO.Call
            {
                TypeCall = (DO.TypeCalls)call.Type,
                VerbalDecription = call.Description,
                FullAddressOfTheCall = call.CallAddress,
                Latitude = latitude,
                Longitude = longitude,
                OpeningCallTime = call.CallOpenTime,
                MaxEndingCallTime = call.CallMaxCloseTime
            };
            _dal.Call.Create(newCall);
        }
        catch(DO.DalXMLFileLoadCreateException)
        {
            throw new BlCantLoadException("Failed to create call");
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BlinCorrectException($"Failed to create call: {ex}");
        }
    }

    /// <summary>
    /// func to create a new list by the recived parameters
    /// </summary>
    /// <param name="volunteerId"> the id of the Volunteer</param>
    /// <param name="filter"> the kind of the call that the list filter by</param>
    /// <param name="sort">the kind of the call filed that the list sorted by </param>
    /// <returns>return the new list</returns>
    public IEnumerable<ClosedCallInList> GetCloseCallList(int volunteerId, BTypeCalls? sort = null, CloseCallInListFilter? filter = null)
    {
        try
        {
            IEnumerable<ClosedCallInList> closedCallsInList = Helpers.CallManager.GetClosedCallInLists(volunteerId);
            string Filed = filter.ToString()!;
            var property = typeof(BO.ClosedCallInList).GetProperty(Filed);
            if(sort is not null)
                closedCallsInList.Where(c => c.Type == sort);
            return filter == null ? closedCallsInList.OrderBy(c => c.Id) : closedCallsInList.OrderBy(c => property!.GetValue(c));
        }
        catch (DO.DalXMLFileLoadCreateException ex)
        {
            throw new BlCantLoadException($"Failed to get closed call list: {ex}");
        }
    }

    /// <summary>
    /// func that return a sorted list of the calls in list, sorted by filter Booleany and a enum fild 
    /// </summary>
    /// <param name="volunteerId"> the id of the Volunteer</param>
    /// <param name="filter"> filterd the list by the given parameter</param>
    /// <param name="sort"> soreted the list by the given parameter</param>
    /// <returns>return the new list</returns>
    public IEnumerable<OpenCallInList> GetOpenCallList(int volunteerId, BTypeCalls? sort = null, OpenCallInListFilter? filter = null)
    {
        try
        {
            DO.Volunteer volunteer = _dal.Volunteer.Read(volunteerId)!;
            IEnumerable<OpenCallInList> openCallsInList = Helpers.CallManager.GetOpenCallInLists(volunteer);
            if (sort is not null)
                openCallsInList.Where((c) => c.Type == sort);
            string Filed = filter.ToString()!;
            var property = typeof(BO.OpenCallInList).GetProperty(Filed);
            return filter == null ? openCallsInList.OrderBy(c => c.Id) : openCallsInList.OrderBy(c => property!.GetValue(c));
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"Failed to get open call list: {ex}");
        }
        catch(DO.DalXMLFileLoadCreateException ex)
        {
            throw new BlCantLoadException($"Failed to get open call list: {ex}");
        }
    }


    /// <summary>
    /// chek if exist and end the assignment of the Volunteer
    /// </summary>
    /// <param name="volunteerId"> the Volunteer who want to end the assignemnt</param>
    /// <param name="assignmentId"> the assignment that need to close</param>
    public void EndAssignment(int volunteerId, int assignmentId)
    {
        try
        {
            DO.Assignment assignment = _dal.Assignment.Read(assignmentId)!;
            if (assignment is null)
                throw new DO.DalDoesNotExistException($"assignment with id {assignmentId} does not exist");
            if(assignment.VolunteerId !=  volunteerId)
                throw new BlNotAllowException("Failed to end assignment: assignment does not belong to the volunteer");
            if(assignment.FinishTime != null)
                throw new BlNotAllowException("Failed to end assignment: assignment already ended");
            DO.Assignment updateAssignment = new DO.Assignment
            {
                Id = assignment.Id,
                CallId = assignment.CallId,
                VolunteerId = assignment.VolunteerId,
                StartTime = assignment.StartTime,
                FinishTime = Helpers.ClockManager.Now,
                EndKind = DO.EndKinds.Treated
            };
            _dal.Assignment.Update(updateAssignment);   
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"Failed to end assignment with id {assignmentId}: {ex}");
        }
    }


    /// <summary>
    /// func to cancale an assignment by the Volunteer who handle it or the managar if the conditions are good
    /// </summary>
    /// <param name="volunteerId">the Volunteer who want to cencele the assignemnt</param>
    /// <param name="assignmentId">the assignment that need to cancaled</param>
    public void CanceleAssignment(int volunteerId, int assignmentId)
    {
        try
        {
            DO.Assignment assignment = _dal.Assignment.Read(assignmentId)!;
            if (assignment is null)
                throw new DO.DalDoesNotExistException($"assignment with id {assignmentId} does not exist");
            DO.Volunteer volunteer = _dal.Volunteer.Read(volunteerId)!;
            string role = Helpers.VolunteerManager.GetVolunteerRole(volunteer.FullName, volunteer.Password!)!;          
            if (assignment.VolunteerId != volunteerId && role != "Manager")
                throw new BlNotAllowException($"the assignment is not belong to volunteer with id: {volunteerId} or he dosent a manager");
            if (assignment.FinishTime != null)
                throw new BlNotAllowException("Failed to cancel assignment: assignment already ended");
            var update = new DO.Assignment
            {
                Id = assignment.Id,
                CallId = assignment.CallId,
                VolunteerId = assignment.VolunteerId,
                StartTime = assignment.StartTime,
                FinishTime = Helpers.ClockManager.Now,
                EndKind = role == "Manager" ? EndKinds.Administrator_cancellation : EndKinds.Self_cancellation,
            };
            _dal.Assignment.Update(update);        
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"Failed to cancel assignment with id {assignmentId}: {ex}");
        }
    }


    /// <summary>
    /// func that assignet Volunteer to a call if it available and not taken 
    /// </summary>
    /// <param name="volunteerId">the volunteer who want to handle the call</param>
    /// <param name="CallId">the call he want to choose</param>
    public void ChooseCall(int volunteerId, int CallId)
    {
        try
        {
            DO.Call call = _dal.Call.Read(CallId)!;
            if (call is null)
                throw new DO.DalDoesNotExistException($"call with id: {CallId} does not exist");
            Helpers.CallManager.ChooseCallChek(call);
            DO.Assignment assignment = new DO.Assignment
            {
                CallId = CallId,
                VolunteerId = volunteerId,
                StartTime = Helpers.ClockManager.Now
            };
            _dal.Assignment.Create(assignment);
        }
        catch (DO.DalAlreadyExistException)
        {
            throw new BlinCorrectException("Failed to choose call: assignment already exists");
        }
    }   
}
