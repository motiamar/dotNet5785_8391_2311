namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

internal class VolunteerImplementation : IVolunteer
{
    public int Create(Volunteer item)    // the func crate a new spot in the list and add the new entity to the spot
    {
        if (Read(item.Id) != null)
            throw new DalAlreadyExistException($"Volunteer with ID={item.Id} has already exists");
        else
            DataSource.Volunteers.Add(item);
        return item.Id;
    }
    

    public void Delete(int id)    // the func search for the entity in the list by the id and remove it
    {
        if (Read(id) != null)
            DataSource.Volunteers.Remove(Read(id)!);
        else 
            throw new DalDoesNotExistException($"Volunteer with ID={id} doesn't exists");
    }


    public void DeleteAll()  // the func remove all the entities in the list
    {
        DataSource.Volunteers.Clear();  
    }


    public Volunteer? Read(int id) // return if the item with the corrent id exist
    {
        return DataSource.Volunteers.FirstOrDefault(item => item.Id == id); 
    }

    public Volunteer? Read(Func <Volunteer, bool> filter)  // the func search a entity in the list end return a pointer, depend on the filter func, if it not exsist it return null
    {
        return DataSource.Volunteers.FirstOrDefault(filter);
    }


    // the func return the list with/without the filter func pointer
    public IEnumerable<Volunteer> ReadAll(Func<Volunteer, bool> ? filter = null) 
         => filter == null ? DataSource.Volunteers.Select(item => item) : DataSource.Volunteers.Where(filter);

    public void Update(Volunteer item)   // the func updatr a entity in the list by the new parameters that in the given entity

    {
        if (Read(item.Id) != null)
        {
            Delete(item.Id);
            Create(item);
        }
        else
            throw new DalDoesNotExistException($"Volunteer with ID={item.Id} doesn't exists");
    }
}
