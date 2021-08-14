export interface IAnnualProfitOrLoss{
    id: number;
    year: number;
    email: string;
    amount: number;
    taxableIncome: number;
}

export interface IAnnualTaxLiability {
    year: number;
    email: string;
    amount: number;
    taxableIncome: number;
    capitalGainsTax: number;
    surtaxAmount: number;
    totalTaxLiaility: number;
    netProfit: number;
    residence: string;
    surtaxPercentage: number;
}

