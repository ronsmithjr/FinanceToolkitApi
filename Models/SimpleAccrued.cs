namespace FinanceToolkitApi.Models
{
    public class SimpleAccrued
    {
        public decimal Principal { get; set; }
        public decimal AnnualRate { get; set; }
        public decimal DaysAccrued { get; set; }
        public int DayCountBasis { get; set; } 
    }
}
