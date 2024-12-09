using System.Diagnostics.Metrics;
using System.Threading.Channels;
using Dal;
using DalApi;
using DO;
namespace Dal;
using DalTest;

internal class Program
{
    //static readonly IDal s_dal = new DalList(); // stage 2 list only
    //static readonly IDal s_dal = new DalXml(); // stage 3 xml only
    static readonly IDal s_dal = Factory.Get; //stage 4

    static void Main(string[] args)
    {
        bool logOut = true;
        do
        {
            try
            {
                // print func
                ShowMainMenu(); 
                string input = Console.ReadLine()!;
                int key = int.Parse(input);
                switch ((MainMenu)key)
                {
                    case MainMenu.Exit:
                        logOut = false;
                        break;

                    case MainMenu.Sub_volunteer:   // sub menu
                        ShowVolunteerSubMenu("Volunteer");
                        break;

                    case MainMenu.Sub_call:   // sub menu
                        ShowCallSubMenu("Call");
                        break;

                    case MainMenu.Sub_assignment:   // sub menu
                        ShowAssignmentSubMenu("Assignment");
                        break;

                    case MainMenu.Initialization:  // initiolztion all the data
                        Initialization.Do();
                        break;

                    case MainMenu.Show_all:   // print all the lists
                        ShowAllTheData();
                        break;

                    case MainMenu.Sub_confing:   // sub menu
                        ShowConfingSubMenu();
                        break;

                    case MainMenu.Reset:   // Reset all the list and the conping setup
                        s_dal.Assignment!.DeleteAll();
                        s_dal.Call!.DeleteAll();
                        s_dal.Volunteer!.DeleteAll();
                        s_dal.Config!.Reset();
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        } while (logOut);
    }


    // func to Show the main menu
    private static void ShowMainMenu() 
    {
        Console.WriteLine("Enter 0 to Exit the main menu");
        Console.WriteLine("Enter 1 to Show sub menu for Volunteer");
        Console.WriteLine("Enter 2 to Show sub menu for call");
        Console.WriteLine("Enter 3 Show sub menu for assignment");
        Console.WriteLine("Enter 4 preforming data Initialization");
        Console.WriteLine("Enter 5 to Show all the data in the data base");
        Console.WriteLine("Enter 6 to Show sub menu for confing");
        Console.WriteLine("Enter 7 to Reset the data base and the confing");
    }
    
    
    // func to Show the Volunteer sub menu
    private static void ShowVolunteerSubMenu(string type) 
    {
        bool flag = true;
        int Id;

        do
        {
            Console.WriteLine("enter 0 to Exit the sub menu");
            Console.WriteLine($"enter 1 to Add a new {type} to the list");
            Console.WriteLine($"enter 2 to Show the {type} data");
            Console.WriteLine($"enter 3 to see all the list of {type}");
            Console.WriteLine($"enter 4 to Update the {type}");
            Console.WriteLine($"enter 5 to Delete the {type}");
            Console.WriteLine($"enter 6 to Delete all the {type}");
            try
            {
                string input = Console.ReadLine()!;
                int key = int.Parse(input);

                switch ((SubMenu)key)
                {
                    case SubMenu.Exit:
                        flag = false;
                        break;

                    case SubMenu.Add:
                        AddVolunteer();
                        break;

                    case SubMenu.Show:
                        Console.WriteLine("Enter Volunteer Id");
                        Id = int.Parse(Console.ReadLine()!);
                        Volunteer item = s_dal.Volunteer.Read(Id)!;
                        Console.WriteLine(item);
                        break;

                    case SubMenu.See_all:
                        IEnumerable<Volunteer> tmpForShow = s_dal.Volunteer.ReadAll();
                        foreach (var item1 in tmpForShow)
                        {
                            Console.WriteLine(item1);
                        }
                        break;

                    case SubMenu.Update:
                        UpdateVolunteer();
                        break;

                    case SubMenu.Delete:
                        Console.WriteLine("enter ID please");
                        Id = int.Parse(Console.ReadLine()!);
                        s_dal.Volunteer!.Delete(Id);
                        break;

                    case SubMenu.Delete_all:
                        s_dal.Volunteer!.DeleteAll();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        } while (flag);
    }
    
    
    // func to Show the call sub menu
    private static void ShowCallSubMenu(string type)  
    {
        bool flag = true;
        int Id;
        do
        {
            Console.WriteLine("enter 0 to Exit the sub menu");
            Console.WriteLine($"enter 1 to Add a new {type} to the list");
            Console.WriteLine($"enter 2 to Show the {type} data");
            Console.WriteLine($"enter 3 to see all the list of {type}");
            Console.WriteLine($"enter 4 to Update the {type}");
            Console.WriteLine($"enter 5 to Delete the {type}");
            Console.WriteLine($"enter 6 to Delete all the {type}");
            try
            {
                string input = Console.ReadLine()!;
                int key = int.Parse(input);
                switch ((SubMenu)key)
                {
                    case SubMenu.Exit:
                        flag = false;
                        break;

                    case SubMenu.Add:
                        AddCall();
                        break;

                    case SubMenu.Show:
                        Console.WriteLine("Enter Call Id");
                        Id = int.Parse(Console.ReadLine()!);
                        Call item = s_dal.Call.Read(Id)!;
                        Console.WriteLine(item);
                        break;

                    case SubMenu.See_all:
                        IEnumerable<Call> tmpForShow = s_dal.Call.ReadAll();
                        foreach (var item1 in tmpForShow)
                        {
                            Console.WriteLine(item1);
                        }
                        break;

                    case SubMenu.Update:
                        UpdateCall();
                        break;

                    case SubMenu.Delete:
                        Console.WriteLine("enter ID please");
                        Id = int.Parse(Console.ReadLine()!);
                        s_dal.Call!.Delete(Id);
                        break;

                    case SubMenu.Delete_all:
                        s_dal.Call!.DeleteAll();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        } while (flag);
    }
    
    
    // func to Show the assignment sub menu
    private static void ShowAssignmentSubMenu(string type)  
    {
        bool flag = true;
        int Id;
        do
        {
            Console.WriteLine("enter 0 to Exit the sub menu");
            Console.WriteLine($"enter 1 to Add a new {type} to the list");
            Console.WriteLine($"enter 2 to Show the {type} data");
            Console.WriteLine($"enter 3 to see all the list of {type}");
            Console.WriteLine($"enter 4 to Update the {type}");
            Console.WriteLine($"enter 5 to Delete the {type}");
            Console.WriteLine($"enter 6 to Delete all the {type}");
            try
            {
                string input = Console.ReadLine()!;
                int key = int.Parse(input);
                switch ((SubMenu)key)
                {
                    case SubMenu.Exit:
                        flag = false;
                        break;

                    case SubMenu.Add:
                        AddAssignment();
                        break;

                    case SubMenu.Show:
                        Console.WriteLine("Enter Assignment Id");
                        Id = int.Parse(Console.ReadLine()!);
                        Assignment item = s_dal.Assignment.Read(Id)!;
                        Console.WriteLine(item);
                        break;

                    case SubMenu.See_all:
                        IEnumerable<Assignment> tmpForShow = s_dal.Assignment.ReadAll();
                        foreach (var item1 in tmpForShow)
                        {
                            Console.WriteLine(item1);
                        }
                        break;

                    case SubMenu.Update:
                        UpdateAssignment();
                        break;

                    case SubMenu.Delete:
                        Console.WriteLine("enter ID please");
                        Id = int.Parse(Console.ReadLine()!);
                        s_dal.Assignment!.Delete(Id);
                        break;

                    case SubMenu.Delete_all:
                        s_dal.Assignment!.DeleteAll();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        } while (flag);
    } 
    
    
    // func to Show the config sub menu
    private static void ShowConfingSubMenu() 
    {
        bool flag = true;
        do
        {
            Console.WriteLine("enter 0 to Exit the sub menu");
            Console.WriteLine($"enter 1 to Add a minute to the clock");
            Console.WriteLine($"enter 2 to Add a hour to the clock");
            Console.WriteLine($"enter 3 to see the corrent clock time");
            Console.WriteLine($"enter 4 to Update a value");
            Console.WriteLine($"enter 5 to see a specific value");
            Console.WriteLine($"enter 6 to Reset all values");
            try
            {
                string input = Console.ReadLine()!;
                int key = int.Parse(input);
                switch ((ConfingSubMenu)key)
                {
                    case ConfingSubMenu.Exit:
                        flag = false;
                        break;

                    case ConfingSubMenu.Minutes:
                        s_dal.Config.Clock = s_dal.Config.Clock.AddMinutes(1);
                        break;

                    case ConfingSubMenu.Hours:
                        s_dal.Config.Clock = s_dal.Config.Clock.AddHours(1);
                        break;

                    case ConfingSubMenu.Show_time:
                        Console.WriteLine(s_dal.Config.Clock);
                        break;

                    case ConfingSubMenu.Set_new_value:
                        int choise;
                        Console.WriteLine("to change the time press 1, to change the Risk Rnge press 2");
                        choise = int.Parse(Console.ReadLine()!);
                        if (choise == 1)
                        {
                            Console.WriteLine("Enter a new time");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime bdt)) throw new FormatException("time is invalid!");
                            DateTime tmpFinishTime = bdt;
                            s_dal.Config.Clock = bdt;
                        }
                        else if (choise == 2)
                        {
                            Console.WriteLine("Enter a new risk range (in secondes) ");
                            if (int.TryParse(Console.ReadLine(), out int riskrangeInsecondes))
                                s_dal.Config.RiskRnge = TimeSpan.FromSeconds(riskrangeInsecondes);
                        }
                        break;

                    case ConfingSubMenu.Show_spesific_value:
                        int choise2;
                        Console.WriteLine("to see the time press 1, to see the Risk Rnge press 2");
                        choise2 = int.Parse(Console.ReadLine()!);
                        if (choise2 == 1)
                            Console.WriteLine(s_dal.Config.Clock);
                        else if (choise2 == 2)
                            Console.WriteLine(s_dal.Config.RiskRnge);
                        break;

                    case ConfingSubMenu.Reset:
                        s_dal.Config!.Reset();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        } while (flag);
    }
    
    
    // func to Add entity to rhe list
    private static void AddVolunteer()
    {
        Console.WriteLine("enter ID please");
        int Id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("enter Full Name please");
        string FullName = Console.ReadLine()!;
        Console.WriteLine("enter Phone please");
        string Phone = Console.ReadLine()!;
        Console.WriteLine("enter Email please");
        string Email = Console.ReadLine()!;
        Console.WriteLine("enter Password please");
        string? Password = Console.ReadLine()!;
        Console.WriteLine("enter Address please");
        string? Address = Console.ReadLine()!;
        Console.WriteLine("enter Latitude please");
        double? Latitude = double.Parse(Console.ReadLine()!);
        Console.WriteLine("enter Longitude please");
        double? Longitude = double.Parse(Console.ReadLine()!);
        Console.WriteLine("enter Distance Type, 0 to Air, 1 to Walk, 2 to Car ");
        string input = Console.ReadLine()!;
        int key = int.Parse(input);
        DistanceTypes distanceTypes = (DistanceTypes)key;
        Console.WriteLine("enter Maximum Distance please");
        double? MaximumDistance = double.Parse(Console.ReadLine()!);
        Volunteer addvolunteer = new Volunteer(Id, FullName, Phone, Email, Password, Address, Latitude, Longitude, Roles.Volunteer, true, MaximumDistance, distanceTypes);
        s_dal.Volunteer!.Create(addvolunteer);
    }
    
    // func to Update a existing entity
    private static void UpdateVolunteer() 
    {
        Console.WriteLine("enter ID please");
        int Id = int.Parse(Console.ReadLine()!);
        Volunteer tmp = s_dal.Volunteer.Read(Id)!;
        Console.WriteLine(tmp);
        // Show the entity first
        Console.WriteLine("enter Full Name please");
        string FullName = Console.ReadLine()!;
        Console.WriteLine("enter Phone please");
        string Phone = Console.ReadLine()!;
        Console.WriteLine("enter Email please");
        string Email = Console.ReadLine()!;
        Console.WriteLine("enter Password please");
        string? Password = Console.ReadLine()!;
        Console.WriteLine("enter Address please");
        string? Address = Console.ReadLine()!;
        Console.WriteLine("enter Latitude please");
        double? Latitude = double.Parse(Console.ReadLine()!);
        Console.WriteLine("enter Longitude please");
        double? Longitude = double.Parse(Console.ReadLine()!);
        Console.WriteLine("enter Distance Type, 0 to Air, 1 to Walk, 2 to Car ");
        string input = Console.ReadLine()!;
        int key = int.Parse(input!);
        DistanceTypes distanceTypes = (DistanceTypes)key;
        Console.WriteLine("enter Maximum Distance please");
        double? MaximumDistance = double.Parse(Console.ReadLine()!);
        Volunteer Updatevolunteer = new Volunteer(Id, FullName, Phone, Email, Password, Address, Latitude, Longitude, Roles.Volunteer, true, MaximumDistance, distanceTypes);
        s_dal.Volunteer.Update(Updatevolunteer);
    }
    
    
    // func to Add entity to rhe list
    private static void AddCall()  
    {
        Console.WriteLine("enter Type call, 0 to Medical_situation, 1 to Car_accident, 2 to Fall_from_hight, 3 to Violent_event, 4 to Domestic_violent");
        string input = Console.ReadLine()!;
        int key = int.Parse(input);
        TypeCalls tmpTypeCall = (TypeCalls)key;
        Console.WriteLine("enter Verbal Decription please");
        string? tmpVerabal = Console.ReadLine();
        Console.WriteLine("enter call adsress please");
        string? Address = Console.ReadLine()!;
        Console.WriteLine("enter Latitude please");
        double Latitude = double.Parse(Console.ReadLine()!);
        Console.WriteLine("enter Longitude please");
        double Longitude = double.Parse(Console.ReadLine()!);
        DateTime tmpOpeningCallTime = s_dal.Config.Clock;
        Console.WriteLine("enter end call time please");
        int time = int.Parse(Console.ReadLine()!);
        DateTime tmpEndCallTime = s_dal.Config.Clock.AddMinutes(time);
        Call addcall = new Call(0, tmpTypeCall, tmpVerabal, Address, Latitude, Longitude, tmpOpeningCallTime, tmpEndCallTime);
        s_dal.Call!.Create(addcall);
    }
    
    
    // func to Update a existing entity
    private static void UpdateCall()  
    {
        Console.WriteLine("enter ID please");
        int Id = int.Parse(Console.ReadLine()!);
        Call tmp = s_dal.Call.Read(Id)!;
        Console.WriteLine(tmp);
        // Show the entity first
        Console.WriteLine("enter Type call, 0 to Medical_situation, 1 to Car_accident, 2 to Fall_from_hight, 3 to Violent_event, 4 to Domestic_violent");
        TypeCalls tmpTypeCall = (TypeCalls)Enum.Parse(typeof(TypeCalls), Console.ReadLine()!);
        Console.WriteLine("enter Verbal Decription please");
        string? tmpVerabal = Console.ReadLine();
        Console.WriteLine("enter call adsress please");
        string? Address = Console.ReadLine()!;
        Console.WriteLine("enter Latitude please");
        double Latitude = double.Parse(Console.ReadLine()!);
        Console.WriteLine("enter Longitude please");
        double Longitude = double.Parse(Console.ReadLine()!);
        DateTime tmpOpeningCallTime = s_dal.Config.Clock;
        Console.WriteLine("enter end call time please");
        int time = int.Parse(Console.ReadLine()!);
        DateTime tmpEndCallTime = s_dal.Config.Clock.AddMinutes(time);
        Call updateCall = new Call(Id, tmpTypeCall, tmpVerabal, Address, Latitude, Longitude, tmpOpeningCallTime, tmpEndCallTime);
        s_dal.Call.Update(updateCall);
    }
    
    
    // func to Add entity to rhe list
    private static void AddAssignment()  
    {
        Console.WriteLine("enter call Id please");
        int tmpCallId = int.Parse(Console.ReadLine()!);
        Console.WriteLine("enter Volunteer Id please");
        int tmpVolunteerId = int.Parse(Console.ReadLine()!);
        DateTime tmpStartTime = s_dal.Config.Clock;
        Console.WriteLine("enter Assignment end kind, 0 to Treated, 1 to Self_cancellation, 2 to Administrator_cancellation, 3 to Expired_cancellation");
        EndKinds tmpEndKind = (EndKinds)Enum.Parse(typeof(EndKinds), Console.ReadLine()!);
        Assignment addAssignment = new Assignment(0, tmpCallId, tmpVolunteerId, tmpStartTime, null, tmpEndKind);
        s_dal.Assignment!.Create(addAssignment);
    }
    
    
    // func to Update a existing entity
    private static void UpdateAssignment()  
    {
        Console.WriteLine("enter ID please");
        int Id = int.Parse(Console.ReadLine()!);
        Assignment? tmp = s_dal.Assignment.Read(Id);
        Console.WriteLine(tmp);
        // Show the entity first
        Console.WriteLine("enter call Id please");
        int tmpCallId = int.Parse(Console.ReadLine()!);
        Console.WriteLine("enter Volunteer Id please");
        int tmpVolunteerId = int.Parse(Console.ReadLine()!);
        DateTime tmpStartTime = s_dal.Config.Clock;
        Console.WriteLine("enter Assignment end kind, 0 to Treated, 1 to Self_cancellation, 2 to Administrator_cancellation, 3 to Expired_cancellation");
        EndKinds tmpEndKind = (EndKinds)Enum.Parse(typeof(EndKinds), Console.ReadLine()!);
        Assignment UpdateAssignment = new Assignment(Id, tmpCallId, tmpVolunteerId, tmpStartTime, null, tmpEndKind);
        s_dal.Assignment.Update(UpdateAssignment);
    }
    
    
    
    // print all the lists of all the entities
    private static void ShowAllTheData() 
    {
        IEnumerable <Volunteer> tmpForShow = s_dal.Volunteer.ReadAll();
        foreach (var item in tmpForShow)
        {
            Console.WriteLine(item);
        }
        IEnumerable <Call> tmpForShow1 = s_dal.Call.ReadAll();
        foreach (var item1 in tmpForShow1)
        {
            Console.WriteLine(item1);
        }
        IEnumerable <Assignment> tmpForShow2 = s_dal.Assignment.ReadAll();
        foreach (var item2 in tmpForShow2)
        {
            Console.WriteLine(item2);
        }
    }
}



