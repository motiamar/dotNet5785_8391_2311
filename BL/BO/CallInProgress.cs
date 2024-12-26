using Helpers;
namespace BO;

/// <summary>
/// represent a call in the Volunteer treatment. entity the see only.
/// </summary>
public class CallInProgress
{
    /// <summary>
    /// uniquely readable ID representation of the assingment
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// uniquely readable ID representation of the call
    /// </summary>
    public int CallId { get; init; }
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
    /// the call Open time in the system
    /// </summary>
    public DateTime CallOpenTime { get; init; }
    /// <summary>
    /// max time to close the call
    /// </summary>
    public DateTime? CallMaxCloseTime { get; set; }
    /// <summary>
    /// time the call started to get Treated
    /// </summary>
    public DateTime CallEnterTime { get; set; }
    /// <summary>
    /// the distance between the Volunteer address and the call address
    /// </summary>
    public double CallDistance { get; set; }
    /// <summary>
    /// if the call is in treatment or in a risk treatment
    /// </summary>
    public BCallStatus CallStatus { get; set; }
    public override string ToString() => this.TostringProperty();

}
