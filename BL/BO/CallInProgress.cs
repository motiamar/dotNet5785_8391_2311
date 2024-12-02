namespace BO;
public class CallInProgress
{
    public int Id { get; init; }
    public int CallId { get; init; }
    public BTypeCalls Type { get; set; }  
    public string? Description { get; set; }
    public string CallAddress { get; set; }
    public DateTime CallOpenTime { get; init; }
    public DateTime? CallMaxCloseTime { get; set; }
    public DateTime CallEnterTime { get; set; }
    public double CallDistance { get; set; }
    public  BCallStatus CallStatus { get; set; }
    public override string ToString()=>this.TostringProperty();

}
