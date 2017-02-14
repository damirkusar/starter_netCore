export interface ICredentials {
    username: string;
    password: string;
    grant_type?:string;
    scope?: string;
}

export class Credentials implements ICredentials {
    constructor(public username: string, public password: string, public grant_type?: string, public scope?: string){}
}