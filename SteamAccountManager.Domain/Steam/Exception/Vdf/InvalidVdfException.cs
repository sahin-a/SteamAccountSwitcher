namespace SteamAccountManager.Domain.Steam.Exception.Vdf
{
    public class InvalidVdfException : System.Exception
    {
        public InvalidVdfException() { }
        public InvalidVdfException(string message) : base(message) { }
        public InvalidVdfException(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidVdfException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
