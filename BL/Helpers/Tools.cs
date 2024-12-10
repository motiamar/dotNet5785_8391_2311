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
    public static double Distance(DO.DistanceTypes distanceType, double VolLatitude, double VolLongitude, double CallLatitude, double CallLongitude)
    {
        return 0.0;
    }

    /// <summary>
    /// func to check if the id is valid
    /// </summary>
    public static bool VaildId(int id)
    {
        if (id < 100000000 || id > 999999999)
            return false;
        int sum = 0;
        bool even = true;
        for (int i = 0; i < 9; i++)
        {
            int num = id % 10;
            id /= 10;
            if (even)
            {
                sum += num;
            }
            else
            {
                int doubled = num * 2;
                sum += (doubled > 9) ? doubled - 9 : doubled;
            }
            even = !even;
        }
        return sum % 10 == 0;
    }

    /// <summary>
    /// func to check if the phone is valid
    /// </summary>
    public static bool VailPhone(string phone)
    {
        phone = phone.Replace(" ", "");
        string pattern = @"^05[0-9]{8}$";
        return Regex.IsMatch(phone, pattern);
    }

    /// <summary>
    /// func to check if the email is valid
    /// </summary>
    public static bool VaildEmail(string email)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    /// <summary>
    /// func to check if the password is strong enoungh
    /// </summary>
    public static bool VaildPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;
        if (password.Length < 8)
            return false;
        if (!Regex.IsMatch(password, @"[A-Z]"))
            return false;
        if (!Regex.IsMatch(password, @"[a-z]"))
            return false;
        if (!Regex.IsMatch(password, @"[0-9]"))
            return false;
        if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?\""]"))
            return false;
        return true;
    }

    /// <summary>
    /// func to check if the address is valid (exist on arth by google maps)
    /// </summary>
    public static async Task<bool> IsValidAddressAsync(string address)
    {
        if (string.IsNullOrEmpty(address))
            return false;

        try
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = string.Format(GoogleMapsApiUrl, Uri.EscapeDataString(address));
                HttpResponseMessage response = await client.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(content);
                    var results = jsonResponse["results"];
                    if (results.HasValues)
                        return true; 
                    else
                        return false;       
                }
                else
                    return false;                
            }
        }
        catch (Exception)
        {
            return false; 
        }
    }

    /// <summary>
    /// func to get the latitude of the address
    /// </summary>
    public static async Task<double?> GetLatitudeFromAddressAsync(string address)
    {
        using (HttpClient client = new HttpClient())
        {
            string requestUrl = string.Format(GoogleMapsApiUrl, Uri.EscapeDataString(address));
            HttpResponseMessage response = await client.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(content);
                var results = json["results"];
                if (results.HasValues)
                {
                    var geometry = results[0]["geometry"];
                    var location = geometry["location"];
                    double? latitude = location["lat"]?.ToObject<double>();
                    return latitude;
                }
            }
        }
        // אם משהו השתבש, מחזירים null
        return null;
    }

    /// <summary>
    /// func to get the longitude of the address
    /// </summary>
    public static async Task<double?> GetLongitudeFromAddressAsync(string address)
    {
        using (HttpClient client = new HttpClient())
        {
            string requestUrl = string.Format(GoogleMapsApiUrl, Uri.EscapeDataString(address));
            HttpResponseMessage response = await client.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                // קריאת התגובה כ-JSON
                string content = await response.Content.ReadAsStringAsync();

                // ניתוח ה-JSON כדי לחלץ את ה-longitude
                JObject json = JObject.Parse(content);
                var results = json["results"];
                if (results.HasValues)
                {
                    var geometry = results[0]["geometry"];
                    var location = geometry["location"];
                    double? longitude = location["lng"]?.ToObject<double>();
                    return longitude;
                }
            }
        }
        // אם משהו השתבש, מחזירים null
        return null;
    }

    /// <summary>
    /// func to check if the maximum distance is valid
    /// </summary>
    public static bool VaildMaxDistance(double? MaxDistance)
    {
        if (MaxDistance.Value < 0)
            return false;
        return true;
    }


    
}

