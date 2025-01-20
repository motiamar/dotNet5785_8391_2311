using System.Runtime.CompilerServices;

namespace Dal;
internal static class Config
{
    ///<summary>
    /// filds that Saves the entity of the lists
    ///</summary>
    ///<param name="s_data_config_xml">Saves the entity list of config</param>
    ///<param name="s_calls_xml">Saves the entity list of calls</param>
    ///<param name="s_assignments_xml">Saves the entity list of assignments</param>
    ///<param name="s_volunteers_xml">Saves the entity list of volunteers</param>
    
    internal const string s_data_config_xml = "data-config.xml";
    internal const string s_calls_xml = "calls.xml";
    internal const string s_assignments_xml = "assignments.xml";
    internal const string s_volunteers_xml = "volunteers.xml";

    // set value into the file
    // get element from the file, and encrise by one
    internal static int NextCallId
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get => XMLTools.GetAndIncreaseConfigIntVal(s_data_config_xml, "NextCallId");
        [MethodImpl(MethodImplOptions.Synchronized)]
        private set => XMLTools.SetConfigIntVal(s_data_config_xml, "NextCallId", value);
    }

    // set value into the file
    // get element from the file, and encrise by one
    internal static int NextAssignmentId
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get => XMLTools.GetAndIncreaseConfigIntVal(s_data_config_xml, "NextAssignmentId");
        [MethodImpl(MethodImplOptions.Synchronized)]
        private set => XMLTools.SetConfigIntVal(s_data_config_xml, "NextAssignmentId", value);
    }

    // set and get the clock from the file
    internal static DateTime Clock
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get => XMLTools.GetConfigDateVal(s_data_config_xml, "Clock");
        [MethodImpl(MethodImplOptions.Synchronized)]
        set => XMLTools.SetConfigDateVal(s_data_config_xml, "Clock", value);
    }

    // set and get the time spand from the file
    internal static TimeSpan RiskRnge
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get => XMLTools.GetConfigTimeVal(s_data_config_xml, "RiskRnge");
        [MethodImpl(MethodImplOptions.Synchronized)]
        set => XMLTools.SetConfigTimeVal(s_data_config_xml, "RiskRnge", value);
    }

    // Reset all the value in the file
    [MethodImpl(MethodImplOptions.Synchronized)]
    internal static void Reset()
    {
        NextCallId = 1000;
        NextAssignmentId = 1000;
        Clock = DateTime.Now;
        RiskRnge = TimeSpan.Zero;
    }
}
