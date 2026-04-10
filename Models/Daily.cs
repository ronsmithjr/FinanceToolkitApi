namespace FinanceToolkitApi.Models
{
    public class Daily : Base
    {
        public int DaysAccrued { get; set; }
        public _YearBasis Yearbasis { get; set; }
        public decimal DailyInterest { get; set; }

    }
}
