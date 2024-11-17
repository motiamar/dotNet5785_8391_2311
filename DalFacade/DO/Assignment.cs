namespace DO;

/// <summary>
/// Assignment is entity that connect between the call to the volunteer 
/// </summary>
/// <param name = "Id">  unique id of the Assignment </param> 
/// <param name = "CallId"> unique id that represnte the call number  </param> 
/// <param name = "VolunteerId"> the unique id of the volunteer that hendel the call </param> 
/// <param name = "StartTime"> the time that the volunteer approves the call </param> 
/// <param name = "FinishTime"> the time that the volunteer close the call </param> 
/// <param name = "EndKind"> tell how the call ended (by volunteer, manager or cancellation) </param> 
public record Assignment
(
    int Id,
    int CallId,
    int VolunteerId,
    DateTime StartTime = default, 
    DateTime? FinishTime = null, 
    Enum? EndKind = default 
)

{ 
    public Assignment() : this (0, 0, 0) { }
}

