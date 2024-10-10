namespace Formula1Import.Contracts.F1Dtos;

public record F1RaceResults(
    int Year,
    int Round,
    string CircuitName,
    F1Result[] Results);
