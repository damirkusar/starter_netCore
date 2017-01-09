export interface IUser {
    id?: string;
    email?: string;
    firstName?:string;
    lastName?: string;
    phoneNumber?: string;
    assignedRoles?: string[];
}

export class User implements IUser {
    id?: string;
    email?: string;
    firstName?:string;
    lastName?: string;
    phoneNumber?: string;
    assignedRoles?: string[];

    constructor(email: string, firstName: string, lastName: string) {
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
    }
}