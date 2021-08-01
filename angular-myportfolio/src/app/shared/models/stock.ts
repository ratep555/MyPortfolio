export interface IStock {
    id: number;
    symbol: string;
    currentPrice: number;
    companyName: string;
    category: string;
    modality: string;
    segment: string;
    typeOfStock: string;
    numberOfEmployees: number;
    sharesOutstanding: number;
    ownShares: number;
    revenue: number;
    expenditure: number;
    enterpriseValue: number;
    dividend: number;
  }

export class INewStock {
    id: number;
    symbol: string;
    currentPrice: number;
    companyName: string;
    category: string;
    modality: string;
    segment: string;
    typeOfStock: string;
    totalQuantity: number;
  }
