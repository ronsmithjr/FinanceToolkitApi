using FinanceToolkitApi.Models;
using FinanceToolkitApi.Services;

namespace FinanceToolkitApi.EndPoints
{
    public static partial class IntCalcEndpoints
    {
        internal static IResult SimpleAccruedInterest(IInterestCalcService calcService, SimpleAccrued accrued)
        {
            if (accrued == null)
            {
                return Results.BadRequest("The request Object cannot be null.");
            }
            if (accrued.DayCountBasis != 365 && accrued.DayCountBasis != 360)
            {
                return Results.BadRequest("The DayCountBasis must either be 360 or 365");
            }
            if (accrued.DaysAccrued <= 0)
            {
                return Results.Ok("No calculation has been done because nothing has accrued.");
            }
            if(accrued.Principal <= 0)
            {
                return Results.BadRequest("The principal must be greater than 0");
            }

            return Results.Ok(calcService.SimpleAccruedInterest(accrued));
        }
        internal static IResult CompoundAccruedInterest(IInterestCalcService calcService, CompoundAccrued accrued)
        {
            if (accrued == null)
            {
                return Results.BadRequest("The request Object cannot be null.");
            }
            if (accrued.DayCountBasis != 365 && accrued.DayCountBasis != 360)
            {
                return Results.BadRequest("The DayCountBasis must either be 360 or 365");
            }
            if (accrued.DaysAccrued <= 0)
            {
                return Results.Ok("No calculation has been done because nothing has accrued.");
            }
            if (accrued.Principal <= 0)
            {
                return Results.BadRequest("The principal must be greater than 0");
            }

            return Results.Ok(calcService.CompoundAccruedInterest(accrued));
        }

        internal static IResult AmortizedPayment(IInterestCalcService calcService, Amortized amortized)
        {
            return Results.Ok(calcService.AmortizedInterestPayment(amortized));
        }
        internal static IResult AmortizedSchedule(IInterestCalcService calcService, Amortized amortized)
        {
            return Results.Ok(calcService.AmortizedInterestSchedule(amortized));
        }
        internal static IResult Compound(IInterestCalcService calcService, CompoundAccrued compound)
        {
            return Results.Ok(calcService.CalcualateCompoundInterest(compound));
        }
        internal static IResult CompoundInterestWithContributions(IInterestCalcService calcService, CompoundWithContributions compound)
        {
            if (compound.AnnualRate < 0 || compound.Years <= 0)
            {
                return Results.BadRequest("Invalid Annual Rate or Years.  Please check and resend");
            }
            return Results.Ok(calcService.CompoundInterestWithContributions(compound));
        }
        internal static IResult Continuous(IInterestCalcService calcService, Continuous continuous)
        {
            if (continuous.AnnualRate < 0 || continuous.AnnualRate > 1)
            {
                return Results.BadRequest("Invalid Annual Rate or Years. Please make sure it is between 0 and 1");
            }
            return Results.Ok(calcService.CalcualateContinuousInterest(continuous));
        }
        internal static IResult Daily(IInterestCalcService calcService, Daily daily)
        {
            if (daily.DayCountBasis <= 0 || daily.DaysAccrued < 0)
            {
                return Results.BadRequest("Invalid day basis or accrual period.");
            }
            return Results.Ok(calcService.CalculateDailyCompoundedInterestAmount(daily));
        }
        internal static IResult EffectiveRate(IInterestCalcService calcService, EffectiveRate err)
        {
            if (err.NominalRate < 0)
            {
                return Results.BadRequest("Nominal Rate cannot be 0.");
            }
            if (err.CompoundsPerYear < 0)
            {
                return Results.BadRequest("Compunds Per Year cannot be 0.");
            }
            return Results.Ok(calcService.CalcualateEffectiveRate(err));
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
        internal static IResult TaxAdjustedRealInterest(IInterestCalcService calcService, TaxAdjusted taxAdj)
        {
            return Results.Ok(calcService.CalcualateTaxAdjustedRealInterest(taxAdj));
        }

    }
}
