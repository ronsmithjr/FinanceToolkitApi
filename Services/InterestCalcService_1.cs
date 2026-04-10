using FinanceToolkitApi.Models;

namespace FinanceToolkitApi.Services
{
    public partial class InterestCalcService : IInterestCalcService
    {
        public decimal CalcualateAcrruedInterest(Accrued accruedRequest)
        {
            decimal rate = accruedRequest.AnnualRate / 100;
            decimal accruedInterest = rate / accruedRequest.YearBasis * accruedRequest.DaysAccrued * accruedRequest.Principal;
            return Math.Round(accruedInterest, 2);
        }

        public decimal CalcualateAmortizedInterest(Amortized amortizedRequest){ return 0.0M; }
        public decimal CalcualateCompoundInterest(Compound compoundRequest){ return 0.0M; }
        public decimal CalcualateContinuousInterest(Continuous continuousRequest){ return 0.0M; }
        public decimal CalcualateDailyInterest(Daily dailyRequest){ return 0.0M; }
        public decimal CalcualateEffectiveRate(EffectiveRate effectiveRateRequest){ return 0.0M; }
        public decimal CalcualateInflationAdjusted(InflationAdjusted inflAdjRequest)
        {
            decimal realRate = (1 + inflAdjRequest.Nominal) / (1 + inflAdjRequest.Inflation) - 1 ;
            return Math.Round(realRate * 100,4);
        }
        public Dictionary<string, decimal> CalcualateSimple(Simple simpleRequest)
        { 
            var retVal = new Dictionary<string, decimal>();

            decimal interest = simpleRequest.Principal * simpleRequest.AnnualRate * simpleRequest.Years;
            decimal total = simpleRequest.Principal + interest;
            retVal.Add("Interest", Math.Round(interest, 2));
            retVal.Add("Total_Amount",Math.Round(total, 2));
            return retVal;
             
        }
        public decimal CalcualateTaxAdjusted(TaxAdjusted taxAdjRequest){ return 0.0M; }
    }
}
