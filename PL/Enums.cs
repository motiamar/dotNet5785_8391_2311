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
public class VolunteerListFilterCollection : IEnumerable
{
    static readonly IEnumerable<BO.BTypeCalls> s_enums =
        (Enum.GetValues(typeof(BO.BTypeCalls)) as IEnumerable<BO.BTypeCalls>)!;
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

/// <summary>
/// define the sort of call that enterd the system
/// </summary>
public class CallInListCollection : IEnumerable
{
    static readonly IEnumerable<BO.CallInListFilter> s_enums =
        (Enum.GetValues(typeof(BO.CallInListFilter)) as IEnumerable<BO.CallInListFilter>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}


public class CloseCallInListCollection : IEnumerable
{
    static readonly IEnumerable<BO.CloseCallInListFilter> s_enums =
        (Enum.GetValues(typeof(BO.CloseCallInListFilter)) as IEnumerable<BO.CloseCallInListFilter>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
/// <summary>
/// define the type of the call
/// </summary>
public class CallTypeListCollection : IEnumerable
{
    static readonly IEnumerable<BO.BTypeCalls> s_enums =
        (Enum.GetValues(typeof(BO.BTypeCalls)) as IEnumerable<BO.BTypeCalls>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

