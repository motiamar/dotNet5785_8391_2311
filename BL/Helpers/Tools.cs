using DO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
namespace Helpers;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using BO;


internal static class Tools
{
    private static readonly HttpClient client = new HttpClient();
    private const string NominatimApiUrl = "https://nominatim.openstreetmap.org/search?format=json&q={0}";

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
                result.AppendLine("]");
            }
            else
                result.AppendLine($"{prop.Name}:{value}");
        }
        return result.ToString();
    }

    /// <summary>
    /// func to calculate the distance between the volunteer and the call by Latitude and Longitude depend on the distance type
    /// </summary>
    /// <returns></returns>
    public static double Distance(DO.DistanceTypes distanceType, double VolLatitude, double VolLongitude, double CallLatitude, double CallLongitude)
    {
        return 0.0;
    }


    /// <summary>
    /// func to check if the address is valid (exist on arth by google maps)
    /// </summary>
    public static bool IsValidAddress(string address)
    {
        //try
        //{
        //   string requestUri = string.Format(NominatimApiUrl, Uri.EscapeDataString(address));
        //    client.DefaultRequestHeaders.Add("User-Agent", "CSharpApp/1.0");
        //    HttpResponseMessage response = client.GetAsync(requestUri).Result;
        //    if(response.IsSuccessStatusCode)
        //    {
        //        string content = response.Content.ReadAsStringAsync().Result;
        //        JArray responseArray = JArray.Parse(content);
        //        return responseArray.Count > 0;              
        //    }
        //    return false;
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("Error: " + ex.Message);
        //    return false;
        //}
        return true;
    }


    /// <summary>
    /// func to get the latitude of the address
    /// </summary>
    public static double GetLatitudeFromAddress(string address)
    {
        //try
        //{
            
        //}
        //catch (Exception ex)
        //{
        //    // במקרה של שגיאה כלשהי (כמו בעיית חיבור)
        //    Console.WriteLine("Error: " + ex.Message);
        //}
        return 0;
    }

    /// <summary>
    /// func to get the longitude of the address
    /// </summary>
    public static double GetLongitudeFromAddress(string address)
    {
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        // במקרה של שגיאה כלשהי (כמו בעיית חיבור)
        //        Console.WriteLine("Error: " + ex.Message);
        //    }
        return 0;
    }

};
    


