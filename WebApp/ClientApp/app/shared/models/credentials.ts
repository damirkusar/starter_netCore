export interface ICredentials {
    username: string;
    password: string;
    grant_type?:string;
    scope?: string;
}