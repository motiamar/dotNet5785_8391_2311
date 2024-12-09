using Helpers;
namespace BO;

/// <summary>
/// represent a Volunteer in a list. entity the see only.
/// </summary>
public class VolunteerInList
{
    /// <summary>
    /// uniquely readable ID representation
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// the full name of the Volunteer
    /// </summary>
    public string FullName { get; set; }
    /// <summary>
    /// indicate if the Volunteer is active right now or not
    /// </summary>
    public Boolean Active { get; set; }
    /// <summary>
    /// tells the amount of calls that this Volunteer Treated
    /// </summary>
    public int TreatedCalls { get; set; }
    /// <summary>
    /// tells the amount of calls that this Volunteer cenceld
    /// </summary>
    public int CanceledCalls { get; set; }
    /// <summary>
    /// tells the amount of calls that this Volunteer choose and get expierd
    /// </summary>
    public int ExpiredCalls { get; set; }
    /// <summary>
    /// if there is, it Show the corrent call id that the Volunteer handle
    /// </summary>
    public int? CorrentCallId { get; set; }
    /// <summary>
    /// Show the type of call that the Volunteer handle, if there isnt it will be None
    /// </summary>
    public BTypeCalls CorrentCallType { get; set; }
    public override string ToString() => this.TostringProperty();
}
