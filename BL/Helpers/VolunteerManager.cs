using DalApi;
namespace Helpers;

internal static class VolunteerManager
{
    private static IDal s_dal = Factory.Get;

    public static string? GetVolunteerRole(string username, string password)
    {
        IEnumerable<DO.Volunteer> volunteers = s_dal.Volunteer.ReadAll();
        var volunteer = volunteers.FirstOrDefault(v => v.FullName == username && v.Password == password);
        if (volunteer != null)
        {
            return volunteer.Role.ToString();
        }
        return null;
    }
}
