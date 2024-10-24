namespace Formula1Import.Domain.Entities;

public class Circuit
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string ErgastCircuitId { get; set; }

    public ICollection<Race> Races { get; set; }

    public static Circuit Create() => new() { Id = Guid.NewGuid() };
}
