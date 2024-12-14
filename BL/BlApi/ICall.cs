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
    /// <param name="filter"> if it exist, filter the list by the BTypeCalls prameter </param>
    /// <param name="value"> if it exist, filter the list by the value object</param>
    /// <param name="sort"> sorted the list by the BCallStatus prameter if it exist </param>
    IEnumerable<BO.CallInList> ReadAll(BO.CallInListFilter? sort = null, object? value = null, BO.CallInListFilter? filter = null);

    /// <summary>
    /// create a BO.Call entity if the id exist in the data base 
    /// </summary>
    /// <param name="Id"> get the id of the call</param>
    /// <returns> return the copy of the entity by BO.call entity if it exist</returns>
    BO.Call? Read(int id);

    /// <summary>
    /// func to Update an exist call by the Manager or the self call
    /// </summary>
    /// <param name="change">the new entity we want to switch to</param>
    void Update(BO.Call change);

    /// <summary>
    /// Delete the call if it exist and Open or never assignt 
    /// </summary>
    void Delete(int callId);

    /// <summary>
    /// Add a new call to the data leir if the details are correct
    /// </summary>
    void Create(BO.Call call);

    /// <summary>
    /// func to create a new list by the recived parameters
    /// </summary>
    /// <param name="volunteerId"> the id of the Volunteer</param>
    /// <param name="filter"> the kind of the call that the list filter by</param>
    /// <param name="sort">the kind of the call filed that the list sorted by </param>
    /// <returns>return the new list</returns>
    IEnumerable<BO.ClosedCallInList> GetCloseCallList(int volunteerId, BO.BTypeCalls? filter = null, BO.CloseCallInListFilter? sort = null);

    /// <summary>
    /// func that return a sorted list of the calls in list, sorted by filter Booleany and a enum fild 
    /// </summary>
    /// <param name="volunteerId"> the id of the Volunteer</param>
    /// <param name="filter"> filterd the list by the given parameter</param>
    /// <param name="sort"> soreted the list by the given parameter</param>
    /// <returns>return the new list</returns>
    IEnumerable<BO.OpenCallInList> GetOpenCallList(int volunteerId, BO.BTypeCalls? filter = null, BO.OpenCallInListFilter? sort = null); 

    /// <summary>
    /// chek if exist and end the assignment of the Volunteer
    /// </summary>
    /// <param name="volunteerId"> the Volunteer who want to end the assignemnt</param>
    /// <param name="assignmentId"> the assignment that need to close</param>
    void EndAssignment (int volunteerId, int assignmentId);

    /// <summary>
    /// func to cancale an assignment by the Volunteer who handle it or the managar if the conditions are good
    /// </summary>
    /// <param name="volunteerId">the Volunteer who want to cencele the assignemnt</param>
    /// <param name="assignmentId">the assignment that need to cancaled</param>
    void CanceleAssignment(int volunteerId, int assignmentId);

    /// <summary>
    /// func that assignet Volunteer to a call if it available and not taken 
    /// </summary>
    /// <param name="volunteerId">the volunteer who want to handle the call</param>
    /// <param name="CallId">the call he want to choose</param>
    void ChooseCall(int  volunteerId, int CallId);
}

