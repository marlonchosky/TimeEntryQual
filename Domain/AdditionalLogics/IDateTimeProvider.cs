namespace Domain.AdditionalLogics;

public interface IDateTimeProvider {
    public TimeOnly CurrentTime { get; }
    public DateOnly CurrentDate { get; }
    public DateTime CurrentDateTime { get; }
}
