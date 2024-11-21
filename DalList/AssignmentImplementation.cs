namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class AssignmentImplementation : IAssignment
{
    public int Create(Assignment item)  // the func crate a new spot in the list and add the new entity to the spot with a new Id and return the new Id.
    {
        int NewId = Config.NextAssignmentId;
        var copy = item with { Id = NewId };
        DataSource.Assignments.Add(copy);
        return NewId;
    }
   

    public void Delete(int id)   // the func search for the entity in the list by the id and remove it
    {
        bool flag = false;
        foreach (var item in DataSource.Assignments)
        {
            if (item.Id == id)
            {
                flag = true;
                DataSource.Assignments.Remove(item);
                break;
            }
        }
        if (flag == false)
            throw new DalDoesNotExistException($"Assignment with ID={id} doesn't exists");
    }
   

    public void DeleteAll()   // the func remove all the entities in the list
    {
        DataSource.Assignments.Clear();
    }
   

    public Assignment? Read(int id)  // return if the item with the corrent id exist
    {
        return DataSource.Assignments.FirstOrDefault(item => item.Id == id); 
    }

    public Assignment? Read(Func<Assignment, bool> filter)  // the func search a entity in the list end return a pointer, depend on the filter func, if it not exsist it return null
    {
        return DataSource.Assignments.FirstOrDefault(filter);
    }


    // the func return the list with/without the filter func pointer
    public IEnumerable<Assignment> ReadAll(Func<Assignment, bool>? filter = null) 
         => filter == null
             ? DataSource.Assignments.Select(item => item)
            : DataSource.Assignments.Where(filter);


    public void Update(Assignment item) // the func updatr a entity in the list by the new parameters that in the given entity
    {
        Delete(item.Id);
        Create(item);
    }
    

}
