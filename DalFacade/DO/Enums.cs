namespace DO;

// define the character of the Volunteer
public enum Roles 
{
    Volunteer,  // default
    Manager,
}


// define in what means the distance get measured
public enum DistanceTypes 
{
    Air, // default
    Walk,
    Car,
}

// define the type of call that enterd the system
public enum TypeCalls 
{
    Medical_situation, // default
    Car_accident,
    Fall_from_hight,
    Violent_event,
    Domestic_violent,
} 

// tells how the assiment ended
public enum EndKinds
{
    Treated, // default
    Self_cancellation,
    Administrator_cancellation,
    Expired_cancellation,
}

//  choises for the menu
public enum MainMenu 
{
    Exit,
    Sub_volunteer,
    Sub_call,
    Sub_assignment,
    Initialization,
    Show_all,
    Sub_confing,
    Reset

}


//  choises for the sub menu
public enum SubMenu  
{ 
    Exit,
    Add,
    Show,
    See_all,
    Update,
    Delete,
    Delete_all
}

 //  choises for the confing sub menu
public enum ConfingSubMenu 
{
    Exit,
    Minutes,
    Hours,
    Show_time,
    Set_new_value,
    Show_spesific_value,
    Reset
}


