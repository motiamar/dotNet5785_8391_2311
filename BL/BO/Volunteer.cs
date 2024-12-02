//using Helpers;
namespace BO;
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

    //public BO.CallInProgress? A;
    public override string ToString() => this.TostringProperty();   
}