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


    public IVolunteer Volunteer { get; } = new VolunteerImplementation();
    public IAssignment Assignment { get; } = new AssignmentImplementation();
    public ICall Call { get; } = new CallImplementation();
    public IConfig Config {  get; } = new ConfigImplementation();


    /// <summary>
    /// reset all the data base
    /// </summary>
    public void ResetDB()
    {
        Volunteer.DeleteAll();
        Assignment.DeleteAll();
        Call.DeleteAll();
        Config.Reset();
    }
}
