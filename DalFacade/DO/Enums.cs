namespace DO;

// define the character of the volunteer
public enum Roles 
{
    volunteer,  // default
    manager,
}


// define in what means the distance get measured
public enum DistanceTypes 
{
    air, // default
    walk,
    car,
}

// define the type of call that enterd the system
public enum TypeCalls 
{
    medical_situation, // default
    car_accident,
    fall_from_hight,
    violent_event,
    domestic_violent,
} 

// tells how the assiment ended
public enum EndKinds
{
    treated, // default
    self_cancellation,
    administrator_cancellation,
    expired_cancellation,
}

//  choises for the menu
public enum MainMenu 
{
    exit,
    sub_volunteer,
    sub_call,
    sub_assignment,
    initialization,
    show_all,
    sub_confing,
    reset

}


//  choises for the sub menu
public enum SubMenu  
{ 
    exit,
    add,
    show,
    see_all,
    update,
    delete,
    delete_all
}

 //  choises for the confing sub menu
public enum ConfingSubMenu 
{
    exit,
    minutes,
    hours,
    show_time,
    set_new_value,
    show_spesific_value,
    reset
}


