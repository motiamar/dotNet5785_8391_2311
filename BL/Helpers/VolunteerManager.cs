using BO;
using DalApi;
using DO;
using System;
using System.Net;
using System.Net.Mail;
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
        var volunteers = s_dal.Volunteer.ReadAll();
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
        var calls = s_dal.Call.ReadAll();
        var VolunteerInLists = from volunteer in s_dal.Volunteer.ReadAll()
                               let assignments = s_dal.Assignment.ReadAll(a => a.VolunteerId == volunteer.Id)
                               let correntCallId = assignments.FirstOrDefault(v => v.FinishTime == null)?.CallId
                               select new BO.VolunteerInList
                               {
                                   Id = volunteer.Id,
                                   FullName = volunteer.FullName,
                                   Active = volunteer.Active,
                                   TreatedCalls = assignments.Count(v => v.EndKind == DO.EndKinds.Treated),
                                   CanceledCalls = assignments.Count(v => v.EndKind == DO.EndKinds.Administrator_cancellation || v.EndKind == DO.EndKinds.Self_cancellation),
                                   ExpiredCalls = assignments.Count(v =>  v.EndKind == DO.EndKinds.Expired_cancellation),
                                   CorrentCallId = correntCallId,
                                   CorrentCallType = correntCallId == null ? BTypeCalls.None : (BO.BTypeCalls)calls.FirstOrDefault(v => v.Id == correntCallId)!.TypeCall
                               };
        return VolunteerInLists;
    }


    /// <summary>
    /// return BO.Volunteer by id
    /// </summary>
    public static BO.Volunteer GetBOVolunteer(int Id)
    {
        DO.Volunteer volunteer = s_dal.Volunteer.Read(Id)!;
        var assignments = s_dal.Assignment.ReadAll(a => a.VolunteerId == Id);
        int? correntCall = assignments.FirstOrDefault(v => v.FinishTime == null)?.Id;
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
            TreatedCalls = assignments.Count(v => v.EndKind == DO.EndKinds.Treated),
            CanceledCalls = assignments.Count(v => v.EndKind == DO.EndKinds.Administrator_cancellation || v.EndKind == DO.EndKinds.Self_cancellation),
            ExpiredCalls = assignments.Count(v => v.EndKind == DO.EndKinds.Expired_cancellation),
            CorrentCall = correntCall == null ? null : GetCallInProgress(correntCall.Value)
        };


        return Bvolunteer;
    }


    /// <summary>
    /// return the call in progress by id
    /// </summary>
    public static BO.CallInProgress? GetCallInProgress(int id)
    {
        DO.Assignment assignment = s_dal.Assignment.Read(id)!;
        DO.Volunteer volunteer = s_dal.Volunteer.Read(assignment.VolunteerId)!;
        DO.Call call = s_dal.Call.Read(assignment.CallId)!;
        var callInProgress = new BO.CallInProgress
        {
            Id = id,
            CallId = assignment.CallId,
            Type = (BTypeCalls)call.TypeCall,
            Description = call.VerbalDecription,
            CallAddress = call.FullAddressOfTheCall,
            CallOpenTime = call.OpeningCallTime,
            CallMaxCloseTime = call.MaxEndingCallTime,
            CallEnterTime = assignment.StartTime,
            CallDistance = Helpers.Tools.Distance(volunteer.DistanceType, volunteer.Latitude!.Value, volunteer.Longitude!.Value, call.Latitude, call.Longitude),
            CallStatus = Helpers.CallManager.GetStatus(call)
        };
        return callInProgress;
    }
}   