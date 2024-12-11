namespace BlImplementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
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
    /// create a DO.Volunteer entity if the id is not exist in the data base 
    /// </summary>
    public void Create(BO.Volunteer volunteer)
    {
        try
        {
            // check the incoming details
            Helpers.VolunteerManager.VolunteerChek(volunteer);
            double? latitude = Helpers.Tools.GetLatitudeFromAddress(volunteer.Address!);
            double? longitude = Helpers.Tools.GetLongitudeFromAddress(volunteer.Address!);
            var role = (Roles)Enum.Parse(typeof(BRoles), volunteer.role.ToString());
            var distanceType = (DistanceTypes)Enum.Parse(typeof(BDistanceTypes), volunteer.DistanceType.ToString());
            var newVolunteer = new DO.Volunteer { Id = volunteer.Id, FullName = volunteer.FullName, Phone = volunteer.Phone, Email = volunteer.Email, Password = volunteer.Password, Address = volunteer.Address, Role = role, Latitude = latitude, Longitude = longitude, Active = volunteer.Active, MaximumDistance = volunteer.MaximumDistance, DistanceType = distanceType };
            _dal.Volunteer.Create(newVolunteer);
        }
        catch ( DO.DalAlreadyExistException ex)
        {
            throw new BlVolAllreadyExist($"volunteer with id : {volunteer.Id} is already exist: {ex}");
        }
    }

    /// <summary>
    /// delete the volunteer by the id if is not assignd now or ever
    /// </summary>
    public void Delete(int volunteerId)
    {
        try
        {
            var volunteer = _dal.Volunteer.Read(volunteerId);
            var assignment = _dal.Assignment.ReadAll();
            int? correntCallId = assignment.FirstOrDefault(v => v.VolunteerId == volunteerId)?.CallId;
            if (correntCallId is null)
                _dal.Volunteer.Delete(volunteerId);
            else
                throw new BLVolunteerIsAssign($"cant delete volunteer: {volunteerId}, the volunteer is assign");
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"can't find volunteer with id : {volunteerId} : {ex}");
        }
    }

    /// <summary>
    /// func to update the datiles of an existing volunteer
    /// </summary>
    public void  Update(int volunteerId, BO.Volunteer change)
    {
        try
        {
            var volunteer = _dal.Volunteer.Read(volunteerId);
            if (volunteer == null)
                throw new BlDoesNotExistException($"can't find volunteer with id : {volunteerId}");
            if (volunteerId != change.Id && volunteer!.Role != Roles.Manager)
                throw new BlNotAllowException("you can't change the details of the volunteer");
            // func to chek all the incoming details    
            Helpers.VolunteerManager.VolunteerChek(change);
            double? latitude = Helpers.Tools.GetLatitudeFromAddress(change.Address!);
            double? longitude = Helpers.Tools.GetLongitudeFromAddress(change.Address!);
            var role = (Roles)Enum.Parse(typeof(BRoles), change.role.ToString());
            var distanceType = (DistanceTypes)Enum.Parse(typeof(BDistanceTypes), change.DistanceType.ToString());
            if (volunteer.Role == Roles.Manager)
            {
                // if the volunteer is a manager is can change the role
                var newVolunteer = new DO.Volunteer { Id = volunteer.Id, FullName = change.FullName, Phone = change.Phone, Email = change.Email, Password = change.Password, Address = change.Address, Role = role, Latitude = latitude, Longitude = longitude, Active = change.Active, MaximumDistance = change.MaximumDistance, DistanceType = distanceType};
                _dal.Volunteer.Update(newVolunteer);
            }
            else
            {
                //if the volunteer is not a manager is can't change the role
                var newVolunteer = new DO.Volunteer { Id = volunteer.Id, FullName = change.FullName, Phone = change.Phone, Email = change.Email, Password = change.Password, Address = change.Address, Role = volunteer.Role, Latitude = latitude, Longitude = longitude, Active = change.Active, MaximumDistance = change.MaximumDistance, DistanceType = distanceType };
                _dal.Volunteer.Update(newVolunteer);
            }              
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"can't find volunteer with id : {volunteerId} : {ex}");
        }
        catch (DO.DalXMLFileLoadCreateException)
        {
            throw new BlCantLoadException("can't load the volunteers");
        }
    }

    /// <summary>
    /// return the BO.Volunteer entity by the id
    /// </summary>
    BO.Volunteer? BlApi.IVolunteer.Read(int id)
    {
        try
        {
            if(_dal.Volunteer.Read(id) != null)
            {
                BO.Volunteer volunteer = Helpers.VolunteerManager.GetBOVolunteer(id);
                return volunteer;
            }
            throw new BlDoesNotExistException($"can't find volunteer with id : {id}");           
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"can't find volunteer with id : {id} : {ex}");
        }
    }
}
