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
        get => XMLTools.GetAndIncreaseConfigIntVal(s_data_config_xml, "NextCallId");
        private set => XMLTools.SetConfigIntVal(s_data_config_xml, "NextCallId", value);
    }

    // set value into the file
    // get element from the file, and encrise by one
    internal static int NextAssignmentId
    {
        get => XMLTools.GetAndIncreaseConfigIntVal(s_data_config_xml, "NextAssignmentId");
        private set => XMLTools.SetConfigIntVal(s_data_config_xml, "NextAssignmentId", value);
    }

    // set and get the clock from the file
    internal static DateTime Clock
    {
        get => XMLTools.GetConfigDateVal(s_data_config_xml, "Clock");
        set => XMLTools.SetConfigDateVal(s_data_config_xml, "Clock", value);
    }

    // set and get the time spand from the file
    internal static TimeSpan RiskRnge
    {
        get => XMLTools.GetConfigTimeVal(s_data_config_xml, "RiskRnge");
        set => XMLTools.SetConfigTimeVal(s_data_config_xml, "RiskRnge", value);
    }

    // reset all the value in the file
    internal static void Reset()
    {
        NextCallId = 1000;
        NextAssignmentId = 1000;
        Clock = DateTime.Now;
        RiskRnge = TimeSpan.Zero;
    }
}
