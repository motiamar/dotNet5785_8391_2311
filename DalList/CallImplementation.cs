namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public class CallImplementation : ICall
{
    public int Create(Call item)
    {
        int NewId = Config.NextCallId;
        var copy = item with { Id = NewId };
        DataSource.Calls.Add(copy);
        return NewId;
    }
    // the func crate a new spot in the list and add the new entity to the spot with a new Id and return the new Id.


    public void Delete(int id)
    {
        bool flag = false;
        foreach (var item in DataSource.Calls)
        {
            if (item.Id == id)
            {
                flag = true;
                DataSource.Calls.Remove(item);
                break;
            }
        }
        if (flag == false)
            throw new NotImplementedException($"Call with ID={id} doesn't exists");
    }
    // the func search for the entity in the list by the id and remove it

    public void DeleteAll()
    {
        DataSource.Calls.Clear();
    }
    // the func remove all the entities in the list

    public Call? Read(int id)
    {
        foreach (var item in DataSource.Calls)
        {
            if (item.Id == id)
                return item;
        }
        return null;
    }
    // the func search a entity in the list end return a pointer, if it not exsist it return null

    public List<Call> ReadAll()
    {
        List<Call> newList = new List<Call>();
        newList.AddRange(DataSource.Calls);
        return newList;
    }
    // the func create a new list with the same entities as the list that send to it

    public void Update(Call item)
    {
        Delete(item.Id);
        Create(item);
    }
    // the func updatr a entity in the list by the new parameters that in the given entity

}
