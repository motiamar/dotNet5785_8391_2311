namespace DO;

public enum Role // define the character of the volunteer
{
    volunteer,  // default
    manager,
}
// 

public enum DistanceType // define in what means the distance get measured
{
    air, // default
    walk,
    car,
}
public enum TypeCall // define the type of call that enterd the system
{
    medical_situation, // default
    car_accident,
    fall_from_hight,
    violent_event,
    domestic_violent,
}
public enum EndKind // tells how the assiment ended
{
    treated, // default
    self_cancellation,
    administrator_cancellation,
    expired_cancellation,
}
public enum MainMenu //  choises for the menu
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
public enum SubMenu  //  choises for the sub menu
{ 
    exit,
    add,
    show,
    see_all,
    update,
    delete,
    delete_all
}
public enum ConfingSubMenu  //  choises for the confing sub menu
{
    exit,
    minutes,
    hours,
    show_time,
    set_new_value,
    show_spesific_value,
    reset
}


