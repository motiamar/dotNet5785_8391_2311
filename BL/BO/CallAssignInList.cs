namespace BO;

/// <summary>
/// represent a call assign in the list, entity the see only.
/// </summary>
public class CallAssignInList
{
    /// <summary>
    /// uniquely readable ID representation of the assingment
    /// </summary>
    public int? Id { get; init; }
    /// <summary>
    /// the name of the volunteer who treating the call
    /// </summary>
    public string? VolunteerFullName { get; set; }
    /// <summary>
    /// time the call started to get treated
    /// </summary>
    public DateTime CallEnterTime { get; init; }
    /// <summary>
    /// time the call get closed
    /// </summary>
    public DateTime? CallCloseTime { get; set; }
    /// <summary>
    /// show the way the call ended,treated, self_cancellation,administrator_cancellationor or expired_cancellation
    /// </summary>
    public BEndKinds? EndKind { get; set; }
    public override string ToString() => this.TostringProperty();
}
