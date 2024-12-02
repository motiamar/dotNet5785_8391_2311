namespace BO;

/// <summary>
/// represent a volunteer in a list. entity the see only.
/// </summary>
/// <param name="Id">uniquely readable ID representation</param>
/// <param name="FullName"> the full name of the volunteer</param>
/// <param name="Active">indicate if the volunteer is active right now or not</param>
/// <param name="TreatedCalls"> tells the amount of calls that this volunteer treated</param>
/// <param name="CanceledCalls">tells the amount of calls that this volunteer cenceld</param>
/// <param name="ExpiredCalls">tells the amount of calls that this volunteer choose and get expierd</param>
/// <param name="CorrentCallId">if there is, it show the corrent call id that the volunteer handle</param>
/// <param name="CorrentCallType"> show the type of call that the volunteer handle, if there isnt it will be None</param>

public class VolunteerInList
{
    public int Id { get; init; }
    public string FullName { get; set; }
    public Boolean Active { get; set; }
    public int TreatedCalls { get; set; }
    public int CanceledCalls { get; set; }
    public int ExpiredCalls { get; set; }
    public int? CorrentCallId { get; set; }
    public BTypeCalls CorrentCallType { get; set; }
    public override string ToString() => this.TostringProperty();
}
