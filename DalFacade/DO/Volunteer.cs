namespace DO;
/// <summary>
/// Volunteer in the rescue organization 
/// </summary>
/// <param name = "Id"> presonal unique id of the volunteer </param> 
/// <param name = "FullName"> first and last name of the volunteer </param> 
/// <param name = "Phone"> presonal phone number of the volunteer </param> 
/// <param name = "Email"> presonal email addres of the volunteer </param> 
/// <param name = "Password"> password that first given by the manager and change by the volunteer </param> 
/// <param name = "Address"> presonal full and real corrent addres of the volunteer </param> 
/// <param name = "Latitude"> number that show the distance between the point and the north or sout of the wquator  </param> 
/// <param name = "Longitude"> number that show the distance between the point and the east or west of the wquator </param> 
/// <param name = "Role"> indicate the position of the volunteer, a manager or not </param> 
/// <param name = "Active"> indicate if the volunteer is still in duty </param> 
/// <param name = "MaximumDistance"> show the max distance that the volunteer can reach </param> 
/// <param name = "DistanceType"> indicate in what means the distance is taken from the volunteer to the call point </param> 
public record Volunteer
{
    int Id;
    string FullName;
    string Phone;
    string Email;
    string? Password;
    string? Address;
    double? Latitude;
    double? Longitude;
    Enum Role;
    Boolean Active;
    double? MaximumDistance;
    Enum DistanceType;

    public Volunteer() { }

}
