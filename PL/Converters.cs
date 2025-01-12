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

/// <summary>
/// if the value is true return true else return false
/// </summary>
public class BoolToEnabledConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is bool isActive && isActive;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return false;
    }
}



/// <summary>
/// if the call status is open return true else return false
/// </summary>
public class EnumToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string enumValue = value.ToString()!;
        if (enumValue == "Open")
            return Visibility.Visible;
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// if the call status is open return true else return false
/// </summary>
public class EnumToVisibilityConverter2 : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string enumValue = value.ToString()!;
        if (enumValue == "In_treatment" || enumValue == "In_treatment_in_risk")
            return Visibility.Visible;
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// if the value is true return false else return true
/// </summary>
public class MultiConditionToEnabledConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 2)
        {
            bool isActive = values[0] is bool active && active;
            bool hasCurrentCall = values[1] == null;

            return isActive && hasCurrentCall; 
        }
        return false; 
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
    
}


/// <summary>
/// if there is a current call is return true else return false
/// </summary>
public class NullToNotEnabledConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value != null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts a list of calls to a formatted string. If the list is empty or null, returns an empty string.
/// </summary>
public class CallAssignInListsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is List<BO.CallAssignInList> calls && calls.Count > 0)
        {
            return string.Join("\n\n", calls.Select(call =>
                $"Volunteer id: {call.VolunteerId} \n" +
                $"Volunteer name: {call.VolunteerFullName} \n" +
                $"Enter time: {call.CallEnterTime} \n" +
                $"Close Time: {call.CallCloseTime} \n" +
                $"End Kind: {call.EndKind}"
            ));
        }
        return string.Empty; 
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException(); 
    }
}

/// <summary>
/// if the call status is open or open_in_risk return true else return false
/// </summary>
public class StatusToEnableConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string statusValue = value.ToString()!;
        if (statusValue == "Open" || statusValue == "Open_in_risk")
            return true;
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// if the call status is open or open_in_risk return true else return false
/// </summary>
public class MaxTimeEnableConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string statusValue = value.ToString()!;
        if (statusValue == "Closed" || statusValue == "Expired")
            return false;
        return true;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class DateTimeToDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            return dateTime.Date; // מחזיר רק את התאריך
        }
        return null!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime date)
        {
            return date; // מחזיר את התאריך כפי שהוא
        }

        // אם ה-Value הוא null, מחזיר את התאריך הנוכחי כברירת מחדל
        return DateTime.Now.Date;
    }
}


public class DateTimeToTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            return dateTime.ToString("HH:mm"); // מחזיר את השעה בפורמט HH:mm
        }
        return null!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string timeString && DateTime.TryParseExact(timeString, "HH:mm", culture, DateTimeStyles.None, out DateTime time))
        {
            // במקרה של זמן בלבד, מחזיר את השעה משולבת עם התאריך הנוכחי
            return DateTime.Today.Add(time.TimeOfDay);
        }

        // ברירת מחדל במקרה של שגיאה: השעה הנוכחית
        return DateTime.Now;
    }
}