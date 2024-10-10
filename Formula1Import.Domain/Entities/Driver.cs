namespace Formula1Import.Domain.Entities;

public class Driver
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Result> Results { get; set; }

    public static Driver Create(string name)
        => new() { Name = name };
}
