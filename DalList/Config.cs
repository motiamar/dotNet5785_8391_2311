using System.Runtime.CompilerServices;

namespace Dal;

internal static class Config
{
    // NextCallId is a paramter that change every time its calld and represent the corrent number of the call
    internal const int StartCallId = 1000;
    private static int nextCallId = StartCallId; 
    internal static int NextCallId
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get =>  nextCallId++; 
    }


    // NextAssignmentId is a paramter that change every time its calld and represent the corrent number of the Assignment
    internal const int StartAssignmentId = 1000;
    private static int nextAssignmentId = StartAssignmentId;
    internal static int NextAssignmentId
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get => nextAssignmentId++; 
    }
   


// return the corrent time of the clock
    internal static DateTime Clock
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get;
        [MethodImpl(MethodImplOptions.Synchronized)]
        set; 
    } = DateTime.Now;


    internal static TimeSpan RiskRnge
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get;
        [MethodImpl(MethodImplOptions.Synchronized)]
        set; 
    } = new TimeSpan(0,30,0);


    // rest function for all the entitis

    [MethodImpl(MethodImplOptions.Synchronized)]
    internal static void Reset()
    {
        nextCallId = StartCallId;
        nextAssignmentId = StartAssignmentId;
        Clock = DateTime.Now;
        RiskRnge = TimeSpan.Zero;
    }
    
}
