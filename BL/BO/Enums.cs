namespace BO;


// define the character of the Volunteer
public enum BRoles
{
    Volunteer,  // default
    Manager
}

// define in what means the distance get measured
public enum BDistanceTypes
{
    Air, // default
    Walk,
    Car
}

// define the type of call that enterd the system
public enum BTypeCalls
{
    Medical_situation, // default
    Car_accident,
    Fall_from_hight,
    Violent_event,
    Domestic_violent,
    None
}

public enum BCallStatus
{
    Open,// default
    In_treatment,
    Closed,
    Expired,
    Open_in_risk,
    In_treatment_in_risk
}

// tells how the assiment ended
public enum BEndKinds
{
    Treated, // default
    Self_cancellation,
    Administrator_cancellation,
    Expired_cancellation
}

// enum to choose a filed to sort/filter by
public enum CallInListFilter
{
    Id,
    CallId,
    Type,
    CallOpenTime,
    CallMaxCloseTime,
    LastVolunteerName,
    TotalTreatmentTime,
    CallStatus,
    SumOfAssignments
}

// enum to choose a filed to sort/filter by
public enum CloseCallInListFilter
{
    Id,
    Type,
    CallAddress,
    CallOpenTime,
    CallEnterTime,
    CallCloseTime,
    EndKind
}

// enum to choose a filed to sort/filter by
public enum OpenCallInListFilter
{
    Id,
    Type,
    Description,
    CallAddress,
    CallOpenTime,
    CallMaxCloseTime,
    CallDistance
}

public enum TimeUnit
{
    Minute,
    Hour,
    Day,
    Month,
    Year
}