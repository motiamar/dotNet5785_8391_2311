namespace DO;

/// <summary>
/// Volunteer in the rescue organization 
/// </summary>
/// <param name = "Id"> presonal unique id of the Volunteer </param> 
/// <param name = "FullName"> first and last name of the Volunteer </param> 
/// <param name = "Phone"> presonal phone number of the Volunteer </param> 
/// <param name = "Email"> presonal email addres of the Volunteer </param> 
/// <param name = "Password"> password that first given by the Manager and change by the Volunteer </param> 
/// <param name = "Address"> presonal full and real corrent addres of the Volunteer </param> 
/// <param name = "Latitude"> number that Show the distance between the point and the north or sout of the wquator  </param> 
/// <param name = "Longitude"> number that Show the distance between the point and the east or west of the wquator </param> 
/// <param name = "Role"> indicate the position of the Volunteer, a Manager or not </param> 
/// <param name = "Active"> indicate if the Volunteer is still in duty </param> 
/// <param name = "MaximumDistance"> Show the max distance that the Volunteer can reach </param> 
/// <param name = "DistanceType"> indicate in what means the distance is taken from the Volunteer to the call point </param> 
public record Volunteer
(
    int Id,
    string FullName,
    string Phone,
    string Email,
    string? Password = null,
    string? Address = null,
    double? Latitude = null,
    double? Longitude = null,
    Roles Role = Roles.Volunteer,
    Boolean Active = false,
    double? MaximumDistance = null,
    DistanceTypes DistanceType = DistanceTypes.Air
)

{
    public Volunteer() : this (0,"", "", "") { }
}
