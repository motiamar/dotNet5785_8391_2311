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


    internal static int NextCourseId
    {
        get => XMLTools.GetAndIncreaseConfigIntVal(s_data_config_xml, "NextCourseId");
        private set => XMLTools.SetConfigIntVal(s_data_config_xml, "NextCourseId", value);
    }

    //...	

    internal static DateTime Clock
    {
        get => XMLTools.GetConfigDateVal(s_data_config_xml, "Clock");
        set => XMLTools.SetConfigDateVal(s_data_config_xml, "Clock", value);
    }

    internal static void Reset()
    {
        NextCourseId = 1000;
        //...
        Clock = DateTime.Now;
        //...
    }
}
