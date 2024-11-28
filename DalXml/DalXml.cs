using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;

/// <summary>
/// creating a pointers into the implemetions function 
/// </summary>
public class DalXml : IDal
{
    public IVolunteer volunteer { get; } = new VolunteerImplementation(); 

    public IAssignment assignment { get; } = new AssignmentImplementation();

    public ICall call { get; } = new CallImplementation();

    public IConfig config { get; } = new ConfigImplementation();

    // reset all the xml files from the entities.
    public void ResetDB()
    {
        volunteer.DeleteAll();
        assignment.DeleteAll();
        call.DeleteAll();
        config.Reset();
    }
}
