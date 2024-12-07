namespace BO;

internal class Exceptions
{
    public class VolunteerException : Exception
    {
        public VolunteerException(string message) : base(message) { }
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
