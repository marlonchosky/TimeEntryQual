namespace Domain.ConfigurationClasses;

public class ExcludedDate {
    public required DateOnly Date { get; set; }
    public string? Description { get; set; }
}
