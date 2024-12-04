using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalList : IDal
{
    // we use a lazy singlton with a thread safe 
    private static readonly Lazy<IDal> _instance = new Lazy<IDal>(new DalList());
    public static IDal Instance => _instance.Value;
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
