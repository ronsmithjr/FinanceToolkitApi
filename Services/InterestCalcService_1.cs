using FinanceToolkitApi.Models;

namespace FinanceToolkitApi.Services
{
    public partial class InterestCalcService : IInterestCalcService
    {
        public Dictionary<string, decimal> SimpleAccruedInterest(SimpleAccrued accruedRequest)
        {
            decimal rate = accruedRequest.AnnualRate / 100;
            decimal accruedInterest = rate / accruedRequest.DayCountBasis * accruedRequest.DaysAccrued * accruedRequest.Principal;

            return new Dictionary<string, decimal>
            {
                {"Annual_Rate", rate},
                {"Year_Basis", accruedRequest.DayCountBasis },
                {"Days_Accrued", accruedRequest.DaysAccrued},
                {"Principal", accruedRequest.Principal},
                {"Accrued_Interest",Math.Round(accruedInterest, 4) }
            };
        }
        public Dictionary<string, decimal> CompoundAccruedInterest(CompoundAccrued cr)
        {
            decimal n = cr.CompoundsPerYear * cr.Years;
            decimal ratePerPeriod = (cr.AnnualRate / 100) / cr.CompoundsPerYear;

            // If you want to support partial periods (accrued interest for days):
            // Uncomment and use these lines if DaysAccrued and DayCountBasis are available
            decimal fractionOfYear = cr.DaysAccrued / (decimal)cr.DayCountBasis;
            n = cr.CompoundsPerYear * fractionOfYear;

            decimal total_amt = cr.Principal * (decimal)Math.Pow((double)(1 + ratePerPeriod), (double)n);
            decimal interest = total_amt - cr.Principal;

            return new Dictionary<string, decimal>
            {
                {"Principal",cr.Principal },
                {"AnnualRate", cr.AnnualRate },
                {"CompoundsPerYear", cr.CompoundsPerYear },
                {"Years", cr.Years },
                {"Interest", Math.Round(interest, 2) },
                {"Total_Amount", Math.Round(total_amt, 2) },
                {"Days Accrued", cr.DaysAccrued },
                {"Day Count Basis", cr.DayCountBasis }
            };
        }

        public Dictionary<string, decimal> AmortizedInterestPayment(Amortized ar)
        {
            double rate = ((double)ar.AnnualRate / 100) / (double)ar.PeriodsPerYear;
            int CompundingPeriods = ar.Years * ar.PeriodsPerYear;

            double numerator = rate * Math.Pow(1 + rate, CompundingPeriods);
            double denomimator = Math.Pow(1 + rate, CompundingPeriods) - 1;

            decimal Payment = ar.Principal * (decimal)(numerator / denomimator);
            return new Dictionary<string, decimal>
            {
                {"Annual_Rate", Math.Round(ar.AnnualRate,2)},
                {"Periods_Per_Year", ar.PeriodsPerYear},
                {"Years", ar.Years},
                {"Payment", Math.Round(Payment, 2)}
            };
        }

        public List<Dictionary<string, decimal>> AmortizedInterestSchedule(Amortized ar)
        {
            double rate = ((double)ar.AnnualRate / 100) / (double)ar.PeriodsPerYear;
            int compundingPeriods = ar.Years * ar.PeriodsPerYear;

            decimal payment = AmortizedInterestPayment(ar)["Payment"];

            decimal balance = ar.Principal;

            var schedule = new List<Dictionary<string, decimal>>();

            for (int period = 1; period <= compundingPeriods; period++)
            {
                decimal interest = Math.Round(balance * (decimal)rate, 2);
                decimal principalPaid = Math.Round(payment - interest, 2);
                balance = Math.Round(balance - principalPaid, 2);

                schedule.Add(new Dictionary<string, decimal>
                {
                    { "Period", period },
                    { "Payment", payment },
                    { "Principal Paid", principalPaid },
                    { "Interest Paid", interest },
                    { "Remaining Balance", balance > 0 ? balance : 0 }
                });
            }

            return schedule;

        }

        public Dictionary<string, decimal> CalcualateCompoundInterest(CompoundAccrued cr)
        {
            //decimal step1 = 1 + (cr.AnnualRate / 100) / cr.CompoundsPerYear;
            //decimal total_amt = cr.Principal * (decimal)Math.Pow((double)step1, cr.CompoundsPerYear * cr.Years);


            //decimal interest = total_amt - cr.Principal;

            decimal n = cr.CompoundsPerYear * cr.Years;
            decimal ratePerPeriod = (cr.AnnualRate / 100) / cr.CompoundsPerYear;

            decimal fractionOfYear = cr.DaysAccrued / (decimal)cr.DayCountBasis;
            n = cr.CompoundsPerYear * fractionOfYear;

            decimal total_amt = cr.Principal * (decimal)Math.Pow((double)(1 + ratePerPeriod), (double)n);
            decimal interest = total_amt - cr.Principal;

            return new Dictionary<string, decimal>
            {
                {"Principal",cr.Principal },
                {"AnnualRate", cr.AnnualRate },
                {"CompoundsPerYear", cr.CompoundsPerYear },
                {"Years", cr.Years },
                {"Days_Accrued", cr.DaysAccrued },
                {"Day_Count_Basis", cr.DayCountBasis },
                {"Interest", Math.Round(interest, 2) },
                {"Total_Amount", Math.Round(total_amt, 2) }
            };
        }
        /// <summary>
        /// This method produces a month‑by‑month projection of investment growth that reflects:
        /// An initial lump‑sum investment
        /// Regular monthly contributions
        /// Growth through compound interest

