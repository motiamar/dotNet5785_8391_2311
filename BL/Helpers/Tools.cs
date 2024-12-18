using DO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
namespace Helpers;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json.Linq;
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
            if(value is IEnumerable<CallAssignInList> enumerable && !(value is string))
            {
                result.AppendLine($"{prop.Name}: [");
                foreach (var item in enumerable)
                {
                    result.AppendLine($"{item}");
                }
                result.AppendLine();
            }
            else
                result.AppendLine($"{prop.Name}:{value}");
        }
        return result.ToString();
    }
    private const double EarthRadiusKm = 6371.0;
    private const string ApiKey = "pk.171d9d217781e7387c5cb9df70d511bf"; //  מפתח-API 
    private const string BaseUrl = "https://us1.locationiq.com/v1/search.php";
    /// <summary>
    /// func to calculate the distance between the volunteer and the call by Latitude and Longitude depend on the distance type
    /// </summary>
    /// <returns></returns>
    public static double Distance(DO.DistanceTypes distanceType, double VolLatitude, double VolLongitude, double CallLatitude, double CallLongitude)
    {
        if (distanceType == DistanceTypes.Air)
        {
            double radLat1 = DegreesToRadians(VolLatitude);
            double radLon1 = DegreesToRadians(VolLongitude);
            double radLat2 = DegreesToRadians(CallLatitude);
            double radLon2 = DegreesToRadians(CallLongitude);
            double deltaLat = radLat2 - radLat1;
            double deltaLon = radLon2 - radLon1;

            double a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                       Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(deltaLon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // חישוב המרחק
            double distance = EarthRadiusKm * c;

            return distance; // המרחק בקילומטרים
        }
        if(distanceType == DistanceTypes.Car)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // בניית URL לבקשה עם מצב "car" עבור נסיעה ברכב
                    string requestUrl = $"{BaseUrl}?key={ApiKey}&mode=car&start={VolLatitude},{VolLongitude}&end={CallLatitude},{CallLongitude}&format=xml";

                    // שליחת הבקשה וקבלת התגובה
                    HttpResponseMessage response = client.GetAsync(requestUrl).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Error: API returned status code {response.StatusCode}");
                        return double.NaN;
                    }

                    // קריאת התגובה כ-XML
                    string xmlResponse = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("API Response:");
                    Console.WriteLine(xmlResponse);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlResponse);

                    // שליפת תגית ה-distance מה-XML
                    XmlNode distanceNode = xmlDoc.SelectSingleNode("//distance")!;
                    if (distanceNode != null && double.TryParse(distanceNode.InnerText, out double distance))
                    {
                        return distance / 1000; // המרחק בקילומטרים
                    }
                    else
                    {
                 
                        return double.NaN;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return double.NaN;
            }
        }
        if(distanceType == DistanceTypes.Walk)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // בניית URL לבקשה עם מצב "walk" עבור הליכה רגלית
                    string requestUrl = $"{BaseUrl}?key={ApiKey}&mode=walk&start={VolLatitude},{VolLongitude}&end={CallLatitude},{CallLongitude}&format=xml";
                    Console.WriteLine($"Request URL: {requestUrl}");

                    // שליחת הבקשה וקבלת התגובה
                    HttpResponseMessage response = client.GetAsync(requestUrl).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                       
                        return double.NaN;
                    }

                    // קריאת התגובה כ-XML
                    string xmlResponse = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("API Response:");
                    Console.WriteLine(xmlResponse);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlResponse);

                    // שליפת תגית ה-distance מה-XML
                    XmlNode distanceNode = xmlDoc.SelectSingleNode("//distance")!;
                    if (distanceNode != null && double.TryParse(distanceNode.InnerText, out double distance))
                    {
                        return distance / 1000; // המרחק בקילומטרים
                    }
                    else
                    {
                       
                        return double.NaN;
                    }
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
                string requestUrl = $"{BaseUrl}?key={ApiKey}&q={Uri.EscapeDataString(address)}&format=xml";

                // שליחת הבקשה וקבלת תגובה
                HttpResponseMessage response = client.GetAsync(requestUrl).Result;

                if (!response.IsSuccessStatusCode)
                    return false;

                // קריאת התוכן בפורמט XML
                string xmlResponse = response.Content.ReadAsStringAsync().Result;

                // פירוק ה-XML
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlResponse);

                // בדיקה אם ה-XML מכיל תוצאות
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("place");
                return nodes.Count > 0;
            }

        }
        catch (Exception)
        {
            return false; // חריגה כלשהי - נניח שהכתובת לא תקינה
        }
    }




    /// <summary>
    /// func to get the latitude of the address
    /// </summary>
    public static double GetLatitudeFromAddress(string address)
    {
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string requestUrl = $"{BaseUrl}?key={ApiKey}&q={Uri.EscapeDataString(address)}&format=xml";
                    HttpResponseMessage response = client.GetAsync(requestUrl).Result;

                    if (!response.IsSuccessStatusCode)
                        return double.NaN;

                    string xmlResponse = response.Content.ReadAsStringAsync().Result;

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlResponse);

                    XmlNode node = xmlDoc.SelectSingleNode("//place")!;
                    if (node != null && node.Attributes!["lat"] != null)
                    {
                        return double.Parse(node.Attributes["lat"]!.Value);
                    }
                }
            }
            catch (Exception)
            {
                // במקרה של שגיאה - מחזיר NaN
            }

            return double.NaN;
        }
    }

    /// <summary>
    /// func to get the longitude of the address
    /// </summary>
    public static double GetLongitudeFromAddress(string address)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{BaseUrl}?key={ApiKey}&q={Uri.EscapeDataString(address)}&format=xml";
                HttpResponseMessage response = client.GetAsync(requestUrl).Result;

                if (!response.IsSuccessStatusCode)
                    return double.NaN;

                string xmlResponse = response.Content.ReadAsStringAsync().Result;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlResponse);

                XmlNode node = xmlDoc.SelectSingleNode("//place")!;
                if (node != null && node.Attributes!["lon"] != null)
                {
                    return double.Parse(node.Attributes["lon"]!.Value);
                }
            }
        }
        catch (Exception)
        {
            // במקרה של שגיאה - מחזיר NaN
        }
        return double.NaN;
    }
};
    


