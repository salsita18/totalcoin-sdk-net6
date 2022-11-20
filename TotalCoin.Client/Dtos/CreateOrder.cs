namespace TotalCoin.Client.Dtos;

internal class CreateOrder
{
    public string Cashier { get; }
    public string Concept { get; }
    public DateTime? ExpirationDate { get; }
    public int? BusinessUnit { get; }
    public string ExternalReference { get; }
    public string QrAmountType { get; }
    public decimal? Amount { get; }
    public string QrType { get; }
    public bool? ResetOnPayment { get; }

    public CreateOrder(string concept,
        DateTime expirationDate,
        string externalReference,
        string qrAmountType,
        decimal? amount,
        string qrType,
        bool? resetOnPayment,
        int? businessUnit)
    {
        Cashier = "API";
        Concept = concept;
        ExpirationDate = expirationDate;
        ExternalReference = externalReference;
        QrAmountType = qrAmountType;
        Amount = amount;
        QrType = qrType;
        ResetOnPayment = resetOnPayment;
        BusinessUnit = businessUnit;
    }
}