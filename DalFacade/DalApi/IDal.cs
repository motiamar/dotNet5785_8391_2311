using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DalApi;

/// <summary>
/// an interfce that alow to use the entities 
/// </summary>
public interface IDal
{
    IVolunteer volunteer { get; }
    IAssignment assignment { get; }
    ICall call { get; } 
    IConfig config { get; }

    void ResetDB();
}
