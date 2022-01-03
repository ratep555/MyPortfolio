import { IUser } from './user';

export class MyParams {
    categoryId = 0;
    query: string;
    page = 1;
    pageCount = 10;
}

export class UserParams {
    categoryId = 0;
    query: string;
    page = 1;
    pageCount = 10;
    displayName: string;

    constructor(user: IUser) {
        this.displayName = user.displayName;
   }
}
