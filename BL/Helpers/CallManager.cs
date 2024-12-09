using DalApi;
namespace Helpers;

internal static class CallManager
{
    private static IDal s_dal = Factory.Get;
    public static BO.BCallStatus GetStatus(DO.Call call)
    {
        ClockManager.Now
        return default(BO.BCallStatus);
    }

}
