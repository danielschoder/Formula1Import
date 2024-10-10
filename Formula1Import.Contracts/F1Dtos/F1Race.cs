namespace Formula1Import.Contracts.F1Dtos;

public record F1Race(
    int Year,
    int Round,
    string GrandPrixName,
    string CircuitName);
