using BlApi;
using BO;
using DalApi;
using DO;
using System.Security.Cryptography;
using static BO.Exceptions;
namespace Helpers;

internal static class CallManager
{
    private static IDal s_dal = DalApi.Factory.Get;

    internal static ObserverManager Observers = new(); //stage 5 

    /// <summary>
    /// return the status of the call = open, In_treatment, Closed, Expired, In_treatment_in_risk 
    /// </summary>
    internal static BO.BCallStatus GetStatus(DO.Call call)
    {
        IEnumerable<Assignment>? assignments;
        lock (AdminManager.BlMutex)
              assignments = s_dal.Assignment.ReadAll();

        var assignment = assignments.FirstOrDefault(a => a.CallId == call.Id);
        var currentRiskTime = Helpers.AdminManager.Now + Helpers.AdminManager.MaxRange;
        if (assignment == null)
        {
            return currentRiskTime < call.MaxEndingCallTime ? BCallStatus.Open : BCallStatus.Open_in_risk;
        }
        if (assignment!.FinishTime is not null)
        {
            return assignment.FinishTime > call.MaxEndingCallTime ? BCallStatus.Expired : BCallStatus.Closed;
        }
        return currentRiskTime < call.MaxEndingCallTime ? BCallStatus.In_treatment : BCallStatus.In_treatment_in_risk;
    }

    /// <summary>
    /// create a list of all assignments by the id of the call
    /// </summary>
    internal static List<BO.CallAssignInList>? GetCallAssignInList(int id)
    {
        IEnumerable<Assignment>? assignments;
        lock (AdminManager.BlMutex)
             assignments = s_dal.Assignment.ReadAll().Where(a => a.CallId == id);

        if (assignments.Count() == 0)
            return null;
        List<BO.CallAssignInList> callAssignInLists = new List<CallAssignInList>();
        foreach (var assignment in assignments)
        {
            DO.Volunteer? volunteer;
            lock (AdminManager.BlMutex)
                 volunteer = s_dal.Volunteer.Read(assignment.VolunteerId);

            callAssignInLists.Add(new BO.CallAssignInList
            {
                VolunteerId = assignment.VolunteerId,
                VolunteerFullName = volunteer!.FullName,
                CallEnterTime = assignment.StartTime,
                CallCloseTime = assignment.FinishTime,
                EndKind = (BO.BEndKinds)assignment.EndKind
            });
        }
        return callAssignInLists;
    }


    /// <summary>
    /// return a call in progress by the id of the assignment
    /// </summary>
    internal static BO.CallInProgress GetCallInProgress(DO.Assignment assignment, DO.Volunteer volunteer)
    {
        DO.Call call;
        lock (AdminManager.BlMutex)
             call = s_dal.Call.Read(assignment.CallId)!;
        double vLati = (Double)volunteer.Latitude!;
        double vLongi = (Double)volunteer.Longitude!;
        var callInProgres = new BO.CallInProgress
        {
            Id = assignment.Id,
            CallId = call.Id,
            Type = (BO.BTypeCalls)call.TypeCall,
            Description = call.VerbalDecription,
            CallAddress = call.FullAddressOfTheCall,
            CallOpenTime = call.OpeningCallTime,
            CallMaxCloseTime = call.MaxEndingCallTime,
            CallEnterTime = assignment.StartTime,
            CallDistance = Helpers.Tools.Distance(volunteer.DistanceType, vLati, vLongi, call.Latitude, call.Longitude),
            CallStatus = GetStatus(call)
        };
        return callInProgres;
    }

    /// <summary>
    /// func to get all the calls in the system
    /// </summary>
    internal static IEnumerable<CallInList> GetAllCallInList()
    {
        lock (AdminManager.BlMutex)
        {
            var calls = s_dal.Call.ReadAll();
            var assignments = s_dal.Assignment.ReadAll();
            var assignmentsLookup = assignments.ToLookup(a => a.CallId);
            var callInLists = from call in calls
                              let assignment = assignmentsLookup[call.Id].FirstOrDefault()
                              let volunteer = assignment != null ? s_dal.Volunteer.Read(assignment.VolunteerId) : null
                              select new BO.CallInList
                              {
                                  Id = assignment?.Id,
                                  CallId = call.Id,
                                  Type = (BO.BTypeCalls)Enum.Parse(typeof(BO.BTypeCalls), call.TypeCall.ToString()),
                                  CallOpenTime = call.OpeningCallTime,
                                  CallLeftTime = call.MaxEndingCallTime > Helpers.AdminManager.Now ? (call.MaxEndingCallTime - Helpers.AdminManager.Now) ?? TimeSpan.Zero : TimeSpan.Zero,
                                  LastVolunteerName = volunteer?.FullName ?? "N/A",
                                  TotalTreatmentTime = assignment?.FinishTime != null && assignment?.StartTime != null ? (TimeSpan)(assignment.FinishTime - assignment.StartTime) : TimeSpan.Zero,
                                  CallStatus = GetStatus(call),
                                  SumOfAssignments = assignmentsLookup[call.Id].Count()
                              };
            return callInLists;
        }
    }

