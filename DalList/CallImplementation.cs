namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

internal class CallImplementation : ICall
{
    
    public int Create(Call item)   // the func crate a new spot in the list and add the new entity to the spot with a new Id and return the new Id.
    {
        int NewId = Config.NextCallId;
        var copy = item with { Id = NewId };
        DataSource.Calls.Add(copy);
        return NewId;
    }


   
    public void Delete(int id)  // the func search for the entity in the list by the id and remove it
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

   

    public void DeleteAll()   // the func remove all the entities in the list
    {
        DataSource.Calls.Clear();
    }

    public Call? Read(int id)   // return if the item with the corrent id exist
    {
        return DataSource.Calls.FirstOrDefault(item => item.Id == id);
    }


    public Call? Read(Func<Call, bool> filter) // the func search a entity in the list end return a pointer, depend on the filter func, if it not exsist it return null
    {
        
        return DataSource.Calls.FirstOrDefault(filter);
    }

    public IEnumerable<Call> ReadAll(Func<Call, bool>? filter = null)  // the func return the list with/without the filter func pointer
         => filter == null
             ? DataSource.Calls.Select(item => item)
            : DataSource.Calls.Where(filter);

   
    public void Update(Call item)  // the func updatr a entity in the list by the new parameters that in the given entity
    {
        Delete(item.Id);
        Create(item);
    }

}
