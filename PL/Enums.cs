using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;


/// <summary>
/// define the character of the Volunteer
/// </summary>
public class VolunteerListCollection : IEnumerable
{
    static readonly IEnumerable<BO.VollInListFilter> s_enums =
        (Enum.GetValues(typeof(BO.VollInListFilter)) as IEnumerable<BO.VollInListFilter>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

/// <summary>
/// define the character of the Volunteer
/// </summary>
public class RoleCollection : IEnumerable
{
    static readonly IEnumerable<BO.BRoles> s_enums =
        (Enum.GetValues(typeof(BO.BRoles)) as IEnumerable<BO.BRoles>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

/// <summary>
/// define in what means the distance get measured
/// </summary>
public class DistanceTypeCollection : IEnumerable
{
    static readonly IEnumerable<BO.BDistanceTypes> s_enums =
        (Enum.GetValues(typeof(BO.BDistanceTypes)) as IEnumerable<BO.BDistanceTypes>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
