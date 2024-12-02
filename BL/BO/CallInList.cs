namespace BO;

/// <summary>
/// represent a call in a calls list, entity the see only.
/// </summary>
/// <param name="Id">uniquely readable ID representation of the assingment</param>
/// <param name="CallId">uniquely readable ID representation of the call</param>
/// <param name="Type">the type of the call</param>
/// <param name="CallOpenTime"> the call open time in the system</param>
/// <param name="CallMaxCloseTime"> max time to close the call</param>
/// <param name="LastVolunteerName">the last volunteer who handle the call</param>
/// <param name="TotalTreatmentTime"> if the call treated, how much time did it take</param>
/// <param name="CallStatus"> if the call is in treatment or in a risk treatment</param>
/// <param name="SumOfAssignments"> show how many time the call gut cenceled</param>

public class CallInList
{
    public int Id { get; init; }
    public int CallId { get; set; }
    public BTypeCalls Type { get; set; }
    public DateTime CallOpenTime { get; init; }
    public TimeSpan? CallMaxCloseTime { get; set; }
    public string? LastVolunteerName { get; set; }  
    public TimeSpan? TotalTreatmentTime { get; set; }
    public BCallStatus CallStatus { get; set; }
    public int SumOfAssignments { get; set; }
    public override string ToString() => this.TostringProperty();
}
