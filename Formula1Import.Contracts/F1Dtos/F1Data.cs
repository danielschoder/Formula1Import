namespace Formula1Import.Contracts.F1Dtos;

public record F1Data(
    F1Constructor[] Constructors,
    F1Driver[] Drivers,
    F1Race[] Races,
    F1RaceResults RaceResults,
    F1Season[] Seasons);
