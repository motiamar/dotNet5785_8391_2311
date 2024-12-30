using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
namespace PL;

/// <summary>
/// if the Role is Manager return Red, else return Yellow
/// </summary>
public class ConverterRoleToColor : IValueConverter
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
/// indicate if the id is 0 or not and return true or false
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


/// <summary>
/// if there is a call in progress return the call to string else return empty string
/// </summary>
public class CallInProgressToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is BO.CallInProgress call)
        {
            return $"Assign id: {call.Id} id: {call.CallId} Type: {call.Type}\n Description: {call.Description}\n Address: {call.CallAddress} Open time: {call.CallOpenTime}\n Max time to close: {call.CallMaxCloseTime} Enter time: {call.CallEnterTime}\n Distance: {call.CallDistance} Status: {call.CallStatus}";
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return null!;
    }
}

/// <summary>
/// if there is a current call is return true else return false
/// </summary>
public class NullToEnabledConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value == null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

