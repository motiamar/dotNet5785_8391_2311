namespace BO;

public class VolunteerInList
{
    public int Id { get; init; }
    public string FullName { get; set; }
    public Boolean Active { get; set; }
    public int TreatedCalls { get; init; }
    public int CanceledCalls { get; init; }
    public int ExpiredCalls { get; init; }
    public int? CorrentCallId { get; set; }
    public BTypeCalls CorrentCallType { get; set; }
    public override string ToString() => this.TostringProperty();
}
