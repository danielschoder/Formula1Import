namespace Formula1Import.Domain.Entities;

public class Race
{
    public int Id { get; set; }

    public int SeasonYear { get; set; }
    public Season Season { get; set; }

    public int Round { get; set; }

    public int? CircuitId { get; set; }
    public Circuit Circuit { get; set; }

    public int GrandPrixId { get; set; }
    public GrandPrix GrandPrix { get; set; }

    public ICollection<Session> Sessions { get; set; }
}
