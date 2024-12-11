using BO;
using DalApi;
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
    /// return the  BO.CallInProgress by id 
    /// </summary>
    public static BO.CallInProgress? GetCallInProgress(int id)
    {
        DO.Assignment assignment = s_dal.Assignment.Read(id)!;
        DO.Volunteer volunteer = s_dal.Volunteer.Read(assignment.VolunteerId)!;
        DO.Call call = s_dal.Call.Read(assignment.CallId)!;
        var callInProgress = new BO.CallInProgress
        {
            Id = id,
            CallId = assignment.CallId,
            Type = (BTypeCalls)call.TypeCall,
            Description = call.VerbalDecription,
            CallAddress = call.FullAddressOfTheCall,
            CallOpenTime = call.OpeningCallTime,
            CallMaxCloseTime = call.MaxEndingCallTime,
            CallEnterTime = assignment.StartTime,
            CallDistance = Helpers.Tools.Distance(volunteer.DistanceType, volunteer.Latitude!.Value, volunteer.Longitude!.Value, call.Latitude, call.Longitude), fix
            CallStatus = Helpers.CallManager.GetStatus(call)
        };
        return callInProgress;
    }
}
