using FinanceToolkitApi.Models;
namespace FinanceToolkitApi.Services
{
    public interface IInterestCalcService
    {
        Dictionary<string, decimal> SimpleInterest(Simple simple);
        /// <summary>
        /// Calculates simple accrued interest for a financial instrument using a day-count basis.
        /// The accrued interest is computed using the simple interest formula:
        /// Accrued Interest = Principal × (Annual Rate ÷ 100) × (Days Accrued ÷ Day Count Basis)
        Dictionary<string, decimal> SimpleAccruedInterest(SimpleAccrued accruedRequest);
        List<Dictionary<string, decimal>> CompoundAccruedInterest(CompoundAccrued accruedRequest);
        Dictionary<string, decimal> CompoundInterest(Compound compoundRequest);
        Dictionary<string, decimal> CompoundInterestWithContributions(CompoundWithContributions compoundRequest);

        Dictionary<string, decimal> AmortizedInterestPayment(Amortized amortizedRequest);
        List<Dictionary<string, decimal>> AmortizedInterestSchedule(Amortized amortizedRequest);
        
        Dictionary<string, decimal> CalcualateContinuousInterest(Continuous continuousRequest);
        Dictionary<string, decimal> CalculateDailyCompoundedInterestAmount(Daily dailyRequest);
        Dictionary<string, decimal> CalcualateEffectiveRate(EffectiveRate effectiveRateRequest);
        Dictionary<string, decimal> CalcualateInflationAdjusted(InflationAdjusted inflationAdjRequest);
        Dictionary<string,decimal> CalcualateSimple(Simple simpleRequest);
        Dictionary<string, decimal> CalcualateTaxAdjusted(TaxAdjusted taxAdjequest);


        Dictionary<string, decimal> CalcualateTaxAdjustedRealInterest(TaxAdjusted taxAdjequest);
    }
}
