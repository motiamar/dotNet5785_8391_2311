using BlApi;
using BO;
using DalApi;
using DO;
using System.Security.Cryptography;
using static BO.Exceptions;
namespace Helpers;

internal static class CallManager
{
    private static IDal s_dal = Factory.Get;

    /// <summary>
    /// return the status of the call = open, In_treatment, Closed, Expired, In_treatment_in_risk 
    /// </summary>
    public static BO.BCallStatus GetStatus(DO.Call call)
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
    public static List<BO.CallAssignInList>? GetCallAssignInList(int id)
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
    public static BO.CallInProgress GetCallInProgress(int id)
    {
        DO.Assignment assignment = s_dal.Assignment.Read(id)!;
        DO.Call call = s_dal.Call.Read(assignment.CallId)!;
        DO.Volunteer volunteer = s_dal.Volunteer.Read(assignment.VolunteerId)!;
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
            CallDistance = Helpers.Tools.Distance(volunteer.DistanceType, volunteer.Latitude, volunteer.Longitude, call.Latitude, call.Longitude),
            CallStatus = GetStatus(call)
        };
        return callInProgres;
    }

    /// <summary>
    /// func to get all the calls in the system
    /// </summary>
    public static IEnumerable<CallInList> GetAllCallInList()
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

    public static void CallChek(BO.Call change)
    {
        if(change.CallOpenTime > change.CallMaxCloseTime)
            throw new BlinCorrectException("The call open time can't be after the call max close time");
        if(Helpers.Tools.IsValidAddress(change.CallAddress) == false)
            throw new BlinCorrectException("The call address is not valid");
    }

}
    