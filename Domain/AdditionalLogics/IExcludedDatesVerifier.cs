namespace Domain.AdditionalLogics;

public interface IExcludedDatesVerifier {
    Task<bool> isTodayAnExcludedDate();
}
