using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlImplementation;
using BlApi;

/// <summary>
/// the implementation of the interface IBl
/// </summary>
internal class BI : IBl
{
    public IAdmin Admin { get; } = new AdminImplementation();
    public IVolunteer Volunteer { get; } = new VolunteerImplementation();
    public ICall Call { get; } = new CallImplementation();

}

