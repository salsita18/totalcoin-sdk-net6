namespace TotalCoin.Client.Dtos;

internal record QrOrderDto(int Id,
    string QrCode,
    DateTime? ExpirationDate,
    decimal? Amount,
    string Concept,
    string Cashier,
    string? ExternalReference,
    bool IsEnabled);