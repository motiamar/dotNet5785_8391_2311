using Helpers;
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
    /// the call Open time in the system
    /// </summary>
    public DateTime CallOpenTime { get; init; }
    /// <summary>
    /// time the call started to get Treated
    /// </summary>
    public DateTime CallEnterTime { get; set; }
    /// <summary>
    /// time the call get Closed
    /// </summary>
    public DateTime? CallCloseTime { get; set; }
    /// <summary>
    /// Show the way the call ended,Treated, Self_cancellation,administrator_cancellationor or Expired_cancellation
    /// </summary>
    public BEndKinds EndKind { get; set; }
    public override string ToString() => this.TostringProperty();

}
