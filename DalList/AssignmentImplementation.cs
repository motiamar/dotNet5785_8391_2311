namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

internal class AssignmentImplementation : IAssignment

{

    // the func crate a new spot in the list and Add the new entity to the spot with a new Id and return the new Id.
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(Assignment item)  
    {
        int NewId = Config.NextAssignmentId;
        var copy = item with { Id = NewId };
        DataSource.Assignments.Add(copy);
        return NewId;
    }

    // the func search for the entity in the list by the id and remove it
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)   
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

    // the func remove all the entities in the list
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteAll()   
    {
        DataSource.Assignments.Clear();
    }

    // return if the item with the corrent id exist
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Assignment? Read(int id)  
    {
        return DataSource.Assignments.FirstOrDefault(item => item.Id == id); 
    }


    // the func search a entity in the list end return a pointer, depend on the filter func, if it not exsist it return null
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Assignment? Read(Func<Assignment, bool> filter)  
    {
        return DataSource.Assignments.FirstOrDefault(filter);
    }


    // the func return the list with/without the filter func pointer
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Assignment> ReadAll(Func<Assignment, bool>? filter = null) 
         => filter == null
             ? DataSource.Assignments.Select(item => item)
            : DataSource.Assignments.Where(filter);



    // the func updatr a entity in the list by the new parameters that in the given entity
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Assignment item) 
    {
        Delete(item.Id);
        DataSource.Assignments.Add(item);
    }
}
