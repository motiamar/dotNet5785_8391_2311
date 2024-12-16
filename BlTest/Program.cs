using System.Linq.Expressions;
using DalApi;
using DalTest;
using DO;
using BO;
namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
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
                switch ((BMainMenu)key)
                {
                    case BMainMenu.Exit:
                        logOut = false;
                        break;

                    case BMainMenu.Sub_volunteer:   // sub menu
                        ShowVolunteerMenu();
                        break;

                    case BMainMenu.Sub_call:   // sub menu
                        ShowCallMenu();
                        break;

                    case BMainMenu.Sub_admin:   // sub menu
                        ShowAdminMenu();
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

    /// <summary>
    /// print the main menu
    /// </summary>
    private static void ShowMainMenu()
    {
        Console.WriteLine("Enter 0 to Exit the main menu");
        Console.WriteLine("Enter 1 to Show sub menu for Volunteer");
        Console.WriteLine("Enter 2 to Show sub menu for call");
        Console.WriteLine("Enter 3 Show sub menu for Admin");
    }

    /// <summary>
    /// print and exicut sub menu for the Volunteer 
    /// </summary>
    private static void ShowVolunteerMenu()
    {
        bool flag = true;
        int Id;

        do
        {
            Console.WriteLine("enter 0 to Exit the sub menu");
            Console.WriteLine($"enter 1 to login the system");
            Console.WriteLine($"enter 2 to Show the volunteers list");
            Console.WriteLine($"enter 3 to see a volunteer details");
            Console.WriteLine($"enter 4 to Update a volunteer");
            Console.WriteLine($"enter 5 to Delete a volunteer");
            Console.WriteLine($"enter 6 to add a volunteer");
            try
            {
                string input = Console.ReadLine()!;
                int key = int.Parse(input);

                switch ((BSubVolMenu)key)
                {
                    case BSubVolMenu.Exit:
                        flag = false;
                        break;

                    case BSubVolMenu.Login: // enter the system
                        Console.WriteLine("please eneter user name and a password to eneter the system");
                        string name = Console.ReadLine()!;
                        string password = Console.ReadLine()!;
                        string? role = s_bl.Volunteer.SystemEnter(name, password);
                        Console.WriteLine(role);
                        break;

                    case BSubVolMenu.Show_volunteers: // show the list
                        bool? active = null;
                        Console.WriteLine("enetr 0 to see all volunteers end 1 to see only active and 2 to see only nun active");
                        int choose = int.Parse(Console.ReadLine()!);
                        if (choose == 1)
                            active = true;
                        if (choose == 2)
                            active = false;                      
                        Console.WriteLine("enter 1 to order by the Full Name");
                        Console.WriteLine("enetr 3 to order by TreatedCalls, 4 to order by the CanceledCalls");
                        Console.WriteLine("enetr 5 to order by ExpiredCalls, 6 to order by the CorrentCallId");
                        VollInListFilter? sort = (VollInListFilter)int.Parse(Console.ReadLine()!);
                        foreach (var item in s_bl.Volunteer.ReadAll(active, sort))
                        {
                            Console.WriteLine(item);
                        }
                        break;

                    case BSubVolMenu.Show_volunteer: // show the entity
                        Console.WriteLine("enter ID please");
                        Id = int.Parse(Console.ReadLine()!);
                        Console.WriteLine(s_bl.Volunteer.Read(Id));
                        break;

                    case BSubVolMenu.Update_volunteer: // update the entity
                        UpdateVolunteer();                       
                        break;

                    case BSubVolMenu.Delete_volunteer: // delete the entity
                        Console.WriteLine("enter ID please");
                        Id = int.Parse(Console.ReadLine()!);
                        s_bl.Volunteer!.Delete(Id);
                        break;

                    case BSubVolMenu.Add_volunteer: // add the entity
                        AddVolunteer();
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

    /// <summary>
    /// print and exicut sub menu for the call
    /// </summary>
    private static void ShowCallMenu()
    {
        bool flag = true;
        int Id;

        do
        {
            Console.WriteLine("enter 0 to Exit the sub menu");
            Console.WriteLine($"enter 1 to see the array of calls");
            Console.WriteLine($"enter 2 to Show the calls list");
            Console.WriteLine($"enter 3 to see a call details");
            Console.WriteLine($"enter 4 to Update a call");
            Console.WriteLine($"enter 5 to Delete a call");
            Console.WriteLine($"enter 6 to add a call");
            Console.WriteLine($"enter 7 to see a list of close calls treated by a volunteer");
            Console.WriteLine($"enter 8 to see a list of open calls to choose by a volunteer");
            Console.WriteLine($"enter 9 to end a call in treatment");
            Console.WriteLine($"enter 10 to cancale a call in treatment");
            Console.WriteLine($"enter 11 to choose a call to treat");

            try
            {
                string input = Console.ReadLine()!;
                int key = int.Parse(input);

                switch ((BSubCallMenu)key)
                {
                    case BSubCallMenu.Exit:
                        flag = false;
                        break;

                    case BSubCallMenu.Array_calls: // show the array of calls
                        int[] array = s_bl.Call.ArrayStatus();
                        for (int i = 0; i < array.Length; i++)
                        {
                            Console.WriteLine($"status {i}: {array[i]}");
                        }
                        break;

                    case BSubCallMenu.Show_calls: // show the list
                        ShowCalls();                      
                        break;

                    case BSubCallMenu.Show_call: // show the entity
                        Console.WriteLine("enter call ID please");
                        Id = int.Parse(Console.ReadLine()!);
                        Console.WriteLine(s_bl.Call.Read(Id)!);
                        break;

                    case BSubCallMenu.Update_call: // update the entity
                        UpdateCall();
                        break;

                    case BSubCallMenu.Delete_call: // delete the entity
                        Console.WriteLine("enter call ID please");
                        Id = int.Parse(Console.ReadLine()!);
                        s_bl.Call.Delete(Id);
                        break;

                    case BSubCallMenu.Add_call: // add the entity
                        AddCall();
                        break;

                    case BSubCallMenu.Show_close_calls: // show the list of close calls
                        ShowCloseCalls();                        
                        break;

                    case BSubCallMenu.Show_open_calls: // show the list of open calls
                        ShowOpenCalls();
                        break;

                    case BSubCallMenu.End_call: // end the call
                        Console.WriteLine("enter call ID please");
                        int callId = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter assignment ID please");
                        int assingmentId = int.Parse(Console.ReadLine()!);
                        s_bl.Call.EndAssignment(callId, assingmentId);
                        break;

                    case BSubCallMenu.Cancel_call: // cancel the call
                        Console.WriteLine("enter call ID please");
                        int callId2 = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter assignment ID please");
                        int assingmentId2 = int.Parse(Console.ReadLine()!);
                        s_bl.Call.CanceleAssignment(callId2, assingmentId2);
                        break;

                    case BSubCallMenu.Choose_call: // choose the call by the volunteer
                        Console.WriteLine("enter volunteer ID please");
                        int volunteerId = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter call ID please");
                        int callId3 = int.Parse(Console.ReadLine()!);
                        s_bl.Call.ChooseCall(volunteerId, callId3);
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

    /// <summary>
    /// print and exicut sub menu for the admin
    /// </summary>
    private static void ShowAdminMenu()
    {
        bool flag = true;
        do
        {
            Console.WriteLine("enter 0 to Exit the sub menu");
            Console.WriteLine($"enter 1 to see the clock");
            Console.WriteLine($"enter 2 to advance the clock");
            Console.WriteLine($"enter 3 to see the risk range");
            Console.WriteLine($"enter 4 to set the risk range");
            Console.WriteLine($"enter 5 to reset the data base");
            Console.WriteLine($"enter 6 to initialize the data base");
            try
            {
                string input = Console.ReadLine()!;
                int key = int.Parse(input);

                switch ((BSubAdmMenu)key)
                {
                    case BSubAdmMenu.Exit:
                        flag = false;
                        break;

                    case BSubAdmMenu.See_clock: // show the clock

                        Console.WriteLine(s_bl.Admin.GetClock());
                        break;
                    case BSubAdmMenu.Change_clock: // advance the clock
                        Console.WriteLine("to add a minute press 0, to add an hour press 1, to add a day press 2, to add a month press 3, to add a year press 4");
                        TimeUnit unit = (TimeUnit)int.Parse(Console.ReadLine()!);
                        s_bl.Admin.ForwordClock(unit);
                        break;

                    case BSubAdmMenu.See_riskRange: // show the risk range
                        Console.WriteLine(s_bl.Admin.GetMaxRange());
                        break;

                    case BSubAdmMenu.Change_RiskRange: // set the risk range
                        Console.WriteLine("enter the new value please");
                        TimeSpan maxRange = TimeSpan.Parse(Console.ReadLine()!);
                        s_bl.Admin.SetMaxRange(maxRange);
                        break;

                    case BSubAdmMenu.Reset: // reset the data base
                        s_bl.Admin.ResetDB();
                        break;

                    case BSubAdmMenu.Initialize: // initialize the data base
                        s_bl.Admin.InitializeDB(); 
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

    /// <summary>
    /// update the volunteer entity
    /// </summary>
    private static void UpdateVolunteer()
    {
        Console.WriteLine("enter ID please");
        int Id = int.Parse(Console.ReadLine()!);
        var tmp = s_bl.Volunteer.Read(Id)!;
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
        Console.WriteLine("enter Address please in formt: street, number, city");
        string? Address = Console.ReadLine()!;
        Console.WriteLine("enter Role Type, 0 to Voluntter, 1 to Manager");
        string input2 = Console.ReadLine()!;
        int key2 = int.Parse(input2!);
        BRoles role = (BRoles)key2;
        Console.WriteLine("eneter 0 if the volunteer is active and 1 if not");
        bool Active = int.Parse(Console.ReadLine()!) == 0;
        Console.WriteLine("enter Maximum Distance please");
        double? MaximumDistance = double.Parse(Console.ReadLine()!);
        Console.WriteLine("enter Distance Type, 0 to Air, 1 to Walk, 2 to Car ");
        string input = Console.ReadLine()!;
        int key = int.Parse(input!);
        BDistanceTypes distanceTypes = (BDistanceTypes)key;
        BO.Volunteer Updatevolunteer = new BO.Volunteer
        {
            Id = Id,
            FullName = FullName,
            Phone = Phone,
            Email = Email,
            Password = Password,
            Address = Address,
            role = role,
            Active = Active,
            MaximumDistance = MaximumDistance,
            DistanceType = distanceTypes,
            TreatedCalls = tmp.TreatedCalls,
            CanceledCalls = tmp.CanceledCalls,
            ExpiredCalls = tmp.ExpiredCalls,
            CorrentCall = tmp.CorrentCall
        };
        s_bl.Volunteer.Update(Id, Updatevolunteer);
    }


    /// <summary>
    /// add the volunteer entity
    /// </summary>
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
        Console.WriteLine("enter Address please in formt: street, number, city");
        string? Address = Console.ReadLine()!;
        Console.WriteLine("enter Role Type, 0 to Voluntter, 1 to Manager");
        string input2 = Console.ReadLine()!;
        int key2 = int.Parse(input2!);
        BRoles role = (BRoles)key2;
        Console.WriteLine("eneter 0 if the volunteer is active and 1 if not");
        bool Active = int.Parse(Console.ReadLine()!) == 0;
        Console.WriteLine("enter Maximum Distance please");
        double? MaximumDistance = double.Parse(Console.ReadLine()!);
        Console.WriteLine("enter Distance Type, 0 to Air, 1 to Walk, 2 to Car ");
        string input = Console.ReadLine()!;
        int key = int.Parse(input!);
        BDistanceTypes distanceTypes = (BDistanceTypes)key;
        BO.Volunteer newVolunteer = new BO.Volunteer
        {
            Id = Id,
            FullName = FullName,
            Phone = Phone,
            Email = Email,
            Password = Password,
            Address = Address,
            role = role,
            Active = Active,
            MaximumDistance = MaximumDistance,
            DistanceType = distanceTypes,        
        };
        s_bl.Volunteer.Create( newVolunteer);
    }

    /// <summary>
    /// print the call list
    /// </summary>
    private static void ShowCalls()
    {
        Console.WriteLine("enetr 0 to filter the list by the enum fild or 1 to continue");
        CallInListFilter? filter = null;
        object? value = null;
        CallInListFilter? sort = null;
        int choose = int.Parse(Console.ReadLine()!);
        if (choose == 0)
        {
            ShowCallEnum();
            filter = (CallInListFilter)int.Parse(Console.ReadLine()!);
            switch(filter)
            {
                case CallInListFilter.Id:
                    Console.WriteLine("enetr the Id to compre by");
                    value = int.Parse(Console.ReadLine()!);
                    break;
                case CallInListFilter.CallId:
                    Console.WriteLine("enetr the CallId to compre by");
                    value = int.Parse(Console.ReadLine()!);
                    break;
                case CallInListFilter.Type:
                    Console.WriteLine("enetr the Type to compre by");
                    value = (BTypeCalls)int.Parse(Console.ReadLine()!);
                    break;
                case CallInListFilter.CallOpenTime:
                    Console.WriteLine("enetr the CallOpenTime to compre by");
                    value = DateTime.Parse(Console.ReadLine()!);
                    break;
                case CallInListFilter.CallMaxCloseTime:
                    Console.WriteLine("enetr the CallMaxCloseTime to compre by");
                    value = DateTime.Parse(Console.ReadLine()!);
                    break;
                case CallInListFilter.LastVolunteerName:
                    Console.WriteLine("enetr the LastVolunteerName to compre by");
                    value = Console.ReadLine()!;
                    break;
                case CallInListFilter.TotalTreatmentTime:
                    Console.WriteLine("enetr the TotalTreatmentTime to compre by");
                    value = TimeSpan.Parse(Console.ReadLine()!);
                    break;
                case CallInListFilter.CallStatus:
                    Console.WriteLine("enetr the CallStatus to compre by");
                    value = (BCallStatus)int.Parse(Console.ReadLine()!);
                    break;
                case CallInListFilter.SumOfAssignments:
                    Console.WriteLine("enetr the SumOfAssignments to compre by");
                    value = int.Parse(Console.ReadLine()!);
                    break;
            }
        }
        Console.WriteLine("enetr 0 to order the list by the enum fild or 1 to continue");
        int choose2 = int.Parse(Console.ReadLine()!);
        if (choose2 == 0)
        {
            ShowCallEnum();
            sort = (CallInListFilter)int.Parse(Console.ReadLine()!);
        }
        foreach (var item in s_bl.Call.ReadAll(filter, value, sort))
        {
            Console.WriteLine(item);
        }
    }

    /// <summary>
    /// print the call enum
    /// </summary>
    private static void ShowCallEnum()
    {
        Console.WriteLine("enetr 0 for Id, enter 1 for CallId, enetr 2 for Type, enter 3 for CallOpenTime");
        Console.WriteLine("enetr 4 for CallMaxCloseTime, enetr 5 for LastVolunteerName, enetr 6 for TotalTreatmentTime");
        Console.WriteLine("enetr 7 for CallStatus, enter 5 for SumOfAssignments");              
    }

    /// <summary>
    /// update the call entity
    /// </summary>
    private static void UpdateCall()
    {
        Console.WriteLine("enter call ID please");
        int Id = int.Parse(Console.ReadLine()!);
        var tmp = s_bl.Call.Read(Id)!;
        Console.WriteLine(tmp);
        // Show the entity first      
        Console.WriteLine("enter Type please, 1 to Medical_situation, 2 to Car_accident, 3 to Fall_from_hight, 3 to Violent_event, 4 to Domestic_violent, 5 to None");
        BTypeCalls type = (BTypeCalls)int.Parse(Console.ReadLine()!);       
        Console.WriteLine("enter descrption please");
        string descrption = Console.ReadLine()!;
        Console.WriteLine("enter Call Address please in formt: street, number, city");
        string CallAddress = Console.ReadLine()!;
        Console.WriteLine("enter Call Max Close Time please");
        DateTime CallMaxCloseTime = DateTime.Parse(Console.ReadLine()!);
        BO.Call UpdateCall = new BO.Call
        {
            Id = Id,
            Type = type,
            Description = descrption,
            CallAddress = CallAddress,
            CallOpenTime = s_bl.Admin.GetClock(),
            CallMaxCloseTime = CallMaxCloseTime,
            CallStatus = tmp.CallStatus,
            callAssignInLists = tmp.callAssignInLists,    
        };
        s_bl.Call.Update(UpdateCall);
    }

    /// <summary>
    /// add the call entity
    /// </summary>
    private static void AddCall()
    {        
        int Id = 0;
        Console.WriteLine("enter Type please, 1 to Medical_situation, 2 to Car_accident, 3 to Fall_from_hight, 3 to Violent_event, 4 to Domestic_violent, 5 to None");
        BTypeCalls type = (BTypeCalls)int.Parse(Console.ReadLine()!);
        Console.WriteLine("enter descrption please");
        string descrption = Console.ReadLine()!;
        Console.WriteLine("enter Call Address please in formt: street, number, city");
        string CallAddress = Console.ReadLine()!;
        Console.WriteLine("enter Call Max Close Time please");
        DateTime CallMaxCloseTime = DateTime.Parse(Console.ReadLine()!);
        BO.Call newCall = new BO.Call
        {
            Id = Id,
            Type = type,
            Description = descrption,
            CallAddress = CallAddress,
            CallOpenTime = s_bl.Admin.GetClock(),
            CallMaxCloseTime = CallMaxCloseTime,
            CallStatus = BCallStatus.Open,
            callAssignInLists = new List<BO.CallAssignInList>(),
        };
        s_bl.Call.Create(newCall);
    }

    /// <summary>
    /// print the close calls list
    /// </summary>
    private static void ShowCloseCalls()
    {
        Console.WriteLine("enetr volunteer id to show the list for");
        int Id = int.Parse(Console.ReadLine()!);
        BTypeCalls? filter = null;
        CloseCallInListFilter? sort = null;
        Console.WriteLine("enter 0 to filter the list by the call type or 1 to continue");
        int choose = int.Parse(Console.ReadLine()!);
        if (choose == 0)
        {
            Console.WriteLine("enter Type please, 1 to Medical_situation, 2 to Car_accident, 3 to Fall_from_hight, 3 to Violent_event, 4 to Domestic_violent, 5 to None");
            filter = (BTypeCalls)int.Parse(Console.ReadLine()!);
        }
        Console.WriteLine("enter 0 to order the list by the enum fild or 1 to continue");
        int choose2 = int.Parse(Console.ReadLine()!);
        if (choose2 == 0)
        {
            ShowCloseCallEnum();
            sort = (CloseCallInListFilter)int.Parse(Console.ReadLine()!);
        }
        foreach (var item in s_bl.Call.GetCloseCallList(Id, filter, sort))
        {
            Console.WriteLine(item);
        }
    }

    /// <summary>
    /// print the open call list
    /// </summary>
    private static void ShowOpenCalls()
    {
        Console.WriteLine("enetr volunteer id to show the list for");
        int Id = int.Parse(Console.ReadLine()!);
        BTypeCalls? filter = null;
        OpenCallInListFilter? sort = null;
        Console.WriteLine("enter 0 to filter the list by the call type or 1 to continue");
        int choose = int.Parse(Console.ReadLine()!);
        if (choose == 0)
        {
            Console.WriteLine("enter Type please, 1 to Medical_situation, 2 to Car_accident, 3 to Fall_from_hight, 3 to Violent_event, 4 to Domestic_violent, 5 to None");
            filter = (BTypeCalls)int.Parse(Console.ReadLine()!);
        }
        Console.WriteLine("enter 0 to order the list by the enum fild or 1 to continue");
        int choose2 = int.Parse(Console.ReadLine()!);
        if (choose2 == 0)
        {
            ShowOpenCallEnum();
            sort = (OpenCallInListFilter)int.Parse(Console.ReadLine()!);
        }
        foreach (var item in s_bl.Call.GetOpenCallList(Id, filter, sort))
        {
            Console.WriteLine(item);
        }
    }

    /// <summary>
    /// print the close call enum
    /// </summary>
    private static void ShowCloseCallEnum()
    {
        Console.WriteLine("enetr 0 for Id, enter 1 for type, enetr 2 for call adrress, enter 3 for CallOpenTime");
        Console.WriteLine("enetr 4 for CallEnterTime, enetr 5 for CallCloseTime, enetr 6 for EndKind");
    }

    /// <summary>
    /// print the open call enum
    /// </summary>
    private static void ShowOpenCallEnum()
    {
        Console.WriteLine("enetr 0 for Id, enter 1 for type, enetr 2 for Descrption, enter 3 for call adrress");
        Console.WriteLine("enetr 4 for CallOpenTime, enetr 5 for CallMaxCloseTime, enetr 6 for CallDistance");
    }   
}


    
   
    
    
    
