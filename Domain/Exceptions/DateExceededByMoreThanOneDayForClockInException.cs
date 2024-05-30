namespace Domain.Exceptions;

[Serializable]
public class DateExceededByMoreThanOneDayForClockInException : Exception {
    public DateExceededByMoreThanOneDayForClockInException(int numberOfDaysExceeded)
        : this(numberOfDaysExceeded, null) { }
    public DateExceededByMoreThanOneDayForClockInException(int numberOfDaysExceeded, string? message)
        : this(numberOfDaysExceeded, message, null) { }
    public DateExceededByMoreThanOneDayForClockInException(int numberOfDaysExceeded, string? message, Exception? inner) 
        : base(message, inner) => this.NumberOfDaysExceeded = numberOfDaysExceeded;

    public int NumberOfDaysExceeded { get; }
}
