namespace TotalCoin.Client.Domain;

public sealed record Order(int Id, string QrCode, bool Enabled, DateTime? ExpirationDate);