namespace BO;

public class CallInList
{
    public int Id { get; init; }
    public int CallId { get; set; }
    public BTypeCalls TypeCall { get; set; }
    public DateTime CallOpenTime { get; init; }
    public TimeSpan? LeftCallTime { get; set; }
    public string? LastVolunteerName { get; set; }  
    public TimeSpan? TotalTreatmentTime { get; set; }
    public BCallStatus CallStatus { get; set; }
    public int SumOfAssignments { get; set; }
    public override string ToString() => this.TostringProperty();
}
