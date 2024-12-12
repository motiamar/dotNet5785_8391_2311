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
    /// the call Open time in the system
    /// </summary>
    public DateTime CallOpenTime { get; init; }
    /// <summary>
    /// max time to close the call
    /// </summary>
    public TimeSpan? CallLeftTime { get; set; }
    /// <summary>
    /// the last Volunteer who handle the call
    /// </summary>
    public string? LastVolunteerName { get; set; }
    /// <summary>
    /// if the call Treated, how much time did it take
    /// </summary>
    public TimeSpan? TotalTreatmentTime { get; set; }
    /// <summary>
    /// if the call is in treatment or in a risk treatment
    /// </summary>
    public BCallStatus CallStatus { get; set; }
    /// <summary>
    /// Show how many time the call gut cenceled
    /// </summary>
    public int SumOfAssignments { get; set; }
    public override string ToString() => this.TostringProperty();
}