    /// <summary>
    /// func to chek if the call datailes are valid
    /// </summary>
    internal static void CallChek(BO.Call change)
    {
        if (change.CallOpenTime > change.CallMaxCloseTime)
            throw new BlinCorrectException("The call open time can't be after the call max close time");
        if (Helpers.Tools.IsValidAddress(change.CallAddress) == false)
            throw new BlinCorrectException("The call address is not valid");
    }

    /// <summary>
    /// func to get all the closed calls in the system of a volunteer
    /// </summary>

    internal static IEnumerable<ClosedCallInList> GetClosedCallInLists(int id)
    {
        lock (AdminManager.BlMutex)
        {
            var calls = s_dal.Call.ReadAll();
            var assignments = s_dal.Assignment.ReadAll(v => v.VolunteerId == id);
            var closedCallInList = from assignment in assignments
                                   let call = calls.FirstOrDefault(v => v.Id == assignment.CallId)
                                   select new BO.ClosedCallInList
                                   {
                                       Id = call.Id,
                                       Type = (BTypeCalls)call.TypeCall,
                                       CallAddress = call.FullAddressOfTheCall,
                                       CallOpenTime = call.OpeningCallTime,
                                       CallEnterTime = assignments.FirstOrDefault(v => v.CallId == call.Id)!.StartTime,
                                       CallCloseTime = assignments.FirstOrDefault(v => v.CallId == call.Id)!.FinishTime,
                                       EndKind = (BEndKinds)assignments.FirstOrDefault(v => v.CallId == call.Id)!.EndKind,
                                   };
            closedCallInList.Where(v => v.CallCloseTime != null);
            return closedCallInList;
        }
    }

    /// <summary>
    /// func to return all the open calls in the system
    /// </summary>
    internal static IEnumerable<OpenCallInList> GetOpenCallInLists(DO.Volunteer volunteer)
    {
        lock (AdminManager.BlMutex)
        {
            var calls = s_dal.Call.ReadAll();
            var Calls = calls.Where(v => Helpers.CallManager.GetStatus(v) == BCallStatus.Open || Helpers.CallManager.GetStatus(v) == BCallStatus.Open_in_risk);
            var openCallInList = from call in Calls
                                 let vLati = volunteer.Latitude
                                 let vLongi = volunteer.Longitude
                                 select new BO.OpenCallInList
                                 {
                                     Id = call.Id,
                                     Type = (BTypeCalls)call.TypeCall,
                                     Description = call.VerbalDecription,
                                     CallAddress = call.FullAddressOfTheCall,
                                     CallOpenTime = call.OpeningCallTime,
                                     CallMaxCloseTime = call.MaxEndingCallTime,
                                     CallDistance = Helpers.Tools.Distance(volunteer.DistanceType, vLati ?? 0, vLongi ?? 0, call.Latitude, call.Longitude)
                                 };
            return openCallInList!;
        }
    }

    /// <summary>
    /// func to chek if the call is valid to be chosen
    /// </summary>
    internal static void ChooseCallChek(DO.Call call)
    {
        IEnumerable<Assignment>? assignments;
        lock (AdminManager.BlMutex)
             assignments = s_dal.Assignment.ReadAll();

        var assignment = assignments.FirstOrDefault(a => a.CallId == call.Id);
        if (assignment is not null)
            throw new BlinCorrectException("The call is already in treatment");
        if (call.MaxEndingCallTime < Helpers.AdminManager.Now)
            throw new BlinCorrectException("The call is expired");
    }

    /// <summary>
    /// func to update all the calls status if the clock time get change
    /// </summary>
    internal static void UpdateCalls()
    {
        IEnumerable<DO.Call>? calls;
        lock (AdminManager.BlMutex) 
             calls = s_dal.Call.ReadAll().ToList();

        foreach (var call in calls)
        {
            IEnumerable<Assignment>? assignments;
            lock (AdminManager.BlMutex)
                 assignments = s_dal.Assignment.ReadAll(v => v.CallId == call.Id).Where(v=> v.FinishTime is null).ToList();

            if (call.MaxEndingCallTime < Helpers.AdminManager.Now)
            {               
                if (assignments is null)
                {
                    DO.Assignment assignment = new DO.Assignment { CallId = call.Id, VolunteerId = 0, FinishTime = Helpers.AdminManager.Now, EndKind = EndKinds.Expired_cancellation };

                    lock (AdminManager.BlMutex)
                        s_dal.Assignment.Create(assignment);

                    CallManager.Observers.NotifyListUpdated();
                }
                else 
                {
                    foreach (var assignment in assignments)
                    {
                        DO.Assignment assign = new DO.Assignment
                        {
                            Id = assignment.Id,
                            CallId = assignment.CallId,
                            VolunteerId = assignment.VolunteerId,
                            StartTime = assignment.StartTime,
                            FinishTime = Helpers.AdminManager.Now,
                            EndKind = EndKinds.Expired_cancellation
                        };

                        lock (AdminManager.BlMutex)
                            s_dal.Assignment.Update(assign);

                        CallManager.Observers.NotifyListUpdated();
                        CallManager.Observers.NotifyItemUpdated(assignment.CallId);
                    }
                }
            }
            
        }
    }

    

}
