namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class AssignmentImplementation : IAssignment
{
    public int Create(Assignment item)
    {
        int NewId = Config.NextAssignmentId;
        var copy = item with { Id = NewId };
        DataSource.Assignments.Add(copy);
        return NewId;
    }
    // the func crate a new spot in the list and add the new entity to the spot with a new Id and return the new Id.

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
            throw new NotImplementedException($"Assignment with ID={id} doesn't exists");
    }
    // the func search for the entity in the list by the id and remove it

    public void DeleteAll()
    {
        DataSource.Assignments.Clear();
    }
    // the func remove all the entities in the list

    public Assignment? Read(int id)
    {
        return DataSource.Assignments.FirstOrDefault(item => item.Id == id); //stage 2
    }

    public Assignment? Read(Func<Assignment, bool> filter)
    {
        return DataSource.Assignments.FirstOrDefault(filter);
    }

    // the func search a entity in the list end return a pointer, if it not exsist it return null

    public IEnumerable<Assignment> ReadAll(Func<Assignment, bool>? filter = null) //stage 2
         => filter == null
             ? DataSource.Assignments.Select(item => item)
            : DataSource.Assignments.Where(filter);

    // the func create a new list with the same entities as the list that send to it

    public void Update(Assignment item)
    {
        Delete(item.Id);
        Create(item);
    }
    // the func updatr a entity in the list by the new parameters that in the given entity

}
