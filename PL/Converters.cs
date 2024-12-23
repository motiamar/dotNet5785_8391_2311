using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
namespace PL;

class ConverterRoleToColor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        BO.BRoles role = (BO.BRoles)value;
        switch (role)
        {
            case BO.BRoles.Manager:
                return Brushes.Red;
            case BO.BRoles.Volunteer:
                return Brushes.Yellow;      
            default:
                return Brushes.Yellow; 
        }   
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// indicate if the id is 0 or not
/// </summary>
public class IdToReadOnlyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int id)
        {
            return id != 0;
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

