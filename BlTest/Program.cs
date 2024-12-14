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
}


