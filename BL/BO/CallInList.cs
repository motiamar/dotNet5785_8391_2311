using Helpers;
namespace BO;

/// <summary>
/// represent a call in a calls list, entity the see only.
/// </summary>
public class CallInList
{
    /// <summary>
    /// uniquely readable ID representation of the assingment
    /// </summary>
    public int? Id { get; init; }
    /// <summary>
    /// uniquely readable ID representation of the call
    /// </summary>
    public int CallId { get; set; }
    /// <summary>
    /// the type of the call
    /// </summary>
    public BTypeCalls Type { get; set; }
    /// <summary>
    /// the call open time in the system
    /// </summary>
    public DateTime CallOpenTime { get; init; }
    /// <summary>
    /// max time to close the call
    /// </summary>
    public TimeSpan? CallMaxCloseTime { get; set; }
    /// <summary>
    /// the last volunteer who handle the call
    /// </summary>
    public string? LastVolunteerName { get; set; }
    /// <summary>
    /// if the call treated, how much time did it take
    /// </summary>
    public TimeSpan? TotalTreatmentTime { get; set; }
    /// <summary>
    /// if the call is in treatment or in a risk treatment
    /// </summary>
    public BCallStatus CallStatus { get; set; }
    /// <summary>
    /// show how many time the call gut cenceled
    /// </summary>
    public int SumOfAssignments { get; set; }
    public override string ToString() => this.TostringProperty();
}
