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
    alergy,
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

