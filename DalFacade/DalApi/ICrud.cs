using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    /// <summary>
    /// a generic interface that testator to all the entitis
    /// </summary>
    public interface ICrud<T> where T : class
    {
        int Create(T item);    //Creates new entity object in DAL
        T? Read(int id);    //Reads entity object by its ID 
        T? Read(Func<T, bool> filter); //Reads entity object by its ID and fiter if exist
        IEnumerable <T> ReadAll(Func<T, bool>? filter = null); // return all the entities that stand the filter func if exist
        void Update(T item);   //Updates entity object
        void Delete(int id);    //Deletes an object by its Id
        void DeleteAll();    //Delete all entity objects
    }
}
