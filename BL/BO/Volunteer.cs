namespace BO;

/// <summary>
/// represent a volunteer in the system. entity the see, update, add and delete.
/// </summary>
/// <param name="Id">uniquely readable ID representation</param>
/// <param name="FullName"> the full name of the volunteer</param>
/// <param name="Phone">the phone number of the volunteer</param>
/// <param name="Email">the email of the volunteer</param>
/// <param name="password">the password of the volunteer</param>
/// <param name="FullAddressOfTheCall">full address of the volunteer according to the format: street(+number) ,city ,country</param>
/// <param name="Latitude"> number that show the distance between the point and the north or sout of the wquator</param>
/// <param name="Longitude">number that show the distance between the point and the east or west of the wquator</param>
/// <param name="Role">indicate if the volunteer is a managar of not</param>
/// <param name="Active">indicate if the volunteer is active right now or not</param>
/// <param name="MaximumDistance"> the maximum distance that the volunteer can reach</param>
/// <param name="DistanceType">tells by what means the distance majar, air,car or walk </param>
/// <param name="TreatedCalls"> tells the amount of calls that this volunteer treated</param>
/// <param name="CanceledCalls">tells the amount of calls that this volunteer cenceld</param>
/// <param name="ExpiredCalls">tells the amount of calls that this volunteer choose and get expierd</param>
/// <param name="CorrentCall">if there is, it show the corrent call that the volunteer handle</param>

public class Volunteer
{
    public int Id { get; init; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
    public string? Address { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public BRoles role { get; set; }
    public Boolean Active { get; set; }
    public double? MaximumDistance { get; set; }
    public BDistanceTypes DistanceType { get; set; }
    public int TreatedCalls { get; set; }
    public int CanceledCalls { get; set; }
    public int ExpiredCalls { get; set; }
    public BO.CallInProgress? CorrentCall { get; set; }
    public override string ToString() => this.TostringProperty();   
}