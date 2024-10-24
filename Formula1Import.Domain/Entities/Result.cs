namespace Formula1Import.Domain.Entities;

public class Result
{
    public Guid Id { get; set; }

    public int Position { get; set; }

    public int Points { get; set; }

    public TimeSpan Time { get; set; }

    public Guid SessionId { get; set; }
    public Session Session { get; set; }

    public Guid DriverId { get; set; }
    public Driver Driver { get; set; }

    public Guid ConstructorId { get; set; }
    public Constructor Constructor { get; set; }

    public static Result Create(Guid sessionId)
        => new()
        {
            Id = Guid.NewGuid(),
            SessionId = sessionId
        };
}
