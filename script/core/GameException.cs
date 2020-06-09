using System;
namespace Core
{
    public class GameException : System.Exception
    {
        public GameException() { }
        public GameException(string message) : base(message) { }
        public GameException(string message, System.Exception inner) : base(message, inner) { }
        protected GameException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
