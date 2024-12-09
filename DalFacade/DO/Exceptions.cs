namespace DO;
[Serializable]

// exception for not exist items
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string message) : base(message) { }
}


// exception for already exist items
public class DalAlreadyExistException : Exception
{
    public DalAlreadyExistException(string message) : base(message) { }
}


// exception for not able to Delete items
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string message) : base(message) { }
}


// exception for the XML functions
public class  DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string message) : base(message) { }
}

