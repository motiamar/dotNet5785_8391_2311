using BO;
using DalApi;
using DO;
using System;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
namespace Helpers;

internal static class VolunteerManager
{
    private static IDal s_dal = Factory.Get;

    /// <summary>
    /// return the role of the volunteer
    /// </summary>
    public static string? GetVolunteerRole(string username, string password)
    {
        IEnumerable<DO.Volunteer> volunteers = s_dal.Volunteer.ReadAll();
        var volunteer = volunteers.FirstOrDefault(v => v.FullName == username && v.Password == password);
        if (volunteer != null)
        {
            return volunteer.Role.ToString();
        }
        return null;
    }

    /// <summary>
    /// func to get all the volunteer in list
    /// </summary>
    public static IEnumerable<VolunteerInList> GetAllVolunteerInList()
    {
        IEnumerable<DO.Call> calls = s_dal.Call.ReadAll();
        var VolunteerInLists = from volunteer in s_dal.Volunteer.ReadAll()
                               let assignments = s_dal.Assignment.ReadAll(a => a.VolunteerId == volunteer.Id)
                               let correntCallId = assignments.FirstOrDefault(v => v.FinishTime == null)?.CallId
                               select new BO.VolunteerInList
                               {
                                   Id = volunteer.Id,
                                   FullName = volunteer.FullName,
                                   Active = volunteer.Active,
                                   TreatedCalls = assignments.Count(v => v.EndKind == DO.EndKinds.Treated),
                                   CanceledCalls = assignments.Count(v => v.EndKind == DO.EndKinds.administrator_cancellation || v.EndKind == DO.EndKinds.self_cancellation),
                                   ExpiredCalls = assignments.Count(v =>  v.EndKind == DO.EndKinds.expired_cancellation),
                                   CorrentCallId = correntCallId,
                                   CorrentCallType = correntCallId == null ? BTypeCalls.None : (BO.BTypeCalls)calls.FirstOrDefault(v => v.Id == correntCallId)!.TypeCall
                               };
        return VolunteerInLists;
    }


    /// <summary>
    /// 
    /// </summary>
    public static BO.Volunteer GetBOVolunteer(int Id)
    {
        DO.Volunteer volunteer = s_dal.Volunteer.Read(Id)!;
        //s_dal.Assignment.ReadAll(a => a.VolunteerId == Id).Where();
        //BO.CallInProgress? callInProgress = 
        BO.Volunteer Bvolunteer = new BO.Volunteer
        {
            Id = Id,
            FullName = volunteer.FullName,
            Phone = volunteer.Phone,
            Email = volunteer.Email,
            Password = volunteer.Password,
            Address = volunteer.Address,
            Latitude = volunteer.Latitude,
            Longitude = volunteer.Longitude,
            role = (BO.BRoles)volunteer.Role,
            Active = volunteer.Active,
            MaximumDistance = volunteer.MaximumDistance,
            DistanceType = (BO.BDistanceTypes)volunteer.DistanceType,
            TreatedCalls = s_dal.Assignment.ReadAll(a => a.VolunteerId == Id).Count(v => v.EndKind == DO.EndKinds.Treated),
            CanceledCalls = s_dal.Assignment.ReadAll(v => v.VolunteerId == Id).Count(v => v.EndKind == DO.EndKinds.administrator_cancellation || v.EndKind == DO.EndKinds.self_cancellation),
            ExpiredCalls = s_dal.Assignment.ReadAll(v => v.VolunteerId == Id).Count(v => v.EndKind == DO.EndKinds.expired_cancellation),
            CorrentCall = 
        };


        return null;
    }
}   
