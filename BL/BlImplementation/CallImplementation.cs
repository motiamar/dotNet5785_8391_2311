namespace BlImplementation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using BlApi;
using BO;
using DalApi;
using DO;
using Helpers;
using static BO.Exceptions;

internal class CallImplementation : BlApi.ICall
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// func that return array filed with the amount of calls exist, orederd by compering the index to the enum size
    /// </summary>
    public int[] ArrayStatus()
    {
        AdminManager.ThrowOnSimulatorIsRunning();  //stage 7
        try
        {
            var calls = Helpers.CallManager.GetAllCallInList();
            var grouped = from call in calls
                          group call by call.CallStatus into callGroup
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
    public IEnumerable<CallInList> ReadAll(CallInListFilter? filter = null, object? value = null, CallInListFilter? sort = null)
    {
        try
        {
            IEnumerable<CallInList> callsInList = Helpers.CallManager.GetAllCallInList();
            if (filter != null && value != null)
            {
                switch (filter)
                {
                    case CallInListFilter.Id:
                        if(value is int id)
                            callsInList = callsInList.Where(c => c.Id == id);
                        break;
                    case CallInListFilter.CallId:
                        if(value is int callId)
                            callsInList = callsInList.Where(c => c.CallId == callId);
                        break;
                    case CallInListFilter.Type:
                        if(value is BTypeCalls type)
                            callsInList = callsInList.Where(c => c.Type == type);
                        break;
                    case CallInListFilter.CallOpenTime:
                        if (value is DateTime time)
                                callsInList = callsInList.Where(c => c.CallOpenTime == time);
                        break;
                    case CallInListFilter.CallMaxCloseTime:
                        if (value is TimeSpan timeSpan)
                                callsInList = callsInList.Where(c => c.CallLeftTime == timeSpan);
                        break;
                    case CallInListFilter.LastVolunteerName:
                        if (value is string name)
                            callsInList = callsInList.Where(c => c.LastVolunteerName == name);
                        break;
                        case CallInListFilter.TotalTreatmentTime:
                        if (value is TimeSpan timeSpan2)
                                callsInList = callsInList.Where(c => c.TotalTreatmentTime == timeSpan2);
                        break;
                    case CallInListFilter.CallStatus:
                        if (value is BCallStatus status)
                            callsInList = callsInList.Where(c => c.CallStatus == status);
                        break;
                    case CallInListFilter.SumOfAssignments:
                        if (value is int sum)
                                callsInList = callsInList.Where(c => c.SumOfAssignments == sum);
                        break;
                }
            }
            if (sort != null)
            {
                switch (sort)
                {
                    case CallInListFilter.Id:
                        callsInList = callsInList.OrderBy(c => c.Id);
                        break;
                    case CallInListFilter.CallId:
                        callsInList = callsInList.OrderBy(c => c.CallId);
                        break;
                    case CallInListFilter.Type:
                        callsInList = callsInList.OrderBy(c => c.Type);
                        break;
                    case CallInListFilter.CallOpenTime:
                        callsInList = callsInList.OrderBy(c => c.CallOpenTime);
                        break;
                    case CallInListFilter.CallMaxCloseTime:
                        callsInList = callsInList.OrderBy(c => c.CallLeftTime);
                        break;
                    case CallInListFilter.LastVolunteerName:
                        callsInList = callsInList.OrderBy(c => c.LastVolunteerName);
                        break;
                    case CallInListFilter.TotalTreatmentTime:
                        callsInList = callsInList.OrderBy(c => c.TotalTreatmentTime);
                        break;
                    case CallInListFilter.CallStatus:
                        callsInList = callsInList.OrderBy(c => c.CallStatus);
                        break;
                    case CallInListFilter.SumOfAssignments:
                        callsInList = callsInList.OrderBy(c => c.SumOfAssignments);
                        break;
                        default:
                        callsInList = callsInList.OrderBy(c => c.Id);
                        break;
                }
            }
            return callsInList;
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
            DO.Call? call; 
            lock (AdminManager.BlMutex)
                call = _dal.Call.Read(id);

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
        AdminManager.ThrowOnSimulatorIsRunning();  //stage 7
        try
        {
            Helpers.CallManager.CallChek(change);

            DO.Call? call; 
            lock (AdminManager.BlMutex)
                call = _dal.Call.Read(change.Id);

            if (call is null)
                throw new DO.DalDoesNotExistException($"call with the id : {change.Id} doesn't exist");
            (double latitude, double longitude) = Helpers.Tools.GetCoordinatesFromAddress(change.CallAddress);
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

            lock (AdminManager.BlMutex)
                _dal.Call.Update(updateCall);

            CallManager.Observers.NotifyItemUpdated(change.Id);
            CallManager.Observers.NotifyListUpdated();
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
        AdminManager.ThrowOnSimulatorIsRunning();  //stage 7
        try
        {
            DO.Call call;
            lock (AdminManager.BlMutex)
                 call = _dal.Call.Read(callId)!; 

            if (call is null)   
                throw new DO.DalDoesNotExistException($"call with id {callId} does not exist");
            if (Helpers.CallManager.GetStatus(call) == BCallStatus.Open)
            {
                lock (AdminManager.BlMutex)
                    _dal.Call.Delete(callId);

                CallManager.Observers.NotifyListUpdated();
            }
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
        AdminManager.ThrowOnSimulatorIsRunning();  //stage 7
        try
        {
            Helpers.CallManager.CallChek(call);
            (double latitude, double longitude) = Helpers.Tools.GetCoordinatesFromAddress(call.CallAddress);
            DO.Call newCall = new DO.Call
            {
                TypeCall = (DO.TypeCalls)call.Type,
                VerbalDecription = call.Description,
                FullAddressOfTheCall = call.CallAddress,
                Latitude = latitude,
                Longitude = longitude,
                OpeningCallTime = AdminManager.Now,
                MaxEndingCallTime = call.CallMaxCloseTime
            };

            lock (AdminManager.BlMutex)
                _dal.Call.Create(newCall);

            CallManager.Observers.NotifyListUpdated();
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
    public IEnumerable<ClosedCallInList> GetCloseCallList(int volunteerId, BTypeCalls? filter = null, CloseCallInListFilter? sort = null)
    {
        try
        {
            lock (AdminManager.BlMutex)
                if (_dal.Volunteer.Read(volunteerId) is null)
                   throw new DO.DalDoesNotExistException($"volunteer with id {volunteerId} does not exist");

            IEnumerable<ClosedCallInList> closedCallsInList = Helpers.CallManager.GetClosedCallInLists(volunteerId);
            if (filter is not null)
            {
                switch (filter)
                {
                    case BTypeCalls.Medical_situation:
                        closedCallsInList = closedCallsInList.Where(c => c.Type == BTypeCalls.Medical_situation);
                        break;
                    case BTypeCalls.Car_accident:
                        closedCallsInList = closedCallsInList.Where(c => c.Type == BTypeCalls.Car_accident);
                        break;
                    case BTypeCalls.Fall_from_hight:
                        closedCallsInList = closedCallsInList.Where(c => c.Type == BTypeCalls.Fall_from_hight);
                        break;
                    case BTypeCalls.Violent_event:
                        closedCallsInList = closedCallsInList.Where(c => c.Type == BTypeCalls.Violent_event);
                        break;
                    case BTypeCalls.Domestic_violent:
                        closedCallsInList = closedCallsInList.Where(c => c.Type == BTypeCalls.Domestic_violent);
                        break;
                }
            }
            if (sort is not null)
            {
                switch (sort)
                {
                    case CloseCallInListFilter.Id:
                        closedCallsInList = closedCallsInList.OrderBy(c => c.Id);
                        break;
                    case CloseCallInListFilter.Type:
                        closedCallsInList = closedCallsInList.OrderBy(c => c.Type);
                        break;
                    case CloseCallInListFilter.CallAddress:
                        closedCallsInList = closedCallsInList.OrderBy(c => c.CallAddress);
                        break;
                    case CloseCallInListFilter.CallOpenTime:
                        closedCallsInList = closedCallsInList.OrderBy(c => c.CallOpenTime);
                        break;
                    case CloseCallInListFilter.CallEnterTime:
                        closedCallsInList = closedCallsInList.OrderBy(c => c.CallEnterTime);
                        break;
                    case CloseCallInListFilter.CallCloseTime:
                        closedCallsInList = closedCallsInList.OrderBy(c => c.CallCloseTime);
                        break;
                    case CloseCallInListFilter.EndKind:
                        closedCallsInList = closedCallsInList.OrderBy(c => c.EndKind);
                        break;
                }
            }
            else
                closedCallsInList = closedCallsInList.OrderBy(c => c.Id);
            return closedCallsInList;
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
    public IEnumerable<OpenCallInList> GetOpenCallList(int volunteerId, BTypeCalls? filter = null, OpenCallInListFilter? sort = null)
    {
        try
        {
            lock (AdminManager.BlMutex)
                if (_dal.Volunteer.Read(volunteerId) is null)
                    throw new DO.DalDoesNotExistException($"volunteer with id {volunteerId} does not exist");
            
            DO.Volunteer volunteer;

            lock (AdminManager.BlMutex)
                volunteer = _dal.Volunteer.Read(volunteerId)!;

            IEnumerable<OpenCallInList> openCallsInList = Helpers.CallManager.GetOpenCallInLists(volunteer);
            if (filter is not null)
            {
                switch (filter)
                {
                    case BTypeCalls.Medical_situation:
                        openCallsInList = openCallsInList.Where(c => c.Type == BTypeCalls.Medical_situation);
                        break;
                    case BTypeCalls.Car_accident:
                        openCallsInList = openCallsInList.Where(c => c.Type == BTypeCalls.Car_accident);
                        break;
                    case BTypeCalls.Fall_from_hight:
                        openCallsInList = openCallsInList.Where(c => c.Type == BTypeCalls.Fall_from_hight);
                        break;
                    case BTypeCalls.Violent_event:
                        openCallsInList = openCallsInList.Where(c => c.Type == BTypeCalls.Violent_event);
                        break;
                    case BTypeCalls.Domestic_violent:
                        openCallsInList = openCallsInList.Where(c => c.Type == BTypeCalls.Domestic_violent);
                        break;
                }
            }
            if (sort is not null)
            {
                switch (sort)
                {
                    case OpenCallInListFilter.Id:
                        openCallsInList = openCallsInList.OrderBy(c => c.Id);
                        break;
                    case OpenCallInListFilter.Type:
                        openCallsInList = openCallsInList.OrderBy(c => c.Type);
                        break;
                    case OpenCallInListFilter.Description:
                        openCallsInList = openCallsInList.OrderBy(c => c.Description);
                        break;
                    case OpenCallInListFilter.CallAddress:
                        openCallsInList = openCallsInList.OrderBy(c => c.CallAddress);
                        break;
                    case OpenCallInListFilter.CallOpenTime:
                        openCallsInList = openCallsInList.OrderBy(c => c.CallOpenTime);
                        break;
                    case OpenCallInListFilter.CallMaxCloseTime:
                        openCallsInList = openCallsInList.OrderBy(c => c.CallMaxCloseTime);
                        break;
                    case OpenCallInListFilter.CallDistance:
                        openCallsInList = openCallsInList.OrderBy(c => c.CallDistance);
                        break;
                }
            }
            else
                openCallsInList = openCallsInList.OrderBy(c => c.Id);
            return openCallsInList;
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"Failed to get open call list: {ex}");
        }
        catch (DO.DalXMLFileLoadCreateException ex)
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
        AdminManager.ThrowOnSimulatorIsRunning();  //stage 7
        try
        {
            DO.Assignment assignment;

            lock (AdminManager.BlMutex)
                 assignment = _dal.Assignment.Read(assignmentId)!;

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
                FinishTime = Helpers.AdminManager.Now,
                EndKind = DO.EndKinds.Treated
            };

            lock (AdminManager.BlMutex)
                _dal.Assignment.Update(updateAssignment);   

            CallManager.Observers.NotifyItemUpdated(assignment.CallId);
            CallManager.Observers.NotifyListUpdated();
            VolunteerManager.Observers.NotifyItemUpdated(volunteerId);
            VolunteerManager.Observers.NotifyListUpdated();
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
        AdminManager.ThrowOnSimulatorIsRunning();  //stage 7
        try
        {
            DO.Assignment assignment;
            lock (AdminManager.BlMutex)
                 assignment = _dal.Assignment.Read(assignmentId)!;

            if (assignment is null)
                throw new DO.DalDoesNotExistException($"assignment with id {assignmentId} does not exist");

            DO.Volunteer volunteer;
            lock (AdminManager.BlMutex)
                 volunteer = _dal.Volunteer.Read(volunteerId)!;

            if (volunteer is null)
                throw new DO.DalDoesNotExistException($"volunteer with id {volunteerId} does not exist");
            string? role = Helpers.VolunteerManager.GetVolunteerRole(volunteer.FullName, volunteer.Password!)!;   
            if(role is null)
                throw new BlDoesNotExistException($"Failed to cancel assignment with id {assignmentId}: volunteer with id {volunteerId} does not exist");
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
                FinishTime = Helpers.AdminManager.Now,
                EndKind = role == "Manager" ? EndKinds.Administrator_cancellation : EndKinds.Self_cancellation,
            };

            lock (AdminManager.BlMutex)
                _dal.Assignment.Update(update);

            CallManager.Observers.NotifyItemUpdated(assignment.CallId);
            CallManager.Observers.NotifyListUpdated();
            VolunteerManager.Observers.NotifyItemUpdated(volunteerId);
            VolunteerManager.Observers.NotifyListUpdated();
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
        AdminManager.ThrowOnSimulatorIsRunning();  //stage 7
        try
        {
            DO.Call call;
            lock (AdminManager.BlMutex)
                 call = _dal.Call.Read(CallId)!;

            if (call is null)
                throw new DO.DalDoesNotExistException($"call with id: {CallId} does not exist");

            DO.Volunteer volunteer;
            lock (AdminManager.BlMutex)
                 volunteer = _dal.Volunteer.Read(volunteerId)!;

            if(volunteer is null)
                throw new DO.DalDoesNotExistException($"Volunteer with id: {volunteerId} does not exist");
            Helpers.CallManager.ChooseCallChek(call);
            DO.Assignment assignment = new DO.Assignment
            {
                CallId = CallId,
                VolunteerId = volunteerId,
                StartTime = Helpers.AdminManager.Now,
                FinishTime = null,
                EndKind = EndKinds.Treated
            };

            lock (AdminManager.BlMutex)
                _dal.Assignment.Create(assignment);

            CallManager.Observers.NotifyItemUpdated(CallId);
            CallManager.Observers.NotifyListUpdated();
            VolunteerManager.Observers.NotifyItemUpdated(volunteerId);
            VolunteerManager.Observers.NotifyListUpdated();
        }
        catch (DO.DalAlreadyExistException)
        {
            throw new BlinCorrectException("Failed to choose call: assignment already exists");
        }
    }

    public void AddObserver(Action listObserver)=> CallManager.Observers.AddListObserver( listObserver);

    public void AddObserver(int id, Action observer) => CallManager.Observers.AddObserver(id, observer);

    public void RemoveObserver(Action listObserver) => CallManager.Observers.RemoveListObserver(listObserver);

    public void RemoveObserver(int id, Action observer) => CallManager.Observers.RemoveObserver(id, observer);
}
