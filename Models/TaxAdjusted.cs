namespace FinanceToolkitApi.Models
{
    public sealed class TaxAdjusted
    {
        public decimal Income { get; set; }
        public decimal NominalRate { get; set; }
        public decimal TaxRate { get; set; }
        public decimal InflationRate { get; set; }
        public decimal RealRate { get; set; }
    }
}
