export interface ITransactionForUser {
    id: number;
    stockId: number;
    stock: string;
    userId: string;
    date: string;
    price: number;
    quantity: number;
    purchase: boolean;
    resolved: number;
}

export interface ITransactionsWithProfitAndTraffic {
    listOfTransactions: ITransactionForUser[];
    totalNetProfit: number;
    totalTraffic: number;
}
