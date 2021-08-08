export interface IPortfolioAccount {
    id: number;
    stockId: number;
    transactionId: number;
    symbol: string;
    totalQuantity: number;
    totalPriceOfPurchasePerStock: number;
    totalMarketValuePerStock: number;
    averagePriceOfPurchase: number;
    currentPrice: number;
    email: string;
    portfolioPercentage: number;
}

export interface IProfitOrLoss {
    clientPortfolios: IPortfolioAccount[];
    totalMarketValue: number;
    totalPriceOfPurchase: number;
}

