namespace Dal;

internal static class Config
{
    internal const int StartCallId = 1000;
    private static int nextCallId = StartCallId;
    internal static int NextCallId { get =>  nextCallId++; }
    // NextCallId is a paramter that change every time its calld and represent the corrent number of the call


    internal const int StartAssignmentId = 1000;
    private static int nextAssignmentId = StartAssignmentId;
    internal static int NextAssignmentId { get => nextAssignmentId++; }
    // NextAssignmentId is a paramter that change every time its calld and represent the corrent number of the Assignment

    internal static DateTime Clock {  get; set; } = DateTime.Now;
    // return the corrent time of the clock

    internal static TimeSpan RiskRnge { get; set; } = TimeSpan.FromHours(1);

    internal static void Reset()
    {
        nextCallId = StartCallId;
        nextAssignmentId = StartAssignmentId;
        Clock = DateTime.Now;
    }
    // rest function for all the entitis

}
