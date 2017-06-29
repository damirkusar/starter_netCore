export interface ICredentials {
    username: string;
    password: string;
    grant_type?: string;
    scope?: string;
}

export interface IErrorMessage {
    status: number;
    statusText: string;
    error?: string;
    error_description?: string;
    message?: string;
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
    fullName?: string;
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