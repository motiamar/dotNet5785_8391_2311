using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace PL;

internal class VolunteersListColelction : IEnumerable
{
    static readonly IEnumerable<BO.VollInListFilter> s_enums =
        (Enum.GetValues(typeof(BO.VollInListFilter)) as IEnumerable<BO.VollInListFilter>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

