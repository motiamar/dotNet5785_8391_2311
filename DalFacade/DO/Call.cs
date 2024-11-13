
namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">uniquely readable ID representation</param>
/// <param name="TypeCall">the type of the call to the name of the system</param>
/// <param name="VerbalDecription">a description of the reading received</param>
/// <param name="FullAddressOfTheCall">full address of the call according to the format: street(+number) ,city ,country</param>
/// <param name="Latitude"> number that show the distance between the point and the north or sout of the wquator</param>
/// <param name="Longitude">number that show the distance between the point and the east or west of the wquator</param>
/// <param name="OpeningCallTime">the time when the call was opened by the manager</param>
/// <param name="MaxEndingCallTime">the maximom time a call should close</param>
public record Call
(
    int Id,
    Enum TypeCall = default,
    string? VerbalDecription = null,
    string FullAddressOfTheCall = " ",
    double Latitude = 0,
    double Longitude = 0,
    DateTime OpeningCallTime = default,
    DateTime? MaxEndingCallTime=null
)
{
    public Call() :this (0) { }
}
