namespace DalTest;

using Dal;
using DalApi;
using DO;

public static class Initialization
{
    private static IAssignment? s_dalAssignment; 
    private static ICall? s_dalCall;
    private static IVolunteer? s_dalVolunteer;
    private static IConfig? s_dalConfig;

    private static readonly Random s_rand = new(); // createing random numbers for the entities
    private static void CreateVolunteers()
    {
        string[] volunteerName = { "moti amar", "elya motai", "iosi yoskovich", "david gindi", "yuval michaeli", "dan zilber", "meir nahum", "izchak grinvald", "shalom salam", "meir morgnshtain", "eden cohen", "shimon cohen", "david hadad", "elly hadar", "nuriel hadad", "israel gadisha" };
        foreach (var name in volunteerName)
        {
            int id;
            do
                id = s_rand.Next(400000000, 200000000);
            while (s_dalVolunteer!.Read(id) != null);



        }




    }







    private static void CreateAssignments()
    {

    }
    private static void CreateCalls()
    {
        
        for (int i = 0; i < 50; i++)
        {
            DateTime start = new DateTime(s_dalConfig.Clock.Year - 2, 1, 1); //stage 1
            int range = (s_dalConfig.Clock - start).Days; //stage 1
			start.AddDays(s_rand.Next(range));
            string? VerbalDecription;
        }


    }
}
