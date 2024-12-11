namespace BO;


/// <summary>
/// define the character of the Volunteer
/// </summary>
public enum BRoles
{
    Volunteer,  // default
    Manager
}

/// <summary>
/// define in what means the distance get measured
/// </summary>
public enum BDistanceTypes
{
    Air, // default
    Walk,
    Car
}

/// <summary>
/// define the type of call that enterd the system
/// </summary>
public enum BTypeCalls
{
    Medical_situation, // default
    Car_accident,
    Fall_from_hight,
    Violent_event,
    Domestic_violent,
    None
}

/// <summary>
///  define the type of call status in the system
/// </summary>
public enum BCallStatus
{
    Open,// default
    In_treatment,
    Closed,
    Expired,
    Open_in_risk,
    In_treatment_in_risk
}

/// <summary>
/// tells how the assiment ended
/// </summary>
public enum BEndKinds
{
    Treated, // default
    Self_cancellation,
    Administrator_cancellation,
    Expired_cancellation
}

/// <summary>
/// enum to choose a filed to sort/filter by
/// </summary>
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

/// <summary>
/// enum to choose a filed to sort/filter by
/// </summary>
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

/// <summary>
/// enum to choose a filed to sort/filter by
/// </summary>
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

/// <summary>
/// enum to choose a unit time to cgange by
/// </summary>
public enum TimeUnit
{
    Minute,
    Hour,
    Day,
    Month,
    Year
}