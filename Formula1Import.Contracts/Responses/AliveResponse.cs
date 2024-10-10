namespace Formula1Import.Contracts.Responses;

public record AliveResponse(
    string Service,
    DateTime UtcNow,
    string Version);
