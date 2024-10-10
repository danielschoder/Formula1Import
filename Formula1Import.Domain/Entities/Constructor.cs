namespace Formula1Import.Domain.Entities;

public class Constructor
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Result> Results { get; set; }

    public static Constructor Create(string name)
        => new() { Name = name };
}
