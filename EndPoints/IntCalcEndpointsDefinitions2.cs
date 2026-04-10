using FinanceToolkitApi.Models;
using FinanceToolkitApi.Services;

namespace FinanceToolkitApi.EndPoints
{
    public static partial class IntCalcEndpoints
    {
        internal static IResult Accrued(IInterestCalcService calcService, Accrued accrued)
        {
            return Results.Ok(calcService.CalcualateAcrruedInterest(accrued));
        }

        internal static IResult Amortized(IInterestCalcService calcService, Amortized amortized)
        {
            return Results.Ok(calcService.CalcualateAmortizedInterest(amortized));
        }
        internal static IResult Compound(IInterestCalcService calcService, Compound compound)
        {
            return Results.Ok(calcService.CalcualateCompoundInterest(compound));
        }
        internal static IResult Continuous(IInterestCalcService calcService, Continuous continuous)
        {
            return Results.Ok(calcService.CalcualateContinuousInterest(continuous));
        }
        internal static IResult Daily(IInterestCalcService calcService, Daily daily)
        {
            return Results.Ok(calcService.CalcualateDailyInterest(daily));
        }
        internal static IResult EffectiveRate(IInterestCalcService calcService, EffectiveRate effectiveRate)
        {
            return Results.Ok(calcService.CalcualateEffectiveRate(effectiveRate));
        }
        internal static IResult InflationAdjusted(IInterestCalcService calcService, InflationAdjusted inflationAdj)
        {
            return Results.Ok(calcService.CalcualateInflationAdjusted(inflationAdj));
        }
        internal static IResult Simple(IInterestCalcService calcService, Simple simple)
        {
            return Results.Ok(calcService.CalcualateSimple(simple));
        }
        internal static IResult TaxAdjusted(IInterestCalcService calcService, TaxAdjusted taxAdj)
        {
            return Results.Ok(calcService.CalcualateTaxAdjusted(taxAdj));
        }

    }
}
