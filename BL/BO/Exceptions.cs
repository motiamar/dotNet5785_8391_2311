namespace BO;

internal class Exceptions
{

    public class BlDoesNotExistException : Exception
    {
        public BlDoesNotExistException(string message) : base(message) { }
    }
    public class BlCantLoadException : Exception
    {
        public BlCantLoadException(string message) : base(message) { }
    }
    public class BLVolunteerIsAssign : Exception
    {
        public BLVolunteerIsAssign(string message) : base(message) { }
    }
    public class BlNotAllowException : Exception
    {
        public BlNotAllowException(string message) : base(message) { }
    }
    public class BlinCorrectException : Exception
    {
        public BlinCorrectException(string message) : base(message) { }
    }
    public class BlVolAllreadyExist : Exception
    {
        public BlVolAllreadyExist(string message) : base(message) { }
    }

}
