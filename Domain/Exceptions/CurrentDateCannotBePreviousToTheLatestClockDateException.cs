namespace Domain.Exceptions;

[Serializable]
public class CurrentDateCannotBePreviousToTheLatestClockDateException : Exception {
    public CurrentDateCannotBePreviousToTheLatestClockDateException(int numberOfDays) =>
        this.NumberOfDays = numberOfDays;
    public CurrentDateCannotBePreviousToTheLatestClockDateException(int numberOfDays, string message) : base(message) =>
        this.NumberOfDays = numberOfDays;
    public CurrentDateCannotBePreviousToTheLatestClockDateException(int numberOfDays, string message, Exception inner) 
        : base(message, inner) => 
        this.NumberOfDays = numberOfDays;

    public int NumberOfDays { get; }
}