using System.Linq.Expressions;
using DalApi;
using DalTest;
using DO;
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
                switch ((MainMenu)key)
                {
                    case MainMenu.Exit:
                        logOut = false;
                        break;

                    case MainMenu.Sub_volunteer:   // sub menu
                        break;

                    case MainMenu.Sub_call:   // sub menu
                        break;

                    case MainMenu.Sub_assignment:   // sub menu
                        break;

                    case MainMenu.Sub_admin:   // sub menu
                        break;

                    case MainMenu.Initialization:  // initiolztion all the data
                        break;

                    case MainMenu.Show_all:   // print all the lists
                        break;

                    case MainMenu.Sub_confing:   // sub menu
                        break;

                    case MainMenu.Reset:   // Reset all the list and the conping setup
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
    public static void ShowMainMenu()
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
    public static void ShowVolunteerMenu()
    {
        Console.WriteLine("enter 0 to Exit the sub menu");
        Console.WriteLine($"enter 1 to Add a new Volunteer to the list");
        Console.WriteLine($"enter 2 to Show the Volunteer data");
        Console.WriteLine($"enter 3 to see all the list of Volunteer");
        Console.WriteLine($"enter 4 to Update the Volunteer");
        Console.WriteLine($"enter 5 to Delete the Volunteer");
        Console.WriteLine($"enter 6 to Delete all the Volunteer");
    }
    public static void ShowCallMenu()
    {
        Console.WriteLine("enter 0 to Exit the sub menu");
            Console.WriteLine($"enter 1 to Add a new Call to the list");
            Console.WriteLine($"enter 2 to Show the Call data");
            Console.WriteLine($"enter 3 to see all the list of Call");
            Console.WriteLine($"enter 4 to Update the Call");
            Console.WriteLine($"enter 5 to Delete the Call");
            Console.WriteLine($"enter 6 to Delete all the Call");
    }
    public static void ShowAssignmentMenu()
    {
        Console.WriteLine("enter 0 to Exit the sub menu");
        Console.WriteLine($"enter 1 to Add a new Assignment to the list");
        Console.WriteLine($"enter 2 to Show the Assignment data");
        Console.WriteLine($"enter 3 to see all the list of Assignment");
        Console.WriteLine($"enter 4 to Update the Assignment");
        Console.WriteLine($"enter 5 to Delete the Assignment");
        Console.WriteLine($"enter 6 to Delete all the Assignment");
    }

    public static void ShowConfigMenu()
    {
        Console.WriteLine("enter 0 to Exit the sub menu");
        Console.WriteLine($"enter 1 to Add a minute to the clock");
        Console.WriteLine($"enter 2 to Add a hour to the clock");
        Console.WriteLine($"enter 3 to see the corrent clock time");
        Console.WriteLine($"enter 4 to Update a value");
        Console.WriteLine($"enter 5 to see a specific value");
        Console.WriteLine($"enter 6 to Reset all values");
        bool flag = true;
        do
        {
            string input = Console.ReadLine()!;
            int key = int.Parse(input);
            switch ((ConfigMenu)key)
            {
                case ConfigMenu.Exit:
                    flag = false;
                    break;
                case ConfigMenu.Add_minute:
                    break;
                case ConfigMenu.Add_hour:
                    break;
                case ConfigMenu.Show_time:
                    break;
                case ConfigMenu.Update:
                    break;
                case ConfigMenu.Show_value:
                    break;
                case ConfigMenu.Reset:
                    break;
                default:
                    break;
            }
        } while (flag);
    }
    public enum ConfigMenu
    {
        Exit,
        Add_minute,
        Add_hour,
        Show_time,
        Update,
        Show_value,
        Reset
    }
    public enum MainMenu
    {
        Exit,
        Sub_volunteer,
        Sub_call,
        Sub_assignment,
        Sub_admin,
        Initialization,
        Show_all,
        Sub_confing,
        Reset
    }
}