        /// The output shows how the balance evolves over time, helping stakeholders understand how savings or investments  accumulate, when growth accelerates, and the combined impact of discipline(contributions) and time(compounding).
        /// </summary>
        /// <param name="cr"></param>
        /// <returns>month‑by‑month projection of investment growth</returns>
        public Dictionary<string, decimal> CompoundInterestWithContributions(CompoundWithContributions cr)
        {
            decimal balance = cr.Principal;
            decimal monthlyRate = (cr.AnnualRate / 100) / 12.0M;
            int totalMonths = cr.Years * 12;

            var results = new Dictionary<string, decimal>();
            results.Add("AnnualRate", cr.AnnualRate);
            results.Add("Total Months", (decimal)totalMonths);
            results.Add("Orig_Balance", Math.Round(cr.Principal));

            for (int i = 0; i < totalMonths; i++)
            {
                balance = balance * (1 + monthlyRate) + cr.MonthlyContribution;
                results.Add($"Month_Balance{i + 1}", Math.Round(balance, 2));
            }
            return results;

        }
        /// <summary>
        /// continuous compounding is analytically useful but operationally impractical.
        /// </summary>
        /// <param name="cr"></param>
        /// <returns></returns>
        public Dictionary<string, decimal> CalcualateContinuousInterest(Continuous cr)
        {
            double exponent = (double)((cr.AnnualRate / 100) * cr.Years);
            decimal growthFactor = (decimal)Math.Exp(exponent);

            return new Dictionary<string, decimal>
            {
                {"Annual_Rate", cr.AnnualRate },
                {"Years", cr.Years},
                {"Yield", Math.Round(cr.Principal * growthFactor, 2)},
            };
        }
        public Dictionary<string, decimal> CalculateDailyCompoundedInterestAmount(Daily dr)
        {
            decimal dailyRate = (dr.AnnualRate / 100) / dr.DayCountBasis;


            decimal compounded =
                    dr.Principal *
                    (decimal)Math.Pow((double)(1 + dailyRate), dr.DaysAccrued);
            decimal interestEarned = Math.Round(compounded - dr.Principal, 2);

            return new Dictionary<string, decimal>
            {
                {"Principal", Math.Round(dr.Principal,2) },
                {"Days Accrued", dr.DaysAccrued },
                {"Annual_Rate", Math.Round(dr.AnnualRate,2) },
                {"Year_Basis", dr.DayCountBasis },
                {"Interest_Earned", Math.Round(interestEarned, 4) }
            };
        }
        public Dictionary<string, decimal> CalcualateEffectiveRate(EffectiveRate err)
        {
            decimal perPeriodGrowth = 1 + (err.NominalRate / err.CompoundsPerYear);
            decimal compoundFullYear = (decimal)Math.Pow((double)perPeriodGrowth, (double)err.CompoundsPerYear);
            decimal effectiveAnnualRate = compoundFullYear - 1;

            return new Dictionary<string, decimal>
            {
                {"Nominal_Rate", Math.Round(err.NominalRate, 2) },
                {"Compounds_Per_Year", err.CompoundsPerYear },
                {"Compound_Full_Year", Math.Round(compoundFullYear, 2) },
                {"Per_Period_Growth", Math.Round(perPeriodGrowth, 2) },
                {"Effective_Annual_Rate", Math.Round(effectiveAnnualRate, 4) }
            };
        }
        public Dictionary<string, decimal> CalcualateInflationAdjusted(InflationAdjusted inflAdjRequest)
        {
            decimal realRate = (1 + inflAdjRequest.Nominal) / (1 + inflAdjRequest.Inflation) - 1;

            return new Dictionary<string, decimal>
            {
                {"Nominal_Rate", inflAdjRequest.Nominal},
                {"Inflation", inflAdjRequest.Inflation},
                {"Real_Rate", realRate}
            };
        }
        public Dictionary<string, decimal> CalcualateSimple(Simple simpleRequest)
        {
            var retVal = new Dictionary<string, decimal>();

            decimal interest = simpleRequest.Principal * (simpleRequest.AnnualRate / 100) * simpleRequest.Years;
            decimal total = simpleRequest.Principal + interest;

            return new Dictionary<string, decimal>
            {
                {"Principal", simpleRequest.Principal},
                {"Annual_Rate", Math.Round(simpleRequest.AnnualRate, 2)},
                {"Years", simpleRequest.Years },
                {"Interest", Math.Round(interest, 4)},
                {"Total_Amount",Math.Round(total, 2)}
            };

        }
        public Dictionary<string, decimal> CalcualateTaxAdjusted(TaxAdjusted tr)
        {
            decimal afterTaxIncome = tr.Income * (1 - tr.TaxRate);
            return new Dictionary<string, decimal>
            {
                {"Income", tr.Income},
                {"Tax_Rate", tr.TaxRate},
                {"After_Tax_Income", afterTaxIncome}
            };
        }

        public Dictionary<string, decimal> CalcualateTaxAdjustedRealInterest(TaxAdjusted tr)
        {
            decimal afterTaxRate = tr.NominalRate * (1 - tr.TaxRate);
            decimal realRate = (1 - afterTaxRate) / (1 + tr.InflationRate);
            return new Dictionary<string, decimal>
            {
                {"Nominal_Rate", tr.NominalRate},
                {"Tax_Rate", tr.TaxRate},
                {"Inflation_Rate", tr.InflationRate},
                {"After_Tax_Rate", afterTaxRate},
                {"Real_Rate",realRate }
            };
        }        
    }
}
