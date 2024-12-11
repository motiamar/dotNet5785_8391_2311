using BO;
using DalApi;
using DO;
namespace Helpers;

internal static class CallManager
{
    private static IDal s_dal = Factory.Get;

    /// <summary>
    /// return the status of the call = In_treatment or In_treatment_in_risk
    /// </summary>
    public static BO.BCallStatus GetStatus(DO.Call call)
    {
        var currentRiskTime = ClockManager.Now + s_dal.Config.RiskRnge;
        return currentRiskTime < call.MaxEndingCallTime ? BO.BCallStatus.In_treatment : BO.BCallStatus.In_treatment_in_risk;
    }

    /// <summary>
    /// create a spzsific call with the given id 
    /// </summary>
    public static BO.Call? GetSpasificCall(int id)
    {
        var call = s_dal.Call.Read(id);

        if (call == null)
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
            CallStatus = GetStatus(call),
            callAssignInLists = GetCallAssignInList(id)
        };
    }

    /// <summary>
    /// create a list of all assignments by the id of the call
    /// </summary>
    public static List<BO.CallAssignInList>? GetCallAssignInList(int id)
    {
       var callAssignInLists = (from a in s_dal.Assignment.ReadAll()
                               where a.CallId == id
                                let call = s_dal.Call.Read(a.CallId)
                                select new BO.CallAssignInList
                               {
                                   Id = a.Id,
                                   VolunteerFullName = s_dal.Volunteer.Read(a.VolunteerId)?.FullName,
                                   CallEnterTime = call.OpeningCallTime,
                                    CallCloseTime = call.MaxEndingCallTime,
                                    EndKind = (BO.BEndKinds)a.EndKind
                               }).ToList();
        return callAssignInLists;
    }
}

    