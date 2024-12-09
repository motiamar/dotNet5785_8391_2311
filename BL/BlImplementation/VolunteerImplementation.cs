namespace BlImplementation;
using System.Collections.Generic;
using System.Security.Cryptography;
using BlApi;
using BO;
using DalApi;
using DO;
using static BO.Exceptions;

internal class VolunteerImplementation : BlApi.IVolunteer
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// method to enter the system
    /// </summary>
    /// <returns>the role of the volunteer and exeption if he doesn't exist or the password is wrong </returns>
    public string SystemEnter(string username, string password)
    {
        try
        {
            string? role = Helpers.VolunteerManager.GetVolunteerRole(username, password);
            if (role != null)
                return role;
            else
                throw new BlDoesNotExistException($"can't find username : {username} with password : {password}");
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"can't find username: {username} : {ex}");
        }
        catch (DO.DalXMLFileLoadCreateException ex)
        {

            throw new BlCantLoadException($"can't load username: {username} : {ex}");

        }
    }

    /// <summary>
    /// func that return a sorted list of the volunteers in list, sorted by filter Booleany and a enum fild 
    /// </summary>
    /// <param name="filter"> return a list of only active volunteers or not</param>
    /// <param name="select">return a list sorted by volunteers Id or by the enum fild</param>
    /// <returns></returns>
    public IEnumerable<VolunteerInList> ReadAll(bool? active = null, CallInListFilter? sort = null)
    {
        try
        {
            IEnumerable<VolunteerInList> volunteerInLists = Helpers.VolunteerManager.GetAllVolunteerInList();
            string Filed = sort.ToString()!;
            var property = typeof(BO.VolunteerInList).GetProperty(Filed);
            volunteerInLists.Where(v => active == null || v.Active == active);
            return sort == null ? volunteerInLists.OrderBy(v => v.Id) : volunteerInLists.OrderBy(v => property!.GetValue(v));
        }
        catch (DO.DalXMLFileLoadCreateException ex)
        {
            throw new BlDoesNotExistException($"can't find any volunteers : {ex}");
        }
    }


    /// <summary>
    /// create a BO.Volunteer entity if the id exist in the data base 
    /// </summary>
    /// <param name="id"> get the id of the volunteer</param>
    /// <returns> return the copy of the entity by BO.volunteer entity if it exist</returns>

    public void Create(BO.Volunteer volunteer)
    {
        throw new NotImplementedException();
    }

    public void Delete(int volunteerId)
    {
        throw new NotImplementedException();
    }

    
    public void Update(int volunteerId, BO.Volunteer change)
    {
        throw new NotImplementedException();
    }

    BO.Volunteer? BlApi.IVolunteer.Read(int id)
    {
        try
        {
            if(_dal.Volunteer.Read(id) != null)
            {
                BO.Volunteer volunteer = Helpers.VolunteerManager.GetBOVolunteer(id);
                return null;
            }
            throw new BlDoesNotExistException($"can't find volunteer with id : {id}");           
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"can't find volunteer with id : {id} : {ex}"););
        }
    }
}
