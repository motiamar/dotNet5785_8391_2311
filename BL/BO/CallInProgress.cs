namespace BO;

/// <summary>
/// represent a call in the volunteer treatment. entity the see only.
/// </summary>
/// <param name="Id">uniquely readable ID representation of the assingment</param>
/// <param name="CallId">uniquely readable ID representation of the call</param>
/// <param name="Type">the type of the call</param>
/// <param name="Description">a virable description of the call</param>
/// <param name="CallAddress">full address of the call</param>
/// <param name="CallOpenTime"> the call open time in the system</param>
/// <param name="CallMaxCloseTime"> max time to close the call</param>
/// <param name="CallEnterTime">time the call started to get treated</param>
/// <param name="CallDistance"> the distance between the volunteer address and the call address</param>
/// <param name="CallStatus"> if the call is in treatment or in a risk treatment</param>

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
