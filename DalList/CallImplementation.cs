namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

internal class CallImplementation : ICall
{

    // the func crate a new spot in the list and Add the new entity to the spot with a new Id and return the new Id.
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(Call item)  
    {
        int NewId = Config.NextCallId;
        var copy = item with { Id = NewId };
        DataSource.Calls.Add(copy);
        return NewId;
    }


    // the func search for the entity in the list by the id and remove it
    [MethodImpl(MethodImplOptions.Synchronized)]
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
            throw new DalDoesNotExistException($"Call with ID={id} doesn't exists");
    }


    // the func remove all the entities in the list
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteAll()  
    {
        DataSource.Calls.Clear();
    }


    // return if the item with the corrent id exist
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Call? Read(int id)   
    {
        return DataSource.Calls.FirstOrDefault(item => item.Id == id);
    }


    // the func search a entity in the list end return a pointer, depend on the filter func, if it not exsist it return null
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Call? Read(Func<Call, bool> filter) 
    {
        
        return DataSource.Calls.FirstOrDefault(filter);
    }

    // the func return the list with/without the filter func pointer
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Call> ReadAll(Func<Call, bool>? filter = null)  
         => filter == null
             ? DataSource.Calls.Select(item => item)
            : DataSource.Calls.Where(filter);


    // the func updatr a entity in the list by the new parameters that in the given entity
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Call item)  
    {
        Delete(item.Id);
        DataSource.Calls.Add(item);
    }
}
