namespace BO;

internal class Exceptions
{
    /// <summary>
    /// exeption for not exist entity
    /// </summary>
    public class BlDoesNotExistException : Exception
    {
        public BlDoesNotExistException(string message) : base(message) { }
    }

    /// <summary>
    /// exeption for cant load exist entity
    /// </summary>
    public class BlCantLoadException : Exception
    {
        public BlCantLoadException(string message) : base(message) { }
    }

    /// <summary>
    /// exeption for unpossible deletitions
    /// </summary>
    public class BLVolunteerIsAssign : Exception
    {
        public BLVolunteerIsAssign(string message) : base(message) { }
    }

    /// <summary>
    /// exeption for changes that are not allowd by the user
    /// </summary>
    public class BlNotAllowException : Exception
    {
        public BlNotAllowException(string message) : base(message) { }
    }

    /// <summary>
    /// exeption for datailes that are uncorrect by the user
    /// </summary>
    public class BlinCorrectException : Exception
    {
        public BlinCorrectException(string message) : base(message) { }
    }

    /// <summary>
    /// exeption for already exist entity
    /// </summary>
    public class BlVolAllreadyExist : Exception
    {
        public BlVolAllreadyExist(string message) : base(message) { }
    }

}
