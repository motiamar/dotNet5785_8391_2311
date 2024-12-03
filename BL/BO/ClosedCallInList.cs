namespace BO;

/// <summary>
/// represent a call in the close calls list, entity the see only.
/// </summary>
public class ClosedCallInList
{
    /// <summary>
    /// uniquely readable ID representation of the call
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// the type of the call
    /// </summary>
    public BTypeCalls Type { get; set; }
    /// <summary>
    /// full address of the call
    /// </summary>
    public string CallAddress { get; set; }
    /// <summary>
    /// the call open time in the system
    /// </summary>
    public DateTime CallOpenTime { get; init; }
    /// <summary>
    /// time the call started to get treated
    /// </summary>
    public DateTime CallEnterTime { get; set; }
    /// <summary>
    /// time the call get closed
    /// </summary>
    public DateTime? CallCloseTime { get; set; }
    /// <summary>
    /// show the way the call ended,treated, self_cancellation,administrator_cancellationor or expired_cancellation
    /// </summary>
    public BEndKinds EndKind { get; set; }
    public override string ToString() => this.TostringProperty();

}
