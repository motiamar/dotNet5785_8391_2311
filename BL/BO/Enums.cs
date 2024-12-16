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

public enum VollInListFilter
{
    Id,
    FullName,
    Active,
    TreatedCalls,
    CanceledCalls,
    ExpiredCalls,
    CorrentCallId,
    CorrentCallType
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

/// <summary>
/// enum to choose a filed in the main menu
/// </summary>
public enum BMainMenu
{
    Exit,
    Sub_volunteer,
    Sub_call,
    Sub_admin,
}

/// <summary>
/// enum to choose a filed in the sub menu of the volunteer
/// </summary>
public enum BSubVolMenu
{
    Exit,
    Login,
    Show_volunteers,
    Show_volunteer,
    Update_volunteer,
    Delete_volunteer,
    Add_volunteer
}

/// <summary>
/// enum to choose a filed in the sub menu of the call
/// </summary>
public enum BSubCallMenu
{
    Exit,
    Array_calls,
    Show_calls,
    Show_call,
    Update_call,
    Delete_call,
    Add_call,
    Show_close_calls,
    Show_open_calls,
    End_call,
    Cancel_call,
    Choose_call
}

/// <summary>
/// enum to choose a filed in the sub menu of the admin
/// </summary>
public enum BSubAdmMenu
{
    Exit,
    See_clock,
    Change_clock,
    See_riskRange,
    Change_RiskRange,
    Reset,
    Initialize
}