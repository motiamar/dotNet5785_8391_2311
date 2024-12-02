namespace BO;
public class ClosedCallInList
{
    public int Id { get; init; }
    public BTypeCalls Type { get; set; }
    public string CallAddress { get; set; }
    public DateTime CallOpenTime { get; init; }
    public DateTime CallEnterTime { get; set; } 
    public DateTime? CallCloseTime { get; set; }
    public BEndKinds EndKind { get; set; }
    public override string ToString() => this.TostringProperty();

}
