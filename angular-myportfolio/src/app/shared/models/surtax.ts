export interface ISurtax {
    id: number;
    residence: string;
    amount: number;
  }

export class INewSurtax {
    id: number;
    residence: string;
    amount: number;
  }

export class INewSurtax1 {
    constructor(
    public id: number,
    public residence: string,
    public amount: number
    ) {}

}
