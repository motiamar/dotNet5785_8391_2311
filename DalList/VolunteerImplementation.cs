namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

internal class VolunteerImplementation : IVolunteer
{
    public int Create(Volunteer item)
    {
        if(Read(item.Id) != null)
            throw new NotImplementedException($"Volunteer with ID={item.Id} has already exists");
        else
            DataSource.Volunteers.Add(item);
        return item.Id;
    }
    // the func crate a new spot in the list and add the new entity to the spot
    
    public void Delete(int id)
    {
        if (Read(id) != null)
            DataSource.Volunteers.Remove(Read(id)!);
        else 
            throw new NotImplementedException($"Volunteer with ID={id} doesn't exists");
    }
    // the func search for the entity in the list by the id and remove it

    public void DeleteAll()
    {
        DataSource.Volunteers.Clear();  
    }
    // the func remove all the entities in the list


    public Volunteer? Read(int id)
    {
        return DataSource.Volunteers.FirstOrDefault(item => item.Id == id); //stage 2
    }

    public Volunteer? Read(Func<Volunteer, bool> filter)
    {
        return DataSource.Volunteers.FirstOrDefault(filter);
    }

    // the func search a entity in the list end return a pointer, if it not exsist it return null

    public IEnumerable<Volunteer> ReadAll(Func<Volunteer, bool>? filter = null) //stage 2
         => filter == null
             ? DataSource.Volunteers.Select(item => item)
            : DataSource.Volunteers.Where(filter);
    // the func create a new list with the same entities as the list that send to it

    public void Update(Volunteer item)
    {
        if (Read(item.Id) != null)
        {
            Delete(item.Id);
            Create(item);
        }
        else
            throw new NotImplementedException($"Volunteer with ID={item.Id} doesn't exists");
    }
    // the func updatr a entity in the list by the new parameters that in the given entity
}
