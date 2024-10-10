namespace Formula1Import.Domain.Entities;

public class Circuit
{
    public int Id { get; set; }

    public string Name { get; set; }

    public static Circuit Create(string name)
        => new() { Name = name };
}
