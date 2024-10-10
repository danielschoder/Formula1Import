namespace Formula1Import.Contracts.F1Dtos;

public record F1Result(
    string DriverName,
    string ConstructorName,
    int Points)
{
    public string Ranking { get; set; }
    public int Position { get; set; }
}
