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

    /// <summary>
    /// return the status of the call = open, In_treatment, Closed, Expired, In_treatment_in_risk 
    /// </summary>
    internal static BO.BCallStatus GetStatus(DO.Call call)
    {
        var assignment = s_dal.Assignment.ReadAll().FirstOrDefault(a => a.CallId == call.Id);
        var currentRiskTime = ClockManager.Now + s_dal.Config.RiskRnge;
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
        var assignments = s_dal.Assignment.ReadAll().Where(a => a.CallId == id);
        if (assignments.Count() == 0)
            return null;
        List<BO.CallAssignInList> callAssignInLists = new List<CallAssignInList>();
        foreach (var assignment in assignments)
        {
            var volunteer = s_dal.Volunteer.Read(assignment.VolunteerId);
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
    internal static BO.CallInProgress GetCallInProgress(int id)
    {
        DO.Assignment assignment = s_dal.Assignment.Read(id)!;
        DO.Call call = s_dal.Call.Read(assignment.CallId)!;
        DO.Volunteer volunteer = s_dal.Volunteer.Read(assignment.VolunteerId)!;
        double vLati = Helpers.Tools.GetLatitudeFromAddress(volunteer.Address!);
        double vLongi = Helpers.Tools.GetLongitudeFromAddress(volunteer.Address!);
        var callInProgres = new BO.CallInProgress
        {
            Id = id,
            CallId = assignment.CallId,
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
        var calls = s_dal.Call.ReadAll();
        var assignments = s_dal.Assignment.ReadAll();
        var callInLists = from call in calls
                          select new BO.CallInList
                          {
                              Id = assignments.FirstOrDefault(a => a.CallId == call.Id)!.Id,
                              CallId = call.Id,
                              Type = (BO.BTypeCalls)call.TypeCall,
                              CallOpenTime = call.OpeningCallTime,
                              CallLeftTime = (TimeSpan)(call.MaxEndingCallTime - ClockManager.Now)!,
                              LastVolunteerName = s_dal.Volunteer.Read(assignments.FirstOrDefault(a => a.CallId == call.Id)!.VolunteerId)!.FullName,
                              TotalTreatmentTime = (TimeSpan)(assignments.FirstOrDefault(a => a.CallId == call.Id)!.FinishTime - assignments.FirstOrDefault(a => a.CallId == call.Id)!.StartTime)!,
                              CallStatus = GetStatus(call),
                              SumOfAssignments = assignments.Count(a => a.CallId == call.Id)
                          };
        return callInLists;
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

    /// <summary>
    /// func to return all the open calls in the system
    /// </summary>
    internal static IEnumerable<OpenCallInList> GetOpenCallInLists(DO.Volunteer volunteer)
    {
        var calls = s_dal.Call.ReadAll();
        calls.Where(v => Helpers.CallManager.GetStatus(v) == BCallStatus.Open || Helpers.CallManager.GetStatus(v) == BCallStatus.Open_in_risk);
        var openCallInList = from call in calls
                             let vLati = Helpers.Tools.GetLatitudeFromAddress(volunteer.Address!)
                             let vLongi = Helpers.Tools.GetLongitudeFromAddress(volunteer.Address!)
                             select new BO.OpenCallInList
                             {
                                 Id = call.Id,
                                 Type = (BTypeCalls)call.TypeCall,
                                 Description = call.VerbalDecription,
                                 CallAddress = call.FullAddressOfTheCall,
                                 CallOpenTime = call.OpeningCallTime,
                                 CallMaxCloseTime = call.MaxEndingCallTime,
                                 CallDistance = Helpers.Tools.Distance(volunteer.DistanceType, vLati, vLongi, call.Latitude, call.Longitude)
                             };
        return openCallInList!;
    }

    /// <summary>
    /// func to chek if the call is valid to be chosen
    /// </summary>
    internal static void ChooseCallChek(DO.Call call)
    {
        var assignment = s_dal.Assignment.ReadAll(v => v.CallId == call.Id);
        if (assignment is not null)
            throw new BlinCorrectException("The call is already in treatment");
        if (call.MaxEndingCallTime < ClockManager.Now)
            throw new BlinCorrectException("The call is expired");
    }

    /// <summary>
    /// func to update all the calls status if the clock time get change
    /// </summary>
    internal static void UpdateCalls()
    {
        var calls = s_dal.Call.ReadAll();
        foreach (var call in calls)
        {
            var assignments = s_dal.Assignment.ReadAll(v => v.CallId == call.Id).Where(v=> v.FinishTime is null);
            if (call.MaxEndingCallTime < ClockManager.Now)
            {               
                if (assignments is null)
                {
                    DO.Assignment assignment = new DO.Assignment { CallId = call.Id, VolunteerId = 0, FinishTime = Helpers.ClockManager.Now, EndKind = EndKinds.Expired_cancellation };
                    s_dal.Assignment.Create(assignment);
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
                            FinishTime = Helpers.ClockManager.Now,
                            EndKind = EndKinds.Expired_cancellation
                        };
                        s_dal.Assignment.Update(assign);
                    }
                }
            }
            
        }
    }
}
    