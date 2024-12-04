using Helpers;
namespace BO;

/// <summary>
/// represent a volunteer in a list. entity the see only.
/// </summary>
public class VolunteerInList
{
    /// <summary>
    /// uniquely readable ID representation
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// the full name of the volunteer
    /// </summary>
    public string FullName { get; set; }
    /// <summary>
    /// indicate if the volunteer is active right now or not
    /// </summary>
    public Boolean Active { get; set; }
    /// <summary>
    /// tells the amount of calls that this volunteer treated
    /// </summary>
    public int TreatedCalls { get; set; }
    /// <summary>
    /// tells the amount of calls that this volunteer cenceld
    /// </summary>
    public int CanceledCalls { get; set; }
    /// <summary>
    /// tells the amount of calls that this volunteer choose and get expierd
    /// </summary>
    public int ExpiredCalls { get; set; }
    /// <summary>
    /// if there is, it show the corrent call id that the volunteer handle
    /// </summary>
    public int? CorrentCallId { get; set; }
    /// <summary>
    /// show the type of call that the volunteer handle, if there isnt it will be None
    /// </summary>
    public BTypeCalls CorrentCallType { get; set; }
    public override string ToString() => this.TostringProperty();
}
