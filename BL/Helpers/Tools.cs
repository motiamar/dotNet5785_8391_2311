using DO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
namespace Helpers;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Threading.Tasks;
using BO;
using RestSharp;
using System.Xml;

internal static class Tools
{
    /// <summary>
    /// func to ovveride and print tne logical entities
    /// </summary>
    /// <typeparam name="T">logic entity</typeparam>
    /// <returns> a string of all the prperty</returns>
    public static string TostringProperty<T>(this T t)
    {
        if (t == null)
            return "Null";

        var properties = typeof(T).GetProperties();
        var result = new System.Text.StringBuilder();

        foreach (var prop in properties)
        {
            var value = prop.GetValue(t, null);

            if (value is IEnumerable<CallAssignInList> enumerable && !(value is string))
            {
                result.AppendLine($"{prop.Name}: ");
                foreach (var item in enumerable)
                {
                    result.AppendLine($"  {item}");
                }
                result.AppendLine();
            }
            else if (value is DateTime dateTime)
            {
                result.AppendLine($"{prop.Name} : {dateTime:yyyy-MM-dd HH:mm:ss}"); // פורמט מותאם אישית
            }
            else if (value is TimeSpan timeSpan)
            {
                result.AppendLine($"{prop.Name} : {timeSpan:c}"); // פורמט קומפקטי ל-TimeSpan
            }
            else if (value is Enum enumValue)
            {
                result.AppendLine($"{prop.Name} : {enumValue}"); // ToString() כברירת מחדל
            }
            else if (value != null && value.GetType().IsClass && value.GetType() != typeof(string))
            {
                result.AppendLine($"{prop.Name}: ");
                result.AppendLine(value.TostringProperty()); // קריאה רקורסיבית
            }
            else
            {
                result.AppendLine($"{prop.Name} : {value}");
            }
        }

        return result.ToString();
    }


    /// <summary>
    /// tools for the addreas and the distance
    /// </summary>
    private const double EarthRadiusKm = 6371.0;
    private const string ApiKey = "AIzaSyDzEzuNVPxLv7EIKKvSU2b8GiAikFbV5jk"; //  מפתח-API 


    // <summary>
    /// func to calculate the distance between the volunteer and the call by Latitude and Longitude depend on the distance type
    /// </summary>
    /// <returns></returns>
    public static double Distance(DO.DistanceTypes distanceType, double VolLatitude, double VolLongitude, double CallLatitude, double CallLongitude)
    {
        if (distanceType == DistanceTypes.Air)
        {
            // חישוב מרחק אווירי באמצעות נוסחת Haversine
            double radLat1 = DegreesToRadians(VolLatitude);
            double radLon1 = DegreesToRadians(VolLongitude);
            double radLat2 = DegreesToRadians(CallLatitude);
            double radLon2 = DegreesToRadians(CallLongitude);
            double deltaLat = radLat2 - radLat1;
            double deltaLon = radLon2 - radLon1;

            double a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                       Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(deltaLon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c; // המרחק בקילומטרים
        }

        if (distanceType == DistanceTypes.Car || distanceType == DistanceTypes.Walk)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // מצב נסיעה (driving או walking)
                    string mode = distanceType == DistanceTypes.Car ? "driving" : "walking";

                    // בניית URL לבקשה ל-Google Distance Matrix API בפורמט XML
                    string requestUrl = $"https://maps.googleapis.com/maps/api/distancematrix/xml?origins={VolLatitude},{VolLongitude}" +
                                        $"&destinations={CallLatitude},{CallLongitude}&mode={mode}&key={ApiKey}";

                    // שליחת הבקשה וקבלת תגובה
                    HttpResponseMessage response = client.GetAsync(requestUrl).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Error: API returned status code {response.StatusCode}");
                        return double.NaN;
                    }

                    // קריאת התוכן בפורמט XML
                    string xmlResponse = response.Content.ReadAsStringAsync().Result;

                    // ניתוח התגובה ב-XML
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlResponse);

                    // בדיקת סטטוס
                    XmlNode statusNode = xmlDoc.SelectSingleNode("//status")!;
                    if (statusNode == null || statusNode.InnerText != "OK")
                    {
                        Console.WriteLine($"Error: API response status is {statusNode?.InnerText}");
                        return double.NaN;
                    }

                    // שליפת מרחק מהתגובה
                    XmlNode distanceNode = xmlDoc.SelectSingleNode("//distance/value")!;
                    if (distanceNode != null && double.TryParse(distanceNode.InnerText, out double distanceInMeters))
                    {
                        return distanceInMeters / 1000; // המרחק בקילומטרים
                    }

                    return double.NaN;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return double.NaN;
            }
        }

        return 0;
    }


    /// <summary>
    /// Convert degrees to radians.
    /// </summary>
    private static double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180.0);
    }



    /// <summary>
    /// func to check if the address is valid (exist on arth by google maps)
    /// </summary>
    public static bool IsValidAddress(string address)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // בניית ה-URL לבקשה
                string requestUrl = $"https://maps.googleapis.com/maps/api/geocode/xml?address={Uri.EscapeDataString(address)}&key=AIzaSyDzEzuNVPxLv7EIKKvSU2b8GiAikFbV5jk";

                // שליחת הבקשה וקבלת תגובה
                HttpResponseMessage response = client.GetAsync(requestUrl).Result;

                if (!response.IsSuccessStatusCode)
                    return false;

                // קריאת התוכן בפורמט XML
                string xmlResponse = response.Content.ReadAsStringAsync().Result;

                // פירוק ה-XML
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlResponse);

                // בדיקת סטטוס
                XmlNode statusNode = xmlDoc.SelectSingleNode("//status")!;
                if (statusNode is null || statusNode.InnerText != "OK")
                    return false;

                // בדיקת אם יש תוצאות
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("result");
                return nodes.Count > 0;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }


    ///// <summary>
    ///// func to get the coordinates of the address
    ///// </summary>
    public static (double, double) GetCoordinatesFromAddress(string address)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // בניית ה-URL לבקשה
                string requestUrl = $"https://maps.googleapis.com/maps/api/geocode/xml?address={Uri.EscapeDataString(address)}&key=AIzaSyDzEzuNVPxLv7EIKKvSU2b8GiAikFbV5jk";

                // שליחת הבקשה וקבלת תגובה
                HttpResponseMessage response = client.GetAsync(requestUrl).Result;

                if (!response.IsSuccessStatusCode)
                    return (double.NaN, double.NaN);

                // קריאת התוכן בפורמט XML
                string xmlResponse = response.Content.ReadAsStringAsync().Result;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlResponse);

                // בדיקת סטטוס
                XmlNode statusNode = xmlDoc.SelectSingleNode("//status")!;
                if (statusNode is null || statusNode.InnerText != "OK")
                    return (double.NaN, double.NaN);

                // שליפת הקואורדינטות
                XmlNode locationNode = xmlDoc.SelectSingleNode("//location")!;
                if (locationNode != null)
                {
                    double lat = double.Parse(locationNode.SelectSingleNode("lat")?.InnerText ?? "NaN");
                    double lon = double.Parse(locationNode.SelectSingleNode("lng")?.InnerText ?? "NaN");
                    return (lat, lon);
                }

                return (double.NaN, double.NaN);
            }
        }
        catch (Exception)
        {
            return (double.NaN, double.NaN);
        }
    }
};



