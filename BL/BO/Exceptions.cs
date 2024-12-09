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
    public class StationException : Exception
    {
        public StationException(string message) : base(message) { }
    }
    public class LineException : Exception
    {
        public LineException(string message) : base(message) { }
    }

}
