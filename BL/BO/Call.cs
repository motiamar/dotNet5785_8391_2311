namespace BO;

/// <summary>
/// represent a call in the system. entity the see, update, add and delete.
/// </summary>
/// <param name="Id">uniquely readable ID representation of the assingment</param>
/// <param name="Type">the type of the call</param>
/// <param name="Description">a virable description of the call</param>
/// <param name="CallAddress">full address of the call</param>
/// <param name="Latitude"> number that show the distance between the point and the north or sout of the wquator</param>
/// <param name="Longitude">number that show the distance between the point and the east or west of the wquator</param>
/// <param name="CallOpenTime"> the call open time in the system</param>
/// <param name="CallMaxCloseTime"> max time to close the call</param>
/// <param name="CallStatus"> if the call is in treatment or in a risk treatment</param>
/// <param name="callAssignInLists">show a list of assignment that assign to that call if there is</param>


internal class Call
{
    public int Id { get; init; }
    public BTypeCalls Type { get; set; }
    public string? Description { get; set; }
    public string? CallAddress { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public DateTime CallOpenTime { get; init; }
    public DateTime? CallMaxCloseTime { get; set; }
    public BCallStatus CallStatus { get; set; }
    public List<BO.CallAssignInList>? callAssignInLists { get; init; } = null;
    public override string ToString() => this.TostringProperty();

}
