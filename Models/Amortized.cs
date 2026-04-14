namespace FinanceToolkitApi.Models
{
    /// <summary>
    /// We are going to need to have a dictionary inside of a list to properly display the schedule.
    /// </summary>
    public class Amortized
    {
        public decimal Principal { get; set; }
        public decimal AnnualRate { get; set; }
        public int Years { get; set; }
        public int PeriodsPerYear { get; set; } = 12;

    }
}
