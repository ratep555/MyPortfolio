import { IStock } from './stock';
import { ISurtax } from './surtax';

export interface IPaginationForSurtax {
    page: number;
    pageCount: number;
    count: number;
    data: ISurtax[];
  }

export interface IPaginationForStock {
    page: number;
    pageCount: number;
    count: number;
    data: IStock[];
  }
