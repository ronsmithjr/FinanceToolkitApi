# interest_calculator

## 🧮 Types of Interest Calculators

| Type                          | Description                                                      | Use Case Example                              |
|-------------------------------|------------------------------------------------------------------|-----------------------------------------------|
| Simple Interest               | Calculates interest only on the original principal.               | Short-term loans, basic savings accounts      |
| Compound Interest             | Calculates interest on principal plus previously earned interest. | Investment accounts, long-term savings        |
| Accrued Interest              | Calculates interest accumulated but not yet paid.                 | Bonds, loans with deferred payments           |
| Amortized Loan                | Breaks down payments into principal and interest over time.       | Mortgages, car loans                          |
| Daily Interest                | Computes interest accrued daily, often used for short-term loans. | Payday loans, credit card balances            |
| Continuously Compounded       | Uses exponential growth with infinite compounding periods.        | Advanced financial modeling, theoretical use  |
| Effective Interest Rate (EIR) | Adjusts for compounding to show the true annual rate.             | Comparing loan offers with different terms    |
| APR vs APY Calculator         | Compares Annual Percentage Rate vs Annual Percentage Yield.       | Credit cards, savings accounts                |
| Inflation-Adjusted Interest   | Accounts for purchasing power loss over time.                     | Long-term investment planning                 |
| Tax-Adjusted Interest         | Calculates post-tax returns on interest income.                   | Retirement accounts, taxable investments      |

## 📆 Year Basis Options

| Basis                 | Days/Year | Common Use Cases                                      |
|-----------------------|-----------|------------------------------------------------------|
| Actual/Actual (365)   | 365       | Most consumer loans, savings accounts, U.S. Treasury instruments |
| Banker's Rule (360)   | 360       | Corporate bonds, commercial loans, international finance         |

## 🔍 Nominal vs. Real vs. Effective

| Type           | Adjusts For Inflation? | Adjusts For Compounding? | Used For                                 |
|----------------|-----------------------|-------------------------|------------------------------------------|
| Nominal Rate   | ❌ No                  | ❌ No                   | Quoted rates on loans/investments        |
| Real Rate      | ✅ Yes                 | ❌ No                   | Purchasing power & economic analysis     |
| Effective Rate | ❌ No                  | ✅ Yes                  | True cost of borrowing or yield          |


##  Special Notes

EULER (or Euler's number) is a mathematical constant approximately equal to 2.71828. It is commonly denoted as 
e
e and is the base of the natural logarithm. In finance and mathematics, 
e
e is used for calculations involving continuous compounding, exponential growth, and decay. In your code, EULER is used to approximate 
e
e for continuous compounding interest calculations.