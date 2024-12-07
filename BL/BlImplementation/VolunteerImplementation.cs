namespace BlImplementation;
using System.Collections.Generic;
using BlApi;
using BO;
using static BO.Exceptions;

internal class VolunteerImplementation : IVolunteer
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    public void Create(Volunteer volunteer)
    {
        throw new NotImplementedException();
    }

    public void Delete(int volunteerId)
    {
        throw new NotImplementedException();
    }

    public Volunteer? Read(int id)
    {
    //    var volunteer = _dal.Volunteer.Read(id);
    //    if (volunteer != null)
    //    {
    //        return volunteer;
    //    }
    //    else
    //    {
    //        throw new BlDoesNotExistException($"can't find volunteer with id : {id}");
    //    }
        throw new NotImplementedException();
    }

    public IEnumerable<VolunteerInList> ReadAll(bool? active = null, CallInListFilter? sort = null)
    {
        try
        {
            var volunteers = _dal.Volunteer.ReadAll(v => v.Active == active);
            
            
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"can't find any volunteers : {ex}");
        }
        throw new NotImplementedException();
    }

    public string SystemEnter(string username, string password)
    {
        try
        {
            string? role = Helpers.VolunteerManager.GetVolunteerRole(username, password);
            if (role != null)
            {
                return role;
            }
            else
            {
                throw new BlDoesNotExistException($"can't find username : {username} with password : {password}");
            }
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"can't find {username} : {ex}");
        }

    }

    public void Update(int volunteerId, Volunteer change)
    {
        throw new NotImplementedException();
    }
}
