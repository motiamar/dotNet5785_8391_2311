using Helpers;
namespace BO;

/// <summary>
/// represent a call in the open calls list, entity the see only.
/// </summary>
public class OpenCallInList
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
    /// a virable description of the call
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// full address of the call
    /// </summary>
    public string CallAddress { get; set; }
    /// <summary>
    /// the call open time in the system
    /// </summary>
    public DateTime CallOpenTime { get; init; }
    /// <summary>
    /// max time to close the call
    /// </summary>
    public DateTime? CallMaxCloseTime { get; set; }
    /// <summary>
    ///  the distance between the volunteer address and the call address
    /// </summary>
    public double CallDistance { get; set; }
    public override string ToString() => this.TostringProperty();
}
