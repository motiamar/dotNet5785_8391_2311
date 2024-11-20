namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class AssignmentImplementation : IAssignment
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
        foreach (var item in DataSource.Assignments)
        {
            if (item.Id == id)
                return item;
        }
        return null;
    }
    // the func search a entity in the list end return a pointer, if it not exsist it return null

    public List<Assignment> ReadAll()
    {
        List<Assignment> newList = new List<Assignment>();
        newList.AddRange(DataSource.Assignments);
        return newList;
    }
    // the func create a new list with the same entities as the list that send to it

    public void Update(Assignment item)
    {
        Delete(item.Id);
        Create(item);
    }
    // the func updatr a entity in the list by the new parameters that in the given entity

}
