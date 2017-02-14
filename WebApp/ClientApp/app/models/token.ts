export interface IToken {
    token_type: string;
    access_token: string;
    expires_in: number;
    id_token: string;
}

export class Token implements IToken {
    constructor(public token_type: string, public access_token: string, public expires_in: number, public id_token: string){}
}