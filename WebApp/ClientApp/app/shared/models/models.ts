export interface ICredentials {
    userName: string;
    password: string;
    grant_type?:string;
    scope?: string;
}

export interface IErrorMessage {
    status: number;
    message: string;
}

export interface IToken {
    token_type: string;
    access_token: string;
    expires_in: number;
    id_token: string;
}

export interface IUser {
    id?: string;
    email?: string;
    userName?: string;
    firstName?: string;
    lastName?: string;
    phoneNumber?: string;
    image?: string;
    assignedRoles?: string[];
}

export interface IUserRegister {
    email?: string;
    userName?: string;
    firstName?: string;
    lastName?: string;
    password?: string;
    confirmPassword?: string;
}