using BlImplementation;
using BO;
using DalApi;
using DO;
using System;
using System.Data;
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

    internal static ObserverManager Observers = new(); //stage 5 
    /// <summary>
    /// return the role of the volunteer
    /// </summary>
    internal static string? GetVolunteerRole(string username, string password)
    {
        IEnumerable<DO.Volunteer>? volunteers;
        lock (AdminManager.BlMutex)
            volunteers = s_dal.Volunteer.ReadAll();

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
    internal static IEnumerable<VolunteerInList> GetAllVolunteerInList()
    {
        lock (AdminManager.BlMutex)
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
                                       ExpiredCalls = assignments.Count(v => v.EndKind == DO.EndKinds.Expired_cancellation),
                                       CorrentCallId = correntCallId,
                                       CorrentCallType = correntCallId == null ? BTypeCalls.None : (BO.BTypeCalls)calls.FirstOrDefault(v => v.Id == correntCallId)!.TypeCall
                                   };
            return VolunteerInLists;
        }
    }


    /// <summary>
    /// return BO.Volunteer by id
    /// </summary>
    internal static BO.Volunteer GetBOVolunteer(int Id)
    {
        DO.Volunteer volunteer;
        IEnumerable<Assignment>? assignments;
        lock (AdminManager.BlMutex)
        {
            volunteer = s_dal.Volunteer.Read(Id)!;
            assignments = s_dal.Assignment.ReadAll(a => a.VolunteerId == Id);
        }

        var CurrentAssignmentOfCall = assignments.FirstOrDefault(v => v.FinishTime == null);
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
            CorrentCall = CurrentAssignmentOfCall == null ? null : Helpers.CallManager.GetCallInProgress(CurrentAssignmentOfCall, volunteer)
        };
        return Bvolunteer;
    }


    /// <summary>
    /// func to chek if all the fileds in the entity are valid
    /// </summary>
    internal static void VolunteerChek(BO.Volunteer volunteer)
    {
        if (!VaildId(volunteer.Id))
            throw new BlinCorrectException("the ID is not valid");
        if (string.IsNullOrEmpty(volunteer.FullName))
            throw new BlinCorrectException("the Name is empty");
        if (!VailPhone(volunteer.Phone))
            throw new BlinCorrectException("the Phone is not valid");
        if (!VaildEmail(volunteer.Email))
            throw new BlinCorrectException("the Email is not valid");
        if (!VaildPassword(volunteer.Password!))
            throw new BlinCorrectException("the Password is not strong enough");
        if (!Helpers.Tools.IsValidAddress(volunteer.Address!))
            throw new BlinCorrectException("the Address is not valid");
        if (volunteer.MaximumDistance is not null)
        {
            if (!VaildMaxDistance(volunteer.MaximumDistance))
                throw new BlinCorrectException("the Maximum Distance is not valid");
        }
    }

    /// <summary>
    /// func to check if the id is valid
    /// </summary>
    internal static bool VaildId(int id)
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
    internal static bool VailPhone(string phone)
    {
        if (string.IsNullOrEmpty(phone))
            return false;
        phone = phone.Replace(" ", "");
        string pattern = @"^05[0-9]{8}$";
        return Regex.IsMatch(phone, pattern);
    }

    /// <summary>
    /// func to check if the email is valid
    /// </summary>
    internal static bool VaildEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    /// <summary>
    /// func to check if the password is strong enoungh
    /// </summary>
    internal static bool VaildPassword(string password)
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
    internal static bool VaildMaxDistance(double? MaxDistance)
    {
        if (MaxDistance!.Value < 0)
            return false;
        return true;
    }


    /// <summary>
    /// the simulation of the volunteers activity
    /// </summary>
    private static readonly Random s_rand = new();
    private static int s_simulatorCounter = 0;
    internal static void SimulateVolunteerActivity() //stage 7
    {
        try
        {
            Thread.CurrentThread.Name = $"Simulator{++s_simulatorCounter}";
            List<DO.Volunteer> doVolList;

            lock (AdminManager.BlMutex)
                doVolList = s_dal.Volunteer.ReadAll(v => v.Active == true).ToList();

            foreach (var volunteer in doVolList)
            {
                List<DO.Assignment> assignments;
                lock (AdminManager.BlMutex)
                    assignments = s_dal.Assignment.ReadAll(a => a.VolunteerId == volunteer.Id).Where(v => v.FinishTime == null).ToList();


                // there are no open call in progress
                if (assignments == null || assignments.Count == 0)
                {
                    if (s_rand.Next(0, 100) < 20) // 20% chances to choose a call
                    {
                        var openCallInList = Helpers.CallManager.GetOpenCallInLists(volunteer);
                        if (openCallInList != null && openCallInList.Any())
                        {
                            var openCall = openCallInList.ElementAt(s_rand.Next(0, openCallInList.Count()));

                            DO.Assignment assignment = new DO.Assignment
                            {
                                CallId = openCall.Id,
                                VolunteerId = volunteer.Id,
                                StartTime = Helpers.AdminManager.Now,
                                FinishTime = null,
                                EndKind = EndKinds.Treated
                            };

                            lock (AdminManager.BlMutex)
                                s_dal.Assignment.Create(assignment);

                            CallManager.Observers.NotifyItemUpdated(openCall.Id);
                            CallManager.Observers.NotifyListUpdated();
                            VolunteerManager.Observers.NotifyItemUpdated(volunteer.Id);
                            VolunteerManager.Observers.NotifyListUpdated();
                        }
                    }

                }


                // there is an open call in progress
                else
                {
                    DO.Assignment assignment = assignments.First();
                    int randomMinutes = s_rand.Next(30, 61);
                    DateTime adjustedStartTime = assignment.StartTime.AddMinutes(randomMinutes);

                    if (Helpers.AdminManager.Now > adjustedStartTime) // past a random time to close the call
                    {
                        DO.Assignment updateAssignment = new DO.Assignment
                        {
                            Id = assignment.Id,
                            CallId = assignment.CallId,
                            VolunteerId = assignment.VolunteerId,
                            StartTime = assignment.StartTime,
                            FinishTime = Helpers.AdminManager.Now,
                            EndKind = DO.EndKinds.Treated
                        };

                        lock (AdminManager.BlMutex)
                            s_dal.Assignment.Update(updateAssignment);

                        CallManager.Observers.NotifyItemUpdated(assignment.CallId);
                        CallManager.Observers.NotifyListUpdated();
                        VolunteerManager.Observers.NotifyItemUpdated(volunteer.Id);
                        VolunteerManager.Observers.NotifyListUpdated();
                    }
                    else // the time has not yet the past
                    {
                        if (s_rand.Next(0, 100) < 10) // 10% chances to cancle a call
                        {
                            string? role = Helpers.VolunteerManager.GetVolunteerRole(volunteer.FullName, volunteer.Password!)!;
                            var update = new DO.Assignment
                            {
                                Id = assignment.Id,
                                CallId = assignment.CallId,
                                VolunteerId = assignment.VolunteerId,
                                StartTime = assignment.StartTime,
                                FinishTime = Helpers.AdminManager.Now,
                                EndKind = role == "Manager" ? EndKinds.Administrator_cancellation : EndKinds.Self_cancellation,
                            };

                            lock (AdminManager.BlMutex)
                                s_dal.Assignment.Update(update);

                            CallManager.Observers.NotifyItemUpdated(assignment.CallId);
                            CallManager.Observers.NotifyListUpdated();
                            VolunteerManager.Observers.NotifyItemUpdated(volunteer.Id);
                            VolunteerManager.Observers.NotifyListUpdated();
                        }
                    }

                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}