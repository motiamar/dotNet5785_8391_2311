using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace PL;

/// <summary>
/// define the character of the Volunteer
/// </summary>
internal class VolunteersListColelction : IEnumerable
{
    static readonly IEnumerable<BO.VollInListFilter> s_enums =
        (Enum.GetValues(typeof(BO.VollInListFilter)) as IEnumerable<BO.VollInListFilter>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

/// <summary>
/// define the character of the Volunteer
/// </summary>
internal class RoleColelction : IEnumerable
{
    static readonly IEnumerable<BO.BRoles> s_enums =
        (Enum.GetValues(typeof(BO.BRoles)) as IEnumerable<BO.BRoles>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

/// <summary>
/// define in what means the distance get measured
/// </summary>
internal class DistanceTypeColelction : IEnumerable
{
    static readonly IEnumerable<BO.BDistanceTypes> s_enums =
        (Enum.GetValues(typeof(BO.BDistanceTypes)) as IEnumerable<BO.BDistanceTypes>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

