namespace BO;

/// <summary>
/// represent a volunteer in the system. entity the see, update, add and delete.
/// </summary>

public class Volunteer
{
    /// <summary>
    /// uniquely readable ID representation
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// the full name of the volunteer
    /// </summary>
    public string FullName { get; set; }
    /// <summary>
    /// the phone number of the volunteer
    /// </summary>
    public string Phone { get; set; }
    /// <summary>
    /// the email of the volunteer
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// the password of the volunteer
    /// </summary>
    public string? Password { get; set; }
    /// <summary>
    /// full address of the volunteer according to the format: street(+number) ,city ,country
    /// </summary>
    public string? Address { get; set; }
    /// <summary>
    /// number that show the distance between the point and the north or sout of the wquator
    /// </summary>
    public double? Latitude { get; set; }
    /// <summary>
    /// number that show the distance between the point and the east or west of the wquator
    /// </summary>
    public double? Longitude { get; set; }
    /// <summary>
    /// indicate if the volunteer is a managar of not
    /// </summary>
    public BRoles role { get; set; }
    /// <summary>
    /// indicate if the volunteer is active right now or not
    /// </summary>
    public Boolean Active { get; set; }
    /// <summary>
    /// the maximum distance that the volunteer can reach
    /// </summary>
    public double? MaximumDistance { get; set; }
    /// <summary>
    /// tells by what means the distance majar, air,car or walk 
    /// </summary>
    public BDistanceTypes DistanceType { get; set; }
    /// <summary>
    ///  tells the amount of calls that this volunteer treated
    /// </summary>
    public int TreatedCalls { get; set; }
    /// <summary>
    /// tells the amount of calls that this volunteer cenceld
    /// </summary>
    public int CanceledCalls { get; set; }
    /// <summary>
    /// tells the amount of calls that this volunteer choose and get expierd
    /// </summary>
    public int ExpiredCalls { get; set; }
    /// <summary>
    /// if there is, it show the corrent call that the volunteer handle
    /// </summary>
    public BO.CallInProgress? CorrentCall { get; set; }
    public override string ToString() => this.TostringProperty(); 
    
}
