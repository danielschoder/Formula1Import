namespace Formula1Import.Domain.Entities;

public class Constructor
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<Result> Results { get; set; }

    public static Constructor Create() => new() { Id = Guid.NewGuid() };
}
