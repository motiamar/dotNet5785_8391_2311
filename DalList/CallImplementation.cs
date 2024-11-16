namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class CallImplementation : ICall
{
    public void create(Call item)
    { 
        int 
        throw new NotImplementedException();
    }
            
    

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
            throw new NotImplementedException(" אובייקט מסוג שיחה אינו קיים עם תז כזה");
    }

    public void DeleteAll()
    {
        DataSource.Calls.Clear();
    }

    public Call? Read(int id)
    {
        foreach (var item in DataSource.Calls)
        {
            if (item.Id == id)
                return item;
        }
        return null;
    }

    public List<Call> ReadAll()
    {
        List<Call> newList = new List<Call>();
        newList.AddRange(DataSource.Calls);
        return newList;
    }

    public void Update(Call item)
    {
        Delete(item.Id);
        create(item);
    }
}
