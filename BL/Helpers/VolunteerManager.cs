using BO;
using DalApi;
using DO;
using System;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using static BO.Exceptions;
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
    /// func to get all the BO.VolunteerInList
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
        int? idAssignmentOfCall = assignments.FirstOrDefault(v => v.FinishTime == null)?.Id;
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
            CorrentCall = idAssignmentOfCall == null ? null : Helpers.CallManager.GetCallInProgress(idAssignmentOfCall.Value)
        };
        return Bvolunteer;
    }


    /// <summary>
    /// func to chek if all the fileds in the entity are valid
    /// </summary>
    public static void VolunteerChek(BO.Volunteer volunteer)
    {
        if(!VaildId(volunteer.Id))
            throw new BlinCorrectException("the id is not valid");
        if(!VailPhone(volunteer.Phone))
            throw new BlinCorrectException("the Phone is not valid");
        if(!VaildEmail(volunteer.Email))
            throw new BlinCorrectException("the Email is not valid");
        if (!VaildPassword(volunteer.Password!))
            throw new BlinCorrectException("the Password is not strong enough");
        if (! Helpers.Tools.IsValidAddress(volunteer.Address!))
            throw new BlinCorrectException("the Address is not valid");
        if(volunteer.MaximumDistance is not null)
        {
            if (!VaildMaxDistance(volunteer.MaximumDistance))
                throw new BlinCorrectException("the Maximum Distance is not valid");
        }
    }

    /// <summary>
    /// func to check if the id is valid
    /// </summary>
    public static bool VaildId(int id)
    {
        if (id < 100000000 || id > 999999999)
            return false;
        int sum = 0;
        bool even = true;
        for (int i = 0; i < 9; i++)
        {
            int num = id % 10;
            id /= 10;
            if (even)
            {
                sum += num;
            }
            else
            {
                int doubled = num * 2;
                sum += (doubled > 9) ? doubled - 9 : doubled;
            }
            even = !even;
        }
        return sum % 10 == 0;
    }

    /// <summary>
    /// func to check if the phone is valid
    /// </summary>
    public static bool VailPhone(string phone)
    {
        phone = phone.Replace(" ", "");
        string pattern = @"^05[0-9]{8}$";
        return Regex.IsMatch(phone, pattern);
    }

    /// <summary>
    /// func to check if the email is valid
    /// </summary>
    public static bool VaildEmail(string email)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    /// <summary>
    /// func to check if the password is strong enoungh
    /// </summary>
    public static bool VaildPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;
        if (password.Length < 8)
            return false;
        if (!Regex.IsMatch(password, @"[A-Z]"))
            return false;
        if (!Regex.IsMatch(password, @"[a-z]"))
            return false;
        if (!Regex.IsMatch(password, @"[0-9]"))
            return false;
        if (!Regex.IsMatch(password, @"[!@#$%^&*(),?\""]"))
            return false;
        return true;
    }

    /// <summary>
    /// func to check if the maximum distance is valid
    /// </summary>
    public static bool VaildMaxDistance(double? MaxDistance)
    {
        if (MaxDistance.Value < 0)
            return false;
        return true;
    }
}