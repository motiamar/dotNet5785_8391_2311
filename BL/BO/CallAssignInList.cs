using Helpers;
namespace BO;

/// <summary>
/// represent a call assign in the list, entity the see only.
/// </summary>
public class CallAssignInList
{
    /// <summary>
    /// the volunteer id that treating the call
    /// </summary>
    public int? VolunteerId { get; init; }
    /// <summary>
    /// the name of the Volunteer who treating the call
    /// </summary>
    public string? VolunteerFullName { get; set; }
    /// <summary>
    /// time the call started to get Treated
    /// </summary>
    public DateTime CallEnterTime { get; init; }
    /// <summary>
    /// time the call get Closed
    /// </summary>
    public DateTime? CallCloseTime { get; set; }
    /// <summary>
    /// Show the way the call ended,Treated, Self_cancellation,administrator_cancellationor or Expired_cancellation
    /// </summary>
    public BEndKinds? EndKind { get; set; }
    public override string ToString() => this.TostringProperty();
}
