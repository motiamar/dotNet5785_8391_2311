namespace DO;
[Serializable]

/// exception for not exist items
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string message) : base(message) { }
}

/// <summary>
/// exception for already exist items
/// </summary>
public class DalAlreadyExistException : Exception
{
    public DalAlreadyExistException(string message) : base(message) { }
}

/// <summary>
/// exception for not able to Delete items
/// </summary>
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string message) : base(message) { }
}

/// <summary>
/// exception for the XML functions
/// </summary>
public class  DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string message) : base(message) { }
}

