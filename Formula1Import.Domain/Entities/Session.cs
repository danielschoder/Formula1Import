namespace Formula1Import.Domain.Entities;

public class Session
{
    public Guid Id { get; set; }

    public DateTime StartDateTimeUtc { get; set; }

    public int SessionTypeId { get; set; }
    public SessionType SessionType { get; set; }

    public Guid RaceId { get; set; }
    public Race Race { get; set; }

    public ICollection<Result> Results { get; set; }

    public static Session Create(int sessionType, Guid raceId)
        => new()
        {
            Id = Guid.NewGuid(),
            SessionTypeId = sessionType,
            RaceId = raceId,
            Results = []
        };
}
