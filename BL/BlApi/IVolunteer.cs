using System.Data.SqlTypes;
namespace BlApi;

public interface IVolunteer : IObservable
{
    /// <summary>
    /// method to enter the system
    /// </summary>
    /// <returns>the role of the Volunteer and exeption if he doesn't exist or the password is wrong </returns>
    string SystemEnter(string username, string password);

    /// <summary>
    /// func that return a sorted list of the volunteers in list, sorted by filter Booleany and a enum fild 
    /// </summary>
    /// <param name="filter"> return a list of only active volunteers or not</param>
    /// <param name="select">return a list sorted by volunteers Id or by the enum fild</param>
    /// <returns></returns>
    IEnumerable<BO.VolunteerInList> ReadAll(bool? active = null, BO.VollInListFilter? sort = null);


    /// <summary>
    /// func that return a sorted list of the volunteers in list, sorted by filter Booleany and a enum fild 
    /// </summary>
    /// <param name="value"> a parameter to srot for</param>
    /// <param name="filter">return a list sorted by volunteers Id or by the enum fild</param>
    /// <returns></returns>
    IEnumerable<BO.VolunteerInList> ReadAllScreen(object? value = null, BO.VollInListFilter? filter = null);

    /// <summary>
    /// create a BO.Volunteer entity if the id exist in the data base 
    /// </summary>
    /// <param name="id"> get the id of the Volunteer</param>
    /// <returns> return the copy of the entity by BO.Volunteer entity if it exist</returns>
    BO.Volunteer? Read(int id);

    /// <summary>
    /// func to Update an exist Volunteer by the Manager or the self Volunteer
    /// </summary>
    /// <param name="volunteerId"> the Id of the Volunteer</param>
    /// <param name="change">the new entity we want to switch to</param>
    void Update(int volunteerId, BO.Volunteer change);

    /// <summary>
    /// Delete the Volunteer if it exist and not hendel a call right now or never 
    /// </summary>
    void Delete (int volunteerId);

    /// <summary>
    /// Add a new Volunteer to the data leir if the details are correct
    /// </summary>
    void Create(BO.Volunteer volunteer);




}
