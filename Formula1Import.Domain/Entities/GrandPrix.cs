namespace Formula1Import.Domain.Entities;

public class GrandPrix
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<Race> Races { get; set; }

    public static GrandPrix Create() => new() { Id = Guid.NewGuid() };
}
