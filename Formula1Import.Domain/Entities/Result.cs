namespace Formula1Import.Domain.Entities;

public class Result
{
    public int Id { get; set; }

    public int Position { get; set; }

    public string Ranking { get; set; }

    public int Points { get; set; }

    public TimeSpan Time { get; set; }

    public int SessionId { get; set; }
    public Session Session { get; set; }

    public int DriverId { get; set; }
    public Driver Driver { get; set; }

    public int ConstructorId { get; set; }
    public Constructor Constructor { get; set; }
}
