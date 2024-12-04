using System.Data.SqlTypes;

namespace BlApi;

public interface IVolunteer
{
    /// <summary>
    /// method to enter the system
    /// </summary>
    /// <returns>the role of the volunteer and exeption if he doesn't exist or the password is wrong </returns>
    string SystemEnter(string username, string password);

    /// <summary>
    /// func that return a sorted list of the volunteers in list, sorted by filter Booleany and a enum fild 
    /// </summary>
    /// <param name="filter"> return a list of only active volunteers or not</param>
    /// <param name="select">return a list sorted by volunteers Id or by the enum fild</param>
    /// <returns></returns>
    IEnumerable<BO.VolunteerInList> ReadAll(bool? filter = null, BO.BTypeCalls? select = null);

    /// <summary>
    /// create a BO.Volunteer entity if the id exist in the data base 
    /// </summary>
    /// <param name="Id"> get the id of the volunteer</param>
    /// <returns> return the copy of the entity by BO.volunteer entity if it exist</returns>
    BO.Volunteer? Read(int Id);

    /// <summary>
    /// func to update an exist volunteer by the manager or the self volunteer
    /// </summary>
    /// <param name="VolunteerId"> the Id of the volunteer</param>
    /// <param name="change">the new entity we want to switch to</param>
    void Update(int VolunteerId, BO.Volunteer change);

    /// <summary>
    /// delete the Volunteer if it exist and not hendel a call right now or never 
    /// </summary>
    void Delete (int VolunteerId);

    /// <summary>
    /// add a new volunteer to the data leir if the details are correct
    /// </summary>
    void Create(BO.Volunteer volunteer);




}
