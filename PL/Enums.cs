using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
namespace PL;


/// <summary>
/// define the character of the Volunteer
/// </summary>
public partial class VolunteersListColelction : IEnumerable
{
    static readonly IEnumerable<BO.VollInListFilter> s_enums =
        (Enum.GetValues(typeof(BO.VollInListFilter)) as IEnumerable<BO.VollInListFilter>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

/// <summary>
/// define the character of the Volunteer
/// </summary>
public partial class RoleColelction : IEnumerable
{
    static readonly IEnumerable<BO.BRoles> s_enums =
        (Enum.GetValues(typeof(BO.BRoles)) as IEnumerable<BO.BRoles>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

/// <summary>
/// define in what means the distance get measured
/// </summary>
public partial class DistanceTypeColelction : IEnumerable
{
    static readonly IEnumerable<BO.BDistanceTypes> s_enums =
        (Enum.GetValues(typeof(BO.BDistanceTypes)) as IEnumerable<BO.BDistanceTypes>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}



