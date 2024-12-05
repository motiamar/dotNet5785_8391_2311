namespace BlImplementation;
using System.Collections.Generic;
using BlApi;
using BO;

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
        throw new NotImplementedException();
    }

    public IEnumerable<VolunteerInList> ReadAll(bool? active = null, CallInListFilter? sort = null)
    {
        throw new NotImplementedException();
    }

    public string SystemEnter(string username, string password)
    {
        throw new NotImplementedException();
    }

    public void Update(int volunteerId, Volunteer change)
    {
        throw new NotImplementedException();
    }
}
