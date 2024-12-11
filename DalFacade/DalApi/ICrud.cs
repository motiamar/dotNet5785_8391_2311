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
        /// <summary>
        /// Creates new entity object in DAL
        /// </summary>
        int Create(T item);

        /// <summary>
        /// Reads entity object by its ID 
        /// </summary>
        T? Read(int id);

        /// <summary>
        /// Reads entity object by its ID and fiter if exist
        /// </summary>
        T? Read(Func<T, bool> filter);

        /// <summary>
        /// return all the entities that stand the filter func if exist
        /// </summary>
        /// <param name="filter"> return by the filter if exist</param>
        IEnumerable<T> ReadAll(Func<T, bool>? filter = null);

        /// <summary>
        /// Updates entity object
        /// </summary>
        void Update(T item);

        /// <summary>
        /// Deletes an object by its Id
        /// </summary>
        void Delete(int id);

        /// <summary>
        /// Delete all entity objects
        /// </summary>
        void DeleteAll();    
    }
}
