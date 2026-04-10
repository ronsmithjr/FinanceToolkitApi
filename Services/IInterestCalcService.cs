using FinanceToolkitApi.Models;
namespace FinanceToolkitApi.Services
{
    public interface IInterestCalcService
    {
        decimal CalcualateAcrruedInterest(Accrued accruedRequest);
        decimal CalcualateAmortizedInterest(Amortized amortizedRequest);
        decimal CalcualateCompoundInterest(Compound compoundRequest);
        decimal CalcualateContinuousInterest(Continuous continuousRequest);
        decimal CalcualateDailyInterest(Daily dailyRequest);
        decimal CalcualateEffectiveRate(EffectiveRate effectiveRateRequest);
        decimal CalcualateInflationAdjusted(InflationAdjusted inflationAdjRequest);
        Dictionary<string,decimal> CalcualateSimple(Simple simpleRequest);
        decimal CalcualateTaxAdjusted(TaxAdjusted taxAdjequest);
    }
}
