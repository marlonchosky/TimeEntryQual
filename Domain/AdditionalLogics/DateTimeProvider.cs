using Domain.Repositories;

namespace Domain.AdditionalLogics;

public class DateTimeProvider : IDateTimeProvider {
    public TimeOnly CurrentTime => TimeOnly.FromDateTime(DateTime.Now);
    public DateOnly CurrentDate => DateOnly.FromDateTime(DateTime.Now);
    public DateTime CurrentDateTime => DateTime.Now;
}
