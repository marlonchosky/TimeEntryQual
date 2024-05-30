namespace Domain.Exceptions;

[Serializable]
public class TimeExceededForClockInException : Exception {
    public TimeExceededForClockInException(TimeSpan timeSpan) =>
        this.TimeSpan = timeSpan;

    public TimeExceededForClockInException(TimeSpan timeSpan, string message) : base(message) =>
        this.TimeSpan = timeSpan;

    public TimeExceededForClockInException(TimeSpan timeSpan, string message, Exception inner) : base(message, inner) => 
        this.TimeSpan = timeSpan;

    public TimeSpan TimeSpan { get; }
}

