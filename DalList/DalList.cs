using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalList : IDal
{
    
    public static IDal Instance { get; } = new DalList();
    private DalList() { }

    public IVolunteer volunteer { get; } = new VolunteerImplementation();
    public IAssignment assignment { get; } = new AssignmentImplementation();
    public ICall call { get; } = new CallImplementation();
    public IConfig config {  get; } = new ConfigImplementation();

    public void ResetDB()
    {
        volunteer.DeleteAll();
        assignment.DeleteAll();
        call.DeleteAll();
        config.Reset();
    }
}
