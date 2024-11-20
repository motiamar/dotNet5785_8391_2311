
using System.Windows.Markup;
namespace DalTest;
using DalApi;
using DO;
using Dal;
public static class Initialization
{
    //private static IAssignment? s_dalAssignment; 
    //private static ICall? s_dalCall;
    //private static IVolunteer? s_dalVolunteer;
    //private static IConfig? s_dalConfig;
    private static IDal? s_dal;
    private static readonly Random s_rand = new(); // createing random numbers for the entities


    private static void CreateVolunteers() // creating 16 volunteers
    {
        string[] volunteerName = { "moti amar", "elya motai", "iosi yoskovich", "david gindi", "yuval michaeli", "dan zilber", "meir nahum", "izchak grinvald", "shalom salam", "meir morgnshtain", "eden cohen", "shimon cohen", "david hadad", "elly hadar", "nuriel hadad", "israel gadisha" };
        string[] volunteerPhone = { "0524815812", "0501234567", "0527654321", "0549876543", "0584567890", "0533210987", "0555678901", "0501112222", "0523334444", "0545526673", "0587748876", "0529950030", "0558767765", "0528775432", "0528765432", "0542109876" };
        string[] volunteerEmail = { "moti.amar@gmail.com", "elya.motai@yahoo.com", "iosi.yoskovich@icloud.com", "david.gindi@hotmail.com", "yuval.michaeli@gmail.com", "dan.zilber@yahoo.com", "meir.nahum@hotmail.com", "izchak.grinvald@outlock.com", "shalom.salam@icloud.com", "meir.morgnshtain@gmail.com", "eden.cohen@hotmail.com", "shimon.cohen@yahoo.com", "david.hadad@gmail.com", "elly.hadar@icloud.com", "nuriel.hadad@yahoo.com", "israel.gadisha@gmail.com" };
        string[] volunteerAddress = {"Bustanei 15, jerusalem", "yehoshua ben gamla 13, jerusalem", "hala 13, jerusalem", "beit haarava 28, jerusalem", "harav herzog, jerusalem", "bosem 12,jerusalem", "hanamer 17, jerusalem", "hahalutz 17, jerusalem", "harav uziel 80,jerusalem ", "harav beztalel zulti 19, jerusalem", "shderot moshe daiian 109, jerusalem", "harav maymon 1, jerusalem", "a.m brachiu 7, jerusalem", "yzthar 25,jerusalem", "menachem mashkaloc 21, jerusalem", "aharon brend 12,jerusalem" };
        double[] volunteerLatitude = { 35.2126784, 35.2124199, 35.2087076, 35.2203343, 35.1994883, 35.1889476, 35.1805822, 35.1927184, 35.187882091894366,  35.21696116035834 , 35.24036075232489,  35.199124630657806 , 35.18891765398783,  35.18926428816602,  35.17470578874885, 35.17222213737034 };
        double[] volunteerLongitude = { 31.7648204, 31.7611362, 31.7638657, 31.7483032, 31.7589875, 31.7342954, 31.7518568, 31.7810136, 31.76552192492391, 31.810386702304726, 31.828473377413367, 31.788846938792492, 31.77681135390754, 31.738258839334225, 31.786734640142832, 31.784020945981794};

        int i = 0;
        foreach (var name in volunteerName)
        {
            int tmpid;
            do
                tmpid = s_rand.Next(200000000, 400000000);
            while (s_dal.volunteer.Read(tmpid) != null);
            // make sure that the id dosent exsist

            bool tmpaActive = (tmpid % 2) == 0 ? true : false;
            // active choise

            string? password = name + "pass" + (i*4);
            // random password

            Roles tmpRole = default(Roles);
            if(i == volunteerName.Length-1)
            {
                Roles tmpRoleManager = (Roles)Enum.GetValues(typeof(Roles)).Cast<Roles>().ElementAt(1);
                tmpRole = tmpRoleManager;
            }
            // 15 volunteers and one manager

            double tmpMaximumDistance = s_rand.Next(50000, 100000); // random distance

            DistanceTypes tmpDistanceType = default(DistanceTypes);

            s_dal.volunteer.Create(new Volunteer // create a new volunteer with all the tmp arguments
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
        string[] medical_Descriptions = { "Man aged 60 unconscious in critical condition","Young woman complaining of severe pain",  "Infant with high fever and instability", "Elderly person reporting severe shortness of breath", "Teenager unconscious after sudden fainting episode" };
        string[] accident_Descriptions = {"Car overturned with injured passengers inside", "Motorcyclist unconscious after severe accident", "Head-on collision between two vehicles", "Pedestrian hit at a crosswalk", "Woman injured by a car, moderate condition" };
        string[] fall_Descriptions = {"Worker fell from five-story height", "Girl fell from third-floor window", "Elderly person slipped and fell down stairs", "Person lost balance and fell off roof", "Child fell from a tree, lightly injured" };
        string[] violence_Descriptions = {"Man stabbed in the upper body", "Mass brawl with injured individuals in city center", "Young man attacked by several suspects on the street",  "Man injured following an attack with a sharp weapon","Assault victim reporting head and facial injuries"};
        string[] domestic_Descriptions = {"Woman reports assault inside her home", "Child with visible bruises on their face","Man reports violence by a family member", "Elderly person injured following family member's assault", "Teenager bruised due to ongoing domestic violence"};
        string[] callAddress = {"10 Jaffa Street, Jerusalem",
    "20 Emek Refaim Street, Jerusalem",
    "30 Agripas Street, Jerusalem",
    "40 Shmuel HaNavi Street, Jerusalem",
    "50 HaNevi'im Street, Jerusalem",
    "60 Gaza Street, Jerusalem",
    "4 Herzl Boulevard, Jerusalem",
    "40 Masliansky Street, Jerusalem",
    "10 Ben Yehuda Street, Jerusalem",
    "150 Hebron Road, Jerusalem",
    "5 Hillel Street, Jerusalem",
    "6 Korazin Street, Jerusalem",
    "35 King George Street, Jerusalem",
    "78 Ben Zvi Street, Jerusalem",
    "4 Devorah HaNeviah Street, Jerusalem",
    "15 Yehuda HaNasi Street, Jerusalem",
    "5 Aliash Street, Jerusalem",
    "15 Arlozorov Street, Jerusalem",
    "13 Shamai Street, Jerusalem",
    "10 Tavor Street, Jerusalem",
    "55 Ussishkin Street, Jerusalem",
    "7 Helene HaMalka Street, Jerusalem",
    "14 Lincoln Street, Jerusalem",
    "40 Zerach Street, Jerusalem",
    "22 Mea Shearim Street, Jerusalem",
    "10 King David Street, Jerusalem",
    "30 Ruth Street, Jerusalem",
    "9 Beit HaDfus Street, Jerusalem",
    "5 David Yellin Street, Jerusalem",
    "8 Shmuel HaNagid Street, Jerusalem",
    "15 HaTzanhanim Street, Jerusalem",
    "9 Rabbi Akiva Street, Jerusalem",
    "7 Greenberg Street, Jerusalem",
    "6 Henrietta Szold Street, Jerusalem",
    "5 Shmuel HaNagid Street, Jerusalem",
    "18 Beit Vagan Street, Jerusalem",
    "12 Neve Sha'anan Street, Jerusalem",
    "3 Rachel Imenu Street, Jerusalem",
    "15 Hillel Street, Jerusalem",
    "8 Bezalel Street, Jerusalem",
    "9 Kiryat Moshe Street, Jerusalem",
    "13 Pierre Koenig Street, Jerusalem",
    "24 Rabbi Herzog Street, Jerusalem",
    "5 Ein Gedi Street, Jerusalem",
    "8 HaOman Street, Jerusalem",
    "5 Ibn Shaprut Street, Jerusalem",
    "25 Bar Ilan Street, Jerusalem",
    "6 Yitzhak Kariv Street, Jerusalem",
    "17 Weizmann Street, Jerusalem",
    "9 Shmuel HaNagid Street, Jerusalem",
    "7 Ben Ze'ev Street, Jerusalem",
    "10 Rivlin Street, Jerusalem",
    "King George Street 17, Jerusalem",
    "Jaffa Street 210, Jerusalem",
    "Agron Street 8, Jerusalem",
    "Emek Refaim Street 43, Jerusalem",
    "Keren Hayesod Street 32, Jerusalem",
    "Ben Yehuda Street 16, Jerusalem",
    "Rachel Imenu Street 12, Jerusalem",
    "Haneviim Street 37, Jerusalem",
    "King David Street 23, Jerusalem",
    "Mamilla Avenue 6, Jerusalem",
    "Shlomzion Hamalka Street 8, Jerusalem",
    "HaPalmach Street 15, Jerusalem",
    "Jabotinsky Street 27, Jerusalem",
    "Ramban Street 10, Jerusalem",
    "Arlozorov Street 5, Jerusalem",
    "Ben Zvi Boulevard 89, Jerusalem",
    "Zalman Shneour Street 14, Jerusalem",
    "Chaim Vital Street 9, Jerusalem",
    "Rehavia, KKL Boulevard 5, Jerusalem",
    "Maale Hachamisha Street 3, Mevaseret Zion",
    "HaNasi Street 2, Kiryat Anavim",
    "Ein Karem Street 1, Jerusalem",
    "Beit HaKerem Street 21, Jerusalem",
    "Har Nof, Hayarden Street 18, Jerusalem",
    "Pisgat Ze'ev, Moshe Dayan Street 78, Jerusalem",
    "Armon Hanatziv, Zur Street 8, Jerusalem",
    "Abu Tor, Hebron Road 35, Jerusalem",
    "Gilo, Aharon Granot Street 5, Jerusalem",
    "Arnona, Yanovsky Street 15, Jerusalem",
    "HaTzayad Street 4, Tzur Hadassah",
    "Givat Shaul, Kanfei Nesharim Street 35, Jerusalem",
    "Talbieh, Dubnov Street 3, Jerusalem",
    "Nachlaot, Nissim Bachar Street 10, Jerusalem",
    "Katamon, Bilu Street 7, Jerusalem",
    "San Simon, Gad Street 11, Jerusalem",
    "Yemin Moshe, Yemin Moshe Street 2, Jerusalem",
    "Mount Scopus, Har Hatsofim Street 1, Jerusalem",
    "Beit HaKerem, HaRav Herzog Street 5, Jerusalem",
    "HaGefen Street 8, Moshav Ora",
    "HaPerach Street 3, Moshav Aminadav",
    "Neve Yaakov, Neve Yaakov Boulevard 15, Jerusalem",
    "Savyonim Street 22, Ma’ale Adumim",
    "Givat Mordechai, Ben Yosef Street 12, Jerusalem",
    "Talpiot, HaOman Street 9, Jerusalem",
    "French Hill, HaAskan Street 4, Jerusalem",
    "Motza, Hahaganah Street 2, Jerusalem",
    "Ramat Rachel, Kibbut, Jerusalem",
     "Har Nof, Hayarden Street 18, Jerusalem",
    "Pisgat Ze'ev, Moshe Dayan Street 78, Jerusalem",
    "Armon Hanatziv, Zur Street 8, Jerusalem",
    "Abu Tor, Hebron Road 35, Jerusalem",
    "Gilo, Aharon Granot Street 5, Jerusalem",
    "Arnona, Yanovsky Street 15, Jerusalem",
    "HaTzayad Street 4, Tzur Hadassah",
    "Givat Shaul, Kanfei Nesharim Street 35, Jerusalem",
    "Talbieh, Dubnov Street 3, Jerusalem",
    "Nachlaot, Nissim Bachar Street 10, Jerusalem",
    "Katamon, Bilu Street 7, Jerusalem",
    "San Simon, Gad Street 11, Jerusalem",
    "Yemin Moshe, Yemin Moshe Street 2, Jerusalem",
    "Mount Scopus, Har Hatsofim Street 1, Jerusalem",
    "Beit HaKerem, HaRav Herzog Street 5, Jerusalem",
    "HaGefen Street 8, Moshav Ora",
    "HaPerach Street 3, Moshav Aminadav",
    "Neve Yaakov, Neve Yaakov Boulevard 15, Jerusalem",
    "Savyonim Street 22, Ma’ale Adumim",
    "Givat Mordechai, Ben Yosef Street 12, Jerusalem",
    "Talpiot, HaOman Street 9, Jerusalem",
    "French Hill, HaAskan Street 4, Jerusalem",
    "Motza, Hahaganah Street 2, Jerusalem",
    "Ramat Rachel, Kibbut, Jerusalem"
 };
        double[] callLatitude = {35.22119546257982,
    35.22123577032626,
    35.215657362641764,
    35.224665335668945,
    35.221665622205705,
    35.21205714918638,
    35.19764122028971,
    35.18968763754008,
    35.21774235102746,
    35.21658163568471,
    35.21638617800225,
    35.209312878017535,
    35.2159379,
    35.2092867,
    35.224894,
    35.202146,
    35.201317,
    35.215236,
    35.216455,
    35.218053,
    35.213336,
    35.212081,
    35.221056,
    35.219964,
    35.190149,
    35.222337,
    35.222155,
    35.217356,
    35.189180,
    35.216914,
    35.214816,
    35.226296,
    35.192176,
    35.218002,
    35.191500,
    35.160694,
    35.214955,
    35.183917,
    35.200909,
    35.218213,
    35.217068,
    35.214544,
    35.195686,
    35.215021,
    35.208627,
    35.221349,
    35.211373,
    35.211675,
    35.218078,
    35.224944,
    35.198722,
    35.214792,
    35.220409,
    31.7815985,
    31.7882573,
    31.7755135,
    31.7623699,
31.7719907,
31.781439,
31.7635016,
31.7836055,
31.77417,
31.7776897,
31.7588316,
31.767599,
31.7708323,
31.7750074,
31.7738688,
31.785841,
32.0733788,
31.7908428,
31.7737814,
31.8109352,
31.809913,
31.7671028,
31.7804243,
31.752766,
31.8286017,
31.749309,
31.7660038,
31.7697764,
31.7557591,
31.719374,
31.7871046,
31.7683109,
31.7829636,
31.7625612,
31.7554844,
31.7715538,
31.768319,
31.769796,
31.7515142,
31.751705,
31.8426791,
31.777369,
31.762951,
31.7483217,
31.7541261,
31.8041316,
31.7403163,
31.785841,
32.0733788,
31.7908428,
31.7737814,
31.8109352,
31.809913,
31.7671028,
31.7804243,
31.752766,
31.8286017,
31.749309,
31.7660038,
31.7697764,
31.7557591,
31.719374,
31.7871046,
31.7683109,
31.7829636,
31.7625612,
31.7554844,
31.7715538,
31.768319,
31.769796,
31.7515142,
31.751705,
31.8426791,
31.777369,
31.762951,
31.7483217,
31.7541261,
31.8041316,
31.7403163
 };
        double[] callLongitude = {31.78130811991647,
    31.76543961779154,
    31.783470280009738,
    31.791048846509597,
    31.78391107912798,
    31.771053788166768,
    31.785777797665002,
    31.817876781912197,
    31.78186936361579,
    31.74578724034863,
    31.78068910546481,
    31.78255160178496,
    31.779769,
    31.7798566,
    31.789588,
    31.755770,
    31.755974,
    31.781584,
    31.772610,
    31.781239,
    31.782361,
    31.780486,
    31.7820255,
    31.775421,
    31.816719,
    31.787706,
    31.776943,
    31.7765005,
    31.785952,
    31.786426,
    31.780594,
    31.779808,
    31.816370,
    31.779226,
    31.817092,
    31.757365,
    31.780274,
    31.771251,
    31.771493,
    31.763331,
    31.780627,
    31.780728,
    31.786061,
    31.756843,
    31.769767,
    31.753902,
    31.748810,
    31.776448,
    31.794707,
    31.777475,
    31.790191,
    31.779852,
    31.780946,
    35.2167049,
    35.2066516,
    35.2184101,
    35.2183278,
    35.221737,
    35.2172503,
    35.2170943,
    35.2213712,
    35.222486,
    35.2248464,
    35.2228078,
    35.2123256,
    35.2191828,
    35.2165394,
    35.2159331,
    35.207386,
    34.7718297,
    35.1924441,
    35.2130265,
    35.1064139,
    35.120842,
    35.1623714,
    35.1902249,
    35.2241635,
    35.2391439,
    35.236047,
    35.22574,
    35.2033023,
    35.2230772,
    35.096958,
    35.1813603,
    35.2172931,
    35.2102015,
    35.2100508,
    35.2177412,
    35.2241405,
    35.21371,
    35.211126,
    35.1447801,
    35.142428,
    35.2429618,
    35.297955,
    35.197501,
    35.2115231,
    35.2279147,
    35.2356295,
    35.2178825,
    35.2391439,
    35.236047,
    35.22574,
    35.2033023,
    35.2230772,
    35.096958,
    35.1813603,
    35.2172931,
    35.2102015,
    35.2100508,
    35.2177412,
    35.2241405,
    35.21371,
    35.211126,
    35.1447801,
    35.142428,
    35.2429618,
    35.297955,
    35.197501,
    35.2115231,
    35.2279147,
    35.2356295,
    35.2178825
 };
        string tmpVerbalDecription = null;
        int tmpId = 0;

        for (int i = 0; i < 70; i++) // create a new call with all the tmp arguments
        {
            TypeCalls tmpTypeCall = (TypeCalls)s_rand.Next(0, Enum.GetValues(typeof(TypeCalls)).Length);
            switch (tmpTypeCall) // Matches event description to event randomly
            {
                case TypeCalls.medical_situation:
                    tmpVerbalDecription = medical_Descriptions[s_rand.Next(0, medical_Descriptions.Length)];
                    break;
                case TypeCalls.car_accident:
                    tmpVerbalDecription = accident_Descriptions[s_rand.Next(0, accident_Descriptions.Length)];
                    break;
                case TypeCalls.fall_from_hight:
                    tmpVerbalDecription = fall_Descriptions[s_rand.Next(0, fall_Descriptions.Length)];
                    break;
                case TypeCalls.violent_event:
                    tmpVerbalDecription = violence_Descriptions[s_rand.Next(0, violence_Descriptions.Length)];
                    break;
                case TypeCalls.domestic_violent:
                    tmpVerbalDecription = domestic_Descriptions[s_rand.Next(0, domestic_Descriptions.Length)];
                    break;
                default:
                    break;
            }
            DateTime start = s_dal.config!.Clock.AddMinutes(s_rand.Next(5, 10));  // define the random time
            DateTime finish = start.AddMinutes(s_rand.Next(5, 10));   // define the random ending time
            s_dal.call!.Create(new Call
            {
                Id = tmpId,
                TypeCall = tmpTypeCall,
                VerbalDecription = tmpVerbalDecription,
                FullAddressOfTheCall = callAddress[i],
                Latitude = callLatitude[i],
                Longitude = callLongitude[i],
                OpeningCallTime = start,
                MaxEndingCallTime = finish,
            });
        }
    }

    private static void CreateAssignments() // // creating 70 calls Assignments
    {
        int tmpId = 0;
        int tmpIndex = 0;
        int tmpCallId = 0;
        int tmpVolunteerId = 0;
        DateTime ? tmpFinishTime = null;
        DateTime tmpStartTime;
        EndKinds tmpEndKind = default;
        List<Call> tmpCalls = s_dal.call!.ReadAll();
        List<Volunteer> tmpVolenteers = s_dal.volunteer!.ReadAll();

        for (int i = 0; i < 50; i++)
        {
       
            
                tmpIndex = s_rand.Next(0, 50);
                tmpCallId = tmpCalls[tmpIndex].Id;
                tmpVolunteerId = tmpVolenteers[s_rand.Next(0, 16)].Id;
                tmpStartTime = tmpCalls[tmpIndex].OpeningCallTime.AddMinutes(1);
                if ((tmpCallId % 2) == 0)
                {
                    tmpFinishTime = tmpCalls[tmpIndex].MaxEndingCallTime; // the finish time of the assinment is exact like the call
                    tmpEndKind = (EndKinds)s_rand.Next(0, 2);//random end assinment
                }
                else
                {
                    tmpFinishTime = tmpCalls[tmpIndex].MaxEndingCallTime!.Value.AddMinutes(s_rand.Next(1, 2)); // the finish time of the assinment is more then the call
                    tmpEndKind = EndKinds.expired_cancellation;//5 expired calls
                }
                s_dal.assignment!.Create(new Assignment
                {
                    Id = tmpId,
                    CallId = tmpCallId,
                    VolunteerId = tmpVolunteerId,
                    StartTime = tmpStartTime,
                    FinishTime = tmpFinishTime,
                    EndKind = tmpEndKind,
                });
            
        }
    }

    public static void Do(IDal dal)
    {
    //    s_dalAssignment = dalAssignment ?? throw new NullReferenceException("DAL can not be null!");
    //    s_dalCall = dalCall ?? throw new NullReferenceException("DAL can not be null!");
    //    s_dalVolunteer = dalVolunteer ?? throw new NullReferenceException("DAL can not be null!");
    //    s_dalConfig = dalConfig ?? throw new NullReferenceException("DAL can not be null!");
        // assingd entities to use 

        s_dal=dal??throw new NullReferenceException("Reset configuration and List value...");  
        Console.WriteLine("Reset Configuration values and List values");

        //s_dalConfig.Reset();
        //s_dalCall.DeleteAll();
        //s_dalVolunteer.DeleteAll();
        //s_dalAssignment.DeleteAll();
        // reset and delete all the lists
        s_dal.RestDB();


        Console.WriteLine("Initializing Volunteer list");
        CreateVolunteers();
        Console.WriteLine("Initializing Call list");
        CreateCalls();
        Console.WriteLine("Initializing Assignment list");
        CreateAssignments();
        // create all the new list with the entities inside
    }
}
