using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;

/// <summary>
/// creating a pointers into the implemetions function 
/// </summary>
sealed internal class DalXml : IDal
{
    // we use a lazy singlton with a thread safe 
    private static readonly Lazy<IDal> _instance = new Lazy<IDal>(new DalXml());
    public static IDal Instance => _instance.Value;
    private DalXml() { }
    public IVolunteer Volunteer { get; } = new VolunteerImplementation(); 

    public IAssignment Assignment { get; } = new AssignmentImplementation();

    public ICall Call { get; } = new CallImplementation();

    public IConfig Config { get; } = new ConfigImplementation();

    // Reset all the xml files from the entities.
    public void ResetDB()
    {
        Volunteer.DeleteAll();
        Assignment.DeleteAll();
        Call.DeleteAll();
        Config.Reset();
    }
}
