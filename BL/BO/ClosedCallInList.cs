namespace BO;

/// <summary>
/// represent a call in the close calls list, entity the see only.
/// </summary>
/// <param name="Id">uniquely readable ID representation of the call</param>
/// <param name="Type">the type of the call</param>
/// <param name="CallAddress">full address of the call</param>
/// <param name="CallOpenTime"> the call open time in the system</param>
/// <param name="CallEnterTime">time the call started to get treated</param>
/// <param name="CallCloseTime">time the call get closed</param>
/// <param name="EndKind"> show the way the call ended,treated, self_cancellation,administrator_cancellationor or expired_cancellation</param>

public class ClosedCallInList
{
    public int Id { get; init; }
    public BTypeCalls Type { get; set; }
    public string CallAddress { get; set; }
    public DateTime CallOpenTime { get; init; }
    public DateTime CallEnterTime { get; set; } 
    public DateTime? CallCloseTime { get; set; }
    public BEndKinds EndKind { get; set; }
    public override string ToString() => this.TostringProperty();

}
