namespace Dal;

internal static class DataSource
{
    
    // empty lists for all the entitis, apply as empty
    internal static List<DO.Assignment> Assignments { get; } = new();
    internal static List<DO.Call> Calls { get; } = new();
    internal static List<DO.Volunteer> Volunteers { get; } = new();
    
}
