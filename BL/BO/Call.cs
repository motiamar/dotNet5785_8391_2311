namespace BO;

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
