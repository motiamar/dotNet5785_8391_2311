using System.Diagnostics.Metrics;
using System.Threading.Channels;
using Dal;
using DalApi;
using DO;

namespace DalTest
{
    internal class Program
    {
        // set gonpinf entities to use in the main
        private static IConfig? s_dalConfig = new ConfigImplementation();
        private static IAssignment? s_dalAssignment = new AssignmentImplementation();
        private static ICall? s_dalCall = new CallImplementation();
        private static IVolunteer? s_dalVolunteer = new VolunteerImplementation();

        private static void ShowMainMenu() // func to show the main menu
        {
            Console.WriteLine("Enter 0 to exit the main menu");
            Console.WriteLine("Enter 1 to show sub menu for volunteer");
            Console.WriteLine("Enter 2 to show sub menu for call");
            Console.WriteLine("Enter 3 show sub menu for assignment");
            Console.WriteLine("Enter 4 preforming data initialization");
            Console.WriteLine("Enter 5 to show all the data in the data base");
            Console.WriteLine("Enter 6 to show sub menu for confing");
            Console.WriteLine("Enter 7 to reset the data base and the confing");
        }
        private static void ShowVolunteerSubMenu(string type)  // func to show the volunteer sub menu
        {
            Console.WriteLine("enter 0 to exit the sub menu");
            Console.WriteLine($"enter 1 to add a new {type} to the list");
            Console.WriteLine($"enter 2 to show the {type} data");
            Console.WriteLine($"enter 3 to see all the list of {type}");
            Console.WriteLine($"enter 4 to update the {type}");
            Console.WriteLine($"enter 5 to delete the {type}");
            Console.WriteLine($"enter 6 to delete all the {type}");
            SubMenu key = (SubMenu)Console.Read();
            bool flag = true;
            int Id;
            do
            {
                switch (key) 
                {
                    case SubMenu.exit:
                        flag = false;
                        break;

                    case SubMenu.add:
                        AddVolunteer();
                        break;

                    case SubMenu.show:
                        Console.WriteLine("Enter volunteer Id");
                        Id = Console.Read();
                        Volunteer item = s_dalVolunteer.Read(Id);
                        Console.WriteLine(item);
                        break;

                    case SubMenu.see_all:
                        List<Volunteer> tmpForShow = s_dalVolunteer.ReadAll();
                        foreach (var item1 in tmpForShow)
                        {
                            Console.WriteLine(item1);
                        }
                        break;

                    case SubMenu.update:
                        UpdateVolunteer();
                        break;

                    case SubMenu.delete:
                        Console.WriteLine("enter ID please");
                        Id = Console.Read();
                        s_dalVolunteer!.Delete(Id);
                        break;

                    case SubMenu.delete_all:
                        s_dalVolunteer!.DeleteAll();
                        break;

                    default:
                        break;
                }
            } while (flag);
        }
        private static void ShowCallSubMenu(string type)  // func to show the call sub menu
        {
            Console.WriteLine("enter 0 to exit the sub menu");
            Console.WriteLine($"enter 1 to add a new {type} to the list");
            Console.WriteLine($"enter 2 to show the {type} data");
            Console.WriteLine($"enter 3 to see all the list of {type}");
            Console.WriteLine($"enter 4 to update the {type}");
            Console.WriteLine($"enter 5 to delete the {type}");
            Console.WriteLine($"enter 6 to delete all the {type}");
            SubMenu key = (SubMenu)Console.Read();
            bool flag = true;
            int Id;
            do
            {
                switch (key)
                {
                    case SubMenu.exit:
                        flag = false;
                        break;

                    case SubMenu.add:
                        AddCall();
                        break;

                    case SubMenu.show:
                        Console.WriteLine("Enter Call Id");
                        Id = Console.Read();
                        Call item = s_dalCall.Read(Id);
                        Console.WriteLine(item);
                        break;

                    case SubMenu.see_all:
                        List<Call> tmpForShow = s_dalCall.ReadAll();
                        foreach (var item1 in tmpForShow)
                        {
                            Console.WriteLine(item1);
                        }
                        break;

                    case SubMenu.update:
                        UpdateCall();
                        break;

                    case SubMenu.delete:
                        Console.WriteLine("enter ID please");
                        Id = Console.Read();
                        s_dalCall!.Delete(Id);
                        break;

                    case SubMenu.delete_all:
                        s_dalCall!.DeleteAll();
                        break;

                    default:
                        break;
                }
            } while (flag);
        }
        private static void ShowAssignmentSubMenu(string type)  // func to show the assignment sub menu
        {
            Console.WriteLine("enter 0 to exit the sub menu");
            Console.WriteLine($"enter 1 to add a new {type} to the list");
            Console.WriteLine($"enter 2 to show the {type} data");
            Console.WriteLine($"enter 3 to see all the list of {type}");
            Console.WriteLine($"enter 4 to update the {type}");
            Console.WriteLine($"enter 5 to delete the {type}");
            Console.WriteLine($"enter 6 to delete all the {type}");
            SubMenu key = (SubMenu)Console.Read();
            bool flag = true;
            int Id;
            do
            {
                switch (key)
                {
                    case SubMenu.exit:
                        flag = false;
                        break;

                    case SubMenu.add:
                        AddAssignment();
                        break;

                    case SubMenu.show:
                        Console.WriteLine("Enter Assignment Id");
                        Id = Console.Read();
                        Assignment item = s_dalAssignment.Read(Id);
                        Console.WriteLine(item);
                        break;

                    case SubMenu.see_all:
                        List<Assignment> tmpForShow = s_dalAssignment.ReadAll();
                        foreach (var item1 in tmpForShow)
                        {
                            Console.WriteLine(item1);
                        }
                        break;

                    case SubMenu.update:
                        UpdateAssignment();
                        break;

                    case SubMenu.delete:
                        Console.WriteLine("enter ID please");
                        Id = Console.Read();
                        s_dalAssignment!.Delete(Id);
                        break;

                    case SubMenu.delete_all:
                        s_dalAssignment!.DeleteAll();
                        break;

                    default:
                        break;
                }
            } while (flag);
        }
        private static void ShowConfingSubMenu()  // func to show the config sub menu
        {
            Console.WriteLine("enter 0 to exit the sub menu");
            Console.WriteLine($"enter 1 to add a minute to the clock");
            Console.WriteLine($"enter 2 to add a hour to the clock");
            Console.WriteLine($"enter 3 to see the corrent clock time");
            Console.WriteLine($"enter 4 to update a value");
            Console.WriteLine($"enter 5 to see a specific value");
            Console.WriteLine($"enter 6 to reset all values");
            ConfingSubMenu key = (ConfingSubMenu)Console.Read();
            bool flag = true;
            do
            {
                switch (key)
                {
                    case ConfingSubMenu.exit:
                        flag = false;
                        break;

                    case ConfingSubMenu.minutes:
                        s_dalConfig.Clock.AddMinutes(1);
                        break;

                    case ConfingSubMenu.hours:
                        s_dalConfig.Clock.AddHours(1);
                        break;

                    case ConfingSubMenu.show_time:
                        Console.WriteLine(s_dalConfig.Clock);
                        break;

                    case ConfingSubMenu.set_new_value:
                        int choise;
                        Console.WriteLine("to change the time press 1, to change the Risk Rnge press 2");
                        choise = Console.Read();
                        if (choise == 1)
                        {
                            Console.WriteLine("Enter a new time");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime bdt)) throw new FormatException("time is invalid!");
                            DateTime tmpFinishTime = bdt;
                            s_dalConfig.Clock = bdt;
                        }
                        else if (choise == 2)
                        {
                            Console.WriteLine("Enter a new risk range (in secondes) ");
                            if (int.TryParse(Console.ReadLine(), out int riskrangeInsecondes))
                                s_dalConfig.RiskRnge = TimeSpan.FromSeconds(riskrangeInsecondes);
                        }
                        break;

                    case ConfingSubMenu.show_spesific_value:
                        int choise2;
                        Console.WriteLine("to see the time press 1, to see the Risk Rnge press 2");
                        choise2 = Console.Read();
                        if (choise2 == 1)
                            Console.WriteLine(s_dalConfig.Clock);
                        else if (choise2 == 2)
                            Console.WriteLine(s_dalConfig.RiskRnge);
                        break;

                    case ConfingSubMenu.reset:
                        s_dalConfig!.Reset();
                        break;

                    default:
                        break;
                }
            } while (flag);
        }
        private static void AddVolunteer() // func to add entity to rhe list
        {
            Console.WriteLine("enter ID please");
            int Id = Console.Read();
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
            double? Latitude = Console.Read()!;
            Console.WriteLine("enter Longitude please");
            double? Longitude = Console.Read()!;
            Volunteer addvolunteer = new Volunteer(Id, FullName, Phone, Email, Password, Address, Latitude, Longitude);
            s_dalVolunteer!.create(addvolunteer);
        }
        private static void UpdateVolunteer()  // func to update a existing entity
        {
            Console.WriteLine("enter ID please");
            int Id = Console.Read();
            Volunteer tmp = s_dalVolunteer.Read(Id); 
            Console.WriteLine(tmp); 
            // show the entity first
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
            double? Latitude = Console.Read()!;
            Console.WriteLine("enter Longitude please");
            double? Longitude = Console.Read()!;
            Volunteer Updatevolunteer = new Volunteer(Id, FullName, Phone, Email, Password, Address, Latitude, Longitude);
            s_dalVolunteer.Update(Updatevolunteer);
        }
        private static void AddCall()  // func to add entity to rhe list
        {
            Console.WriteLine("enter Type call, 0 to medical_situation, 1 to car_accident, 2 to fall_from_hight, 3 to violent_event, 4 to domestic_violent");
            TypeCall tmpTypeCall = (TypeCall)Console.Read();
            Console.WriteLine("enter Verbal Decription please");
            string? tmpVerabal = Console.ReadLine();
            Console.WriteLine("enter call adsress please");
            string? Address = Console.ReadLine()!;
            Console.WriteLine("enter Latitude please");
            double Latitude = Console.Read()!;
            Console.WriteLine("enter Longitude please");
            double Longitude = Console.Read()!;
            DateTime tmpOpeningCallTime = DateTime.Now;
            Console.WriteLine("enter end call time please");
            int time = Console.Read();
            DateTime tmpEndCallTime = DateTime.Now.AddMinutes(time);
            Call addcall = new Call(0, tmpTypeCall, tmpVerabal, Address, Latitude, Longitude, tmpOpeningCallTime, tmpEndCallTime);
            s_dalCall!.create(addcall);
        }
        private static void UpdateCall()  // func to update a existing entity
        {
            Console.WriteLine("enter ID please");
            int Id = Console.Read();
            Call tmp = s_dalCall.Read(Id);
            Console.WriteLine(tmp);
            // show the entity first
            Console.WriteLine("enter Type call, 0 to medical_situation, 1 to car_accident, 2 to fall_from_hight, 3 to violent_event, 4 to domestic_violent");
            TypeCall tmpTypeCall = (TypeCall)Console.Read();
            Console.WriteLine("enter Verbal Decription please");
            string? tmpVerabal = Console.ReadLine();
            Console.WriteLine("enter call adsress please");
            string? Address = Console.ReadLine()!;
            Console.WriteLine("enter Latitude please");
            double Latitude = Console.Read()!;
            Console.WriteLine("enter Longitude please");
            double Longitude = Console.Read()!;
            DateTime tmpOpeningCallTime = DateTime.Now;
            Console.WriteLine("enter end call time please");
            int time = Console.Read();
            DateTime tmpEndCallTime = DateTime.Now.AddMinutes(time);
            Call updateCall = new Call(0, tmpTypeCall, tmpVerabal, Address, Latitude, Longitude, tmpOpeningCallTime, tmpEndCallTime);
            s_dalCall.Update(updateCall);
        }
        private static void AddAssignment()  // func to add entity to rhe list
        {
            Console.WriteLine("enter call Id please");
            int tmpCallId = Console.Read()!;
            Console.WriteLine("enter volunteer Id please");
            int tmpVolunteerId = Console.Read()!;
            DateTime tmpStartTime = DateTime.Now;
            Console.WriteLine("enter time to finish the Assingment (in format dd/mm/yy hh:mm:ss): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime bdt)) throw new FormatException("time is invalid!");
                DateTime tmpFinishTime = bdt;
            Console.WriteLine("enter Assignment end kind, 0 to treated, 1 to self_cancellation, 2 to administrator_cancellation, 3 to expired_cancellation");
            EndKind tmpEndKind = (EndKind)Console.Read();
            Assignment addAssignment = new Assignment(0, tmpCallId, tmpVolunteerId, tmpStartTime, tmpFinishTime, tmpEndKind);
            s_dalAssignment!.create(addAssignment);
        }
        private static void UpdateAssignment()  // func to update a existing entity
        {
            Console.WriteLine("enter ID please");
            int Id = Console.Read();
            Assignment? tmp = s_dalAssignment.Read(Id);
            Console.WriteLine(tmp);
            // show the entity first
            Console.WriteLine("enter call Id please");
            int tmpCallId = Console.Read()!;
            Console.WriteLine("enter volunteer Id please");
            int tmpVolunteerId = Console.Read()!;
            DateTime tmpStartTime = DateTime.Now;
            Console.WriteLine("enter time to finish the Assingment (in format dd/mm/yy hh:mm:ss): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime bdt)) throw new FormatException("time is invalid!"); // chek the time format that got
            DateTime tmpFinishTime = bdt;
            Console.WriteLine("enter Assignment end kind, 0 to treated, 1 to self_cancellation, 2 to administrator_cancellation, 3 to expired_cancellation");
            EndKind tmpEndKind = (EndKind)Console.Read();
            Assignment UpdateAssignment = new Assignment(0, tmpCallId, tmpVolunteerId, tmpStartTime, tmpFinishTime, tmpEndKind);
            s_dalAssignment.Update(UpdateAssignment);
        }
        private static void ShowAllTheData() // print all the lists of all the entities
        {
            List<Volunteer> tmpForShow = s_dalVolunteer.ReadAll();
            foreach (var item in tmpForShow)
            {
                Console.WriteLine(item);
            }
            List<Call> tmpForShow1 = s_dalCall.ReadAll();
            foreach (var item1 in tmpForShow1)
            {
                Console.WriteLine(item1);
            }
            List<Assignment> tmpForShow2 = s_dalAssignment.ReadAll();
            foreach (var item2 in tmpForShow2)
            {
                Console.WriteLine(item2);
            }
        }
        static void Main(string[] args)
        {
            MainMenu key;
            bool logOut = true;
            try
            {
                do
                {
                    ShowMainMenu(); // print func
                    key = (MainMenu)Console.Read();
                    switch (key)
                    {
                        case MainMenu.exit: 
                            logOut = false;
                            break;

                        case MainMenu.sub_volunteer:   // sub menu
                            ShowVolunteerSubMenu("Volunteer"); 
                            break;

                        case MainMenu.sub_call:   // sub menu
                            ShowCallSubMenu("Call");
                            break;

                        case MainMenu.sub_assignment:   // sub menu
                            ShowAssignmentSubMenu("Assignment");
                            break;

                        case MainMenu.initialization:  // initiolztion all the data
                            Initialization.Do(s_dalAssignment, s_dalCall, s_dalVolunteer, s_dalConfig);
                            break;

                        case MainMenu.show_all:   // print all the lists
                            ShowAllTheData();
                            break;

                        case MainMenu.sub_confing:   // sub menu
                            ShowConfingSubMenu();
                            break;

                        case MainMenu.reset:   // reset all the list and the conping setup
                            s_dalAssignment!.DeleteAll();
                            s_dalCall!.DeleteAll();
                            s_dalVolunteer!.DeleteAll();
                            s_dalConfig!.Reset();
                            break;

                        default:
                            break;
                    }
                } while (logOut);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }
    }
}
