import { ICategory } from './category';
import { IModality } from './modality';
import { ISegment } from './segment';
import { IStock } from './stock';
import { ISurtax } from './surtax';
import { ITypeOfStock } from './typeOfStock';
import { IUser, IUser1 } from './user';

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

export interface IPaginationForCategory {
    page: number;
    pageCount: number;
    count: number;
    data: ICategory[];
  }

export interface IPaginationForModality {
    page: number;
    pageCount: number;
    count: number;
    data: IModality[];
  }

export interface IPaginationForSegment {
    page: number;
    pageCount: number;
    count: number;
    data: ISegment[];
  }

export interface IPaginationForTypeOfStock {
    page: number;
    pageCount: number;
    count: number;
    data: ITypeOfStock[];
  }

export interface IPaginationForUser {
    page: number;
    pageCount: number;
    count: number;
    data: IUser1[];
  }







