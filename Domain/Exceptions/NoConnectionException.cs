using System.Runtime.Serialization;

namespace Domain.Exceptions;

[Serializable]
public class NoConnectionException : Exception {
    public NoConnectionException() {
    }

    public NoConnectionException(string? message) : base(message) {
    }

    public NoConnectionException(string? message, Exception? innerException) : base(message, innerException) {
    }

    protected NoConnectionException(SerializationInfo info, StreamingContext context) {
    }
}