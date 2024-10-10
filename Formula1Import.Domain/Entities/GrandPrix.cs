namespace Formula1Import.Domain.Entities;

public class GrandPrix
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Race> Races { get; set; }

    public static GrandPrix Create(string name)
        => new() { Name = name };
}
