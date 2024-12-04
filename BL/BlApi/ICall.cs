namespace BlApi;

public interface ICall
{
    /// <summary>
    /// func that return array filed with the amount of calls exist, orederd by compering the index to the enum size
    /// </summary>
    int[] ArrayStatus();

    /// <summary>
    /// a func to create a filter and sorted list and return the list 
    /// </summary>
    /// <param name="filert"> if it exist, filter the list by the BTypeCalls prameter </param>
    /// <param name="value"> if it exist, filter the list by the value object</param>
    /// <param name="sort"> sorted the list by the BCallStatus prameter if it exist </param>
    IEnumerable<BO.CallInList> ReadAll(BO.BTypeCalls? filert = null, object? value = null, BO.BCallStatus? sort = null);

    /// <summary>
    /// create a BO.Call entity if the id exist in the data base 
    /// </summary>
    /// <param name="Id"> get the id of the call</param>
    /// <returns> return the copy of the entity by BO.call entity if it exist</returns>
    BO.Call? Read(int Id);

}

