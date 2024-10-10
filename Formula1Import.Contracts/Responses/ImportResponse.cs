namespace Formula1Import.Contracts.Responses;

public class ImportResponse
{
    public string Type { get; set; }

    public int FromYear { get; set; }

    public int ToYear { get; set; }

    public int RowsInDatabase { get; set; }

    public int UniqueRowsInImport { get; set; }

    public int RowsInserted { get; set; }

    public int RowsUpdated { get; set; }

    public ImportResponse(string type, int fromYear, int toYear)
        => (Type, FromYear, ToYear) = (type, fromYear, toYear);
}
