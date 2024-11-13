namespace DO;
/// <summary>
/// <param name = "Id"> presonal unique id of the volunteer </param> 
/// <param name = "FullName"> presonal unique id of the volunteer </param> 
/// <param name = "Phone"> presonal unique id of the volunteer </param> 
/// <param name = "Email"> presonal unique id of the volunteer </param> 
/// <param name = "Password"> presonal unique id of the volunteer </param> 
/// <param name = "Addres"> presonal unique id of the volunteer </param> 
/// <param name = ""> presonal unique id of the volunteer </param> 
/// <param name = ""> presonal unique id of the volunteer </param> 
/// <param name = ""> presonal unique id of the volunteer </param> 
/// <param name = ""> presonal unique id of the volunteer </param> 
/// <param name = ""> presonal unique id of the volunteer </param> 
/// <param name = ""> presonal unique id of the volunteer </param> 
/// </summary>
public record Volunteer
{
    int Id;
    string FullName;
    string Phone;
    string Email;
    string? Password;
    string? Addres;
    double? Latitude;
    double? Longitude;
    Enum Role;
    Boolean Active;
    double? MaximumDistance;
    Enum DistanceType;

    public Volunteer() { }

}
