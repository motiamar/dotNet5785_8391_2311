namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

internal class VolunteerImplementation : IVolunteer
{
    
    // the func crate a new spot in the list and Add the new entity to the spot
    public int Create(Volunteer item)    
    {
        if (Read(item.Id) != null)
            throw new DalAlreadyExistException($"Volunteer with ID={item.Id} has already exists");
        else
            DataSource.Volunteers.Add(item);
        return item.Id;
    }
    

// the func search for the entity in the list by the id and remove it
    public void Delete(int id)    
    {
        if (Read(id) != null)
            DataSource.Volunteers.Remove(Read(id)!);
        else 
            throw new DalDoesNotExistException($"Volunteer with ID={id} doesn't exists");
    }


// the func remove all the entities in the list
    public void DeleteAll()  
    {
        DataSource.Volunteers.Clear();  
    }


// return if the item with the corrent id exist
    public Volunteer? Read(int id) 
    {
        return DataSource.Volunteers.FirstOrDefault(item => item.Id == id); 
    }


 // the func search a entity in the list end return a pointer, depend on the filter func, if it not exsist it return null
    public Volunteer? Read(Func <Volunteer, bool> filter) 
    {
        return DataSource.Volunteers.FirstOrDefault(filter);
    }


    // the func return the list with/without the filter func pointer
    public IEnumerable<Volunteer> ReadAll(Func<Volunteer, bool> ? filter = null) 
         => filter == null ? DataSource.Volunteers.Select(item => item) : DataSource.Volunteers.Where(filter);



// the func updatr a entity in the list by the new parameters that in the given entity
    public void Update(Volunteer item)   

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
