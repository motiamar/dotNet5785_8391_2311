namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

public class VolunteerImplementation : IVolunteer
{
    public void create(Volunteer item)
    {
        if(Read(item.Id) != null)
            throw new NotImplementedException("אובייקט מסוג מתנדב עם תז כזה כבר קיים");
        else
            DataSource.Volunteers.Add(item);
    }
    // the func crate a new spot in the list and add the new entity to the spot
    
    public void Delete(int id)
    {
        if (Read(id) != null)
            DataSource.Volunteers.Remove(Read(id)!);
        else 
            throw new NotImplementedException("אובייקט מסוג מתנדב עם תז כזה לא קיים");
    }
    // the func search for the entity in the list by the id and remove it

    public void DeleteAll()
    {
        DataSource.Volunteers.Clear();  
    }
    // the func remove all the entities in the list


    public Volunteer? Read(int id)
    {
        foreach (Volunteer item in DataSource.Volunteers)
        {
            if (item.Id == id)
                return item;
        }
        return null;
    }
    // the func search a entity in the list end return a pointer, if it not exsist it return null

    public List<Volunteer> ReadAll()
    {
        List<Volunteer> volunteersCopy = new List<Volunteer>();
        volunteersCopy.AddRange(DataSource.Volunteers);  
        return volunteersCopy;
    }
    // the func create a new list with the same entities as the list that send to it

    public void Update(Volunteer item)
    {
        if (Read(item.Id) != null)
        {
            Delete(item.Id);
            create(item);
        }
        else
            throw new NotImplementedException("אובייקט מסוג מתנדב עם תז כזה לא קיים");
    }
    // the func updatr a entity in the list by the new parameters that in the given entity
}
