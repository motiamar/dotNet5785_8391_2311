using Helpers;
namespace BO;

/// <summary>
/// represent a call in the system. entity the see, Update, Add and Delete.
/// </summary>
public class Call
{
    /// <summary>
    /// uniquely readable ID representation of the assingment
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
    public string? CallAddress { get; set; }
    /// <summary>
    /// number that Show the distance between the point and the north or sout of the wquator
    /// </summary>
    public double? Latitude { get; set; }
    /// <summary>
    /// number that Show the distance between the point and the east or west of the wquator
    /// </summary>
    public double? Longitude { get; set; }
    /// <summary>
    /// the call Open time in the system
    /// </summary>
    public DateTime CallOpenTime { get; init; }
    /// <summary>
    /// max time to close the call
    /// </summary>
    public DateTime? CallMaxCloseTime { get; set; }
    /// <summary>
    /// if the call is in treatment or in a risk treatment
    /// </summary>
    public BCallStatus CallStatus { get; set; }
    /// <summary>
    /// Show a list of assignment that assign to that call if there is
    /// </summary>
    public List<BO.CallAssignInList>? callAssignInLists { get; init; } = null;

    public override string ToString() => this.TostringProperty();

}
