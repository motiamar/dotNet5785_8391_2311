using DO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
namespace Helpers;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


internal static class Tools
{
    // filed for the address cheking - API key
    private const string GoogleMapsApiUrl = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key=AIzaSyDzEzuNVPxLv7EIKKvSU2b8GiAikFbV5jk"; 
    private static readonly HttpClient client = new HttpClient();

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
            result.AppendLine($"{prop.Name}:{value}");
        }
        return result.ToString();
    }

    /// <summary>
    /// func to calculate the distance between the volunteer and the call by Latitude and Longitude depend on the distance type
    /// </summary>
    /// <returns></returns>
    public static double Distance(DO.DistanceTypes distanceType, double? VolLatitude, double? VolLongitude, double CallLatitude, double CallLongitude)
    {
        return 0.0;
    }

    
    /// <summary>
    /// func to check if the address is valid (exist on arth by google maps)
    /// </summary>
    public static bool IsValidAddress(string address)
    {
        try
        {
            string requestUrl = string.Format(GoogleMapsApiUrl, Uri.EscapeDataString(address));
            HttpResponseMessage response = client.GetAsync(requestUrl).Result; // block the result until it return
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result; // block the result until it return
                JObject jsonResponse = JObject.Parse(content);
                var results = jsonResponse["results"];
                return results!.HasValues;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }


    /// <summary>
    /// func to get the latitude of the address
    /// </summary>
    public static double GetLatitudeFromAddress(string address)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = string.Format(GoogleMapsApiUrl, Uri.EscapeDataString(address));
                HttpResponseMessage response = client.GetAsync(requestUrl).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    JObject json = JObject.Parse(content);
                    var results = json["results"];
                    if (results!.HasValues)
                    {
                        var geometry = results[0]!["geometry"];
                        var location = geometry!["location"];
                        double latitude = location!["lat"]!.ToObject<double>();
                        return latitude;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // במקרה של שגיאה כלשהי (כמו בעיית חיבור)
            Console.WriteLine("Error: " + ex.Message);
        }
        return 0;
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
                string requestUrl = string.Format(GoogleMapsApiUrl, Uri.EscapeDataString(address));
                HttpResponseMessage response = client.GetAsync(requestUrl).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    JObject json = JObject.Parse(content);
                    var results = json["results"];
                    if (results!.HasValues)
                    {
                        var geometry = results[0]!["geometry"];
                        var location = geometry!["location"];
                        double longitude = location!["lng"]!.ToObject<double>();
                        return longitude;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // במקרה של שגיאה כלשהי (כמו בעיית חיבור)
            Console.WriteLine("Error: " + ex.Message);
        }
        return 0;
    }

};
    


