namespace FinanceToolkitApi.Models
{
    public class Compound
    {
        public decimal Principal { get; set; }
        public decimal AnnualRate { get; set; }
        public int Years { get; set; }
        public int CompoundsPerYear { get; set; }
    }
}
