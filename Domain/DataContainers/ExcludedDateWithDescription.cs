namespace Domain.DataContainers;

public class ExcludedDateWithDescription {
    public required DateOnly Date { get; set; }
    public string? Description { get; set; }
}
