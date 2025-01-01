namespace BlImplementation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Numerics;
using System.Security.Cryptography;
using BlApi;
using BO;
using DalApi;
using DO;
using Helpers;
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
    /// <param name="active"> return a list of only active volunteers or not</param>
    /// <param name="sort">return a list sorted by volunteers Id or by the enum fild</param>
    /// <returns></returns>
    public IEnumerable<VolunteerInList> ReadAll(bool? active = null, VollInListFilter? sort = null)
    {
        try
        {
            IEnumerable<VolunteerInList> volunteerInLists = Helpers.VolunteerManager.GetAllVolunteerInList();           
            if (active != null)
            {
                volunteerInLists = volunteerInLists.Where(v => v.Active == active);
            }
            if (sort == null)
                return volunteerInLists.OrderBy(v => v.Id);
            switch (sort)
            {
                case VollInListFilter.FullName:
                    return volunteerInLists.OrderBy(v => v.FullName);
                case VollInListFilter.TreatedCalls:
                    return volunteerInLists.OrderBy(v => v.TreatedCalls);
                case VollInListFilter.CanceledCalls:
                    return volunteerInLists.OrderBy(v => v.CanceledCalls);
                case VollInListFilter.ExpiredCalls:
                    return volunteerInLists.OrderBy(v => v.ExpiredCalls);
                case VollInListFilter.CorrentCallId:
                    return volunteerInLists.OrderBy(v => v.CorrentCallId);
                default:
                    return volunteerInLists.OrderBy(v => v.Id);
            }
        }
        catch (DO.DalXMLFileLoadCreateException ex)
        {
            throw new BlDoesNotExistException($"can't find any volunteers : {ex}");
        }
    }

    /// <summary>
    /// return the BO.Volunteer entity by the id
    /// </summary>
    BO.Volunteer? BlApi.IVolunteer.Read(int id)
    {
        try
        {
            if (_dal.Volunteer.Read(id) != null)
            {
                BO.Volunteer volunteer = Helpers.VolunteerManager.GetBOVolunteer(id);
                return volunteer;
            }
            throw new BlDoesNotExistException($"can't find volunteer with id : {id}");
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"can't find volunteer with id : {id} : {ex}");
        }
    }

    /// <summary>
    /// func to update the datiles of an existing volunteer
    /// </summary>
    public void Update(int volunteerId, BO.Volunteer change)
    {
        try
        {
            var volunteer = _dal.Volunteer.Read(volunteerId);
            if (volunteer == null)
                throw new BlDoesNotExistException($"can't find volunteer with id : {volunteerId}");
            if (volunteerId != change.Id && volunteer!.Role != Roles.Manager)
                throw new BlNotAllowException("you can't change the details of the volunteer");
            var volunteers = _dal.Volunteer.ReadAll(v=> v.Role == Roles.Manager);
            if (volunteer.Role == Roles.Manager && change.role == BRoles.Volunteer && volunteers == null)
                throw new BlNotAllowException("you can't change the role of the manager becose there is no more managers");
            // func to chek all the incoming details    
            Helpers.VolunteerManager.VolunteerChek(change);
            double? latitude = Helpers.Tools.GetLatitudeFromAddress(change.Address!);
            double? longitude = Helpers.Tools.GetLongitudeFromAddress(change.Address!);
            var role = (Roles)Enum.Parse(typeof(BRoles), change.role.ToString());
            var distanceType = (DistanceTypes)Enum.Parse(typeof(BDistanceTypes), change.DistanceType.ToString());
            var newVolunteer = new DO.Volunteer { Id = volunteer.Id, FullName = change.FullName, Phone = change.Phone, Email = change.Email, Password = change.Password, Address = change.Address, Role = role, Latitude = latitude, Longitude = longitude, Active = change.Active, MaximumDistance = change.MaximumDistance, DistanceType = distanceType };
            _dal.Volunteer.Update(newVolunteer);
            VolunteerManager.Observers.NotifyItemUpdated(volunteerId);
            VolunteerManager.Observers.NotifyListUpdated();
            CallManager.Observers.NotifyListUpdated();
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
            {
                _dal.Volunteer.Delete(volunteerId);
                VolunteerManager.Observers.NotifyListUpdated();
            }
            else
                throw new BLVolunteerIsAssign($"cant delete volunteer: {volunteerId}, the volunteer is assign");

        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"can't find volunteer with id : {volunteerId} : {ex}");
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
            VolunteerManager.Observers.NotifyListUpdated();
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BlVolAllreadyExist($"volunteer with id : {volunteer.Id} is already exist: {ex}");
        }
    }


    /// <summary>
    /// func that return a sorted list of the volunteers in list, sorted by filter Booleany and a enum fild 
    /// </summary>
    /// <param name="value"> a parameter to srot for</param>
    /// <param name="filter">return a list sorted by volunteers Id or by the enum fild</param>
    /// <returns></returns>
    public IEnumerable<VolunteerInList> ReadAllScreen(VollInListFilter? sort = null, VollInListFilter? filter = null, object? value = null)
    {
        try
        {
            IEnumerable<VolunteerInList> volunteerInLists = Helpers.VolunteerManager.GetAllVolunteerInList();
            if (filter != null && value != null)
            {
                switch (filter)
                {
                    case VollInListFilter.Active:
                        volunteerInLists = volunteerInLists.Where(v => v.Active == (bool)value);
                        break;
                    case VollInListFilter.CorrentCallId:
                        volunteerInLists = volunteerInLists.Where(v => v.CorrentCallId == (int)value);
                        break;
                    case VollInListFilter.CorrentCallType:
                        volunteerInLists = volunteerInLists.Where(v => v.CorrentCallType == (BTypeCalls)value);
                        break;
                    default:
                        break;
                }
            }
            if (sort == null)
                return volunteerInLists.OrderBy(v => v.Id);
            switch (sort)
            {
                case VollInListFilter.FullName:
                    return volunteerInLists.OrderBy(v => v.FullName);
                case VollInListFilter.TreatedCalls:
                    return volunteerInLists.OrderBy(v => v.TreatedCalls);
                case VollInListFilter.CanceledCalls:
                    return volunteerInLists.OrderBy(v => v.CanceledCalls);
                case VollInListFilter.ExpiredCalls:
                    return volunteerInLists.OrderBy(v => v.ExpiredCalls);
                case VollInListFilter.CorrentCallId:
                    return volunteerInLists.OrderBy(v => v.CorrentCallId);
                default:
                    return volunteerInLists.OrderBy(v => v.Id);
            }
        }
        catch (DO.DalXMLFileLoadCreateException ex)
        {
            throw new BlDoesNotExistException($"can't find any volunteers : {ex}");
        }
    }


    public void AddObserver(Action listObserver)=> VolunteerManager.Observers.AddListObserver(listObserver);

    public void AddObserver(int id, Action observer) => VolunteerManager.Observers.AddObserver(id, observer);


    public void RemoveObserver(Action listObserver) => VolunteerManager.Observers.RemoveListObserver(listObserver);

    public void RemoveObserver(int id, Action observer) => VolunteerManager.Observers.RemoveObserver(id, observer);

}
