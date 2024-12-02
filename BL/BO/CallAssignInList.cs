namespace BO;

public class CallAssignInList
{
    public int? Id { get; init; }
    public string? VolunteerFullName { get; set; }
    public DateTime CallEnterTime { get; init; }
    public DateTime? CallExitTime { get; set; }
    public BEndKinds? EndKind { get; set; }
    public override string ToString() => this.TostringProperty();
}
