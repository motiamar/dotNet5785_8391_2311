namespace BO;


// define the character of the volunteer
public enum BRoles
{
    volunteer,  // default
    manager,
}


// define in what means the distance get measured
public enum BDistanceTypes
{
    air, // default
    walk,
    car,
}

// define the type of call that enterd the system
public enum BTypeCalls
{
    medical_situation, // default
    car_accident,
    fall_from_hight,
    violent_event,
    domestic_violent,
    None,
}

public enum BCallStatus
{
    open,// default
    in_treatment,
    closed,
    expired,
    open_in_risk,
    in_treatment_in_risk,
}

// tells how the assiment ended
public enum BEndKinds
{
    treated, // default
    self_cancellation,
    administrator_cancellation,
    expired_cancellation,
}
