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
}
