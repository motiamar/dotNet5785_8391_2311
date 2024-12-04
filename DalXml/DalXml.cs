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
    public IVolunteer volunteer { get; } = new VolunteerImplementation(); 

    public IAssignment assignment { get; } = new AssignmentImplementation();

    public ICall call { get; } = new CallImplementation();

    public IConfig Config { get; } = new ConfigImplementation();

    // reset all the xml files from the entities.
    public void ResetDB()
    {
        volunteer.DeleteAll();
        assignment.DeleteAll();
        call.DeleteAll();
        Config.Reset();
    }
}
