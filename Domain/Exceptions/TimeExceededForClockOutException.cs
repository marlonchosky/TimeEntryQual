namespace Domain.Exceptions;

[Serializable]
public class TimeExceededForClockOutException : Exception {
    public TimeExceededForClockOutException(TimeSpan timeSpan) => 
        this.TimeSpan = timeSpan;

    public TimeExceededForClockOutException(string message, TimeSpan timeSpan) : base(message) =>
        this.TimeSpan = timeSpan;

    public TimeExceededForClockOutException(string message, Exception inner, TimeSpan timeSpan) : base(message, inner) =>
        this.TimeSpan = timeSpan;

    public TimeSpan TimeSpan { get; }
}

