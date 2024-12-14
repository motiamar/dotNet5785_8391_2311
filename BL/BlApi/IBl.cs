using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi;

/// <summary>
/// the interface of the BL
/// </summary>
public interface IBl
{
    IAdmin Admin { get; }
    IVolunteer Volunteer { get; }
    ICall Call { get; } 
}
