namespace Formula1Import.Domain.Entities;

public class Session
{
    public int Id { get; set; }

    public DateTime StartDateTimeUtc { get; set; }

    public int SessionTypeId { get; set; }
    public SessionType SessionType { get; set; }

    public int RaceId { get; set; }
    public Race Race { get; set; }

    public ICollection<Result> Results { get; set; }
}
