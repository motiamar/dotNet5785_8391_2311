namespace DalTest;

using Dal;
using DalApi;
using DO;
using System;
using System.Globalization;
using System.Net;
using System.Numerics;

public static class Initialization
{
    private static IAssignment? s_dalAssignment; 
    private static ICall? s_dalCall;
    private static IVolunteer? s_dalVolunteer;
    private static IConfig? s_dalConfig;
    private static readonly Random s_rand = new(); // createing random numbers for the entities


    private static void CreateVolunteers() // creating 16 volunteers
    {
        string[] volunteerName = { "moti amar", "elya motai", "iosi yoskovich", "david gindi", "yuval michaeli", "dan zilber", "meir nahum", "izchak grinvald", "shalom salam", "meir morgnshtain", "eden cohen", "shimon cohen", "david hadad", "elly hadar", "nuriel hadad", "israel gadisha" };
        string[] volunteerPhone = { "0524815812", "0501234567", "0527654321", "0549876543", "0584567890", "0533210987", "0555678901", "0501112222", "0523334444", "0545526673", "0587748876", "0529950030", "0558767765", "0528775432", "0528765432", "0542109876" };
        string[] volunteerEmail = { "moti.amar@gmail.com", "elya.motai@yahoo.com", "iosi.yoskovich@icloud.com", "david.gindi@hotmail.com", "yuval.michaeli@gmail.com", "dan.zilber@yahoo.com", "meir.nahum@hotmail.com", "izchak.grinvald@outlock.com", "shalom.salam@icloud.com", "meir.morgnshtain@gmail.com", "eden.cohen@hotmail.com", "shimon.cohen@yahoo.com", "david.hadad@gmail.com", "elly.hadar@icloud.com", "nuriel.hadad@yahoo.com", "israel.gadisha@gmail.com" };
        string[] volunteerAddress = { "Dizengoff Center, Tel Aviv", "Azrieli Towers, Tel Aviv", "Rabin Square, Tel Aviv", "Herzliya Marina, Herzliya", "Ramat Gan National Park", "Netanya City Hall", "Rishon Lezion Beach", "Bar Ilan University, Ramat Gan", "Shoham Industrial Park", "Kfar Saba Park", "Savyon Residential Area", "Rehovot Science Park", "Holon Children’s Museum", "Bat Yam Promenade", "Weizmann Institute of Science, Rehovot", "Petah Tikva Market" };
        double[] volunteerLatitude = { 32.075775, 32.074983, 32.080744, 32.161965, 32.046663, 32.330043, 31.966392, 32.068422, 31.996737, 32.173125, 32.046094, 31.897801, 32.011409, 32.018166, 31.907377, 32.088914};
        double[] volunteerLongitude = { 34.774815, 34.791462, 34.780527, 34.794919, 34.822562, 34.856891, 34.772859, 34.842592, 34.948718, 34.905683, 34.878171, 34.813523, 34.779743, 34.750858, 34.808884, 34.872197 };

        int i = 0;
        foreach (var name in volunteerName)
        {
            int tmpid;
            do
                tmpid = s_rand.Next(400000000, 200000000);
            while (s_dalVolunteer!.Read(tmpid) != null);
            // make sure that the id dosent exsist

            bool tmpaActive = (tmpid % 2) == 0 ? true : false;
            // active choise

            string? password = name + "pass" + (i*4);
            // random password

            Role tmpRole = default(Role);
            if((i) == volunteerName.Length)
            {
                Role tmpRoleManager = (Role)Enum.GetValues(typeof(Role)).Cast<Role>().ElementAt(1);
                tmpRole = tmpRoleManager;
            }
            // 15 volunteers and one manager

            double tmpMaximumDistance = s_rand.Next(50000, 100000); // random distance

            DistanceType tmpDistanceType = default(DistanceType);

            s_dalVolunteer.create(new Volunteer // create a new volunteer with all the tmp arguments
            {
                Id = tmpid,
                FullName = name,
                Phone = volunteerPhone[i],
                Email = volunteerEmail[i],
                Password = password,
                Address = volunteerAddress[i],
                Latitude = volunteerLatitude[i],
                Longitude = volunteerLongitude[i],
                Role = tmpRole,
                Active = tmpaActive,
                MaximumDistance = tmpMaximumDistance,
                DistanceType = tmpDistanceType,
            });
            i++;
        }
    }
    
    private static void CreateCalls()  // creating 50 calls
    {
        
    }





    private static void CreateAssignments()
    {

    }
}
