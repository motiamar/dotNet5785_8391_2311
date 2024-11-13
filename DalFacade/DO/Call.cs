
namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">presonal unique id of the volunteer</param>
/// <param name="TypeCall"></param>
/// <param name="VerbalDecription"></param>
/// <param name="FullAddressOfTheCall"></param>
/// <param name="Latitude"> number that show the distance between the point and the north or sout of the wquator</param>
/// <param name="Longitude">number that show the distance between the point and the east or west of the wquator</param>
/// <param name="OpeningTime"></param>
/// <param name="MaxEndingCallTime"></param>
public record Call
{
    int Id;
    Enum TypeCall;
    string? VerbalDecription;
    string FullAddressOfTheCall;
    double Latitude;
    double Longitude;
    DateTime OpeningTime;
    DateTime? MaxEndingCallTime;
}
