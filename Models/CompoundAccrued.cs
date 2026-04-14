namespace FinanceToolkitApi.Models
{
    public class CompoundAccrued
    {
        public decimal Principal { get; set; }
        public decimal AnnualRate { get; set; }
        public int Years { get; set; }
        public int CompoundsPerYear {  get; set; }
        public int DaysAccrued { get; set; }

        public int DayCountBasis { get; set; }
    }
}
