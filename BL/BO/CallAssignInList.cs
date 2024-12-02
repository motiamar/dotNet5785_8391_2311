namespace BO;

/// <summary>
/// represent a call assign in the list, entity the see only.
/// </summary>
/// <param name="Id">uniquely readable ID representation of the assingment</param>
/// <param name="VolunteerFullName"> the name of the volunteer who treating the call</param>
/// <param name="CallEnterTime">time the call started to get treated</param>
/// <param name="CallCloseTime">time the call get closed</param>
/// <param name="EndKind"> show the way the call ended,treated, self_cancellation,administrator_cancellationor or expired_cancellation</param>


public class CallAssignInList
{
    public int? Id { get; init; }
    public string? VolunteerFullName { get; set; }
    public DateTime CallEnterTime { get; init; }
    public DateTime? CallCloseTime { get; set; }
    public BEndKinds? EndKind { get; set; }
    public override string ToString() => this.TostringProperty();
}
