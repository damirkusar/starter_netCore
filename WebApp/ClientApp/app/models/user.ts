export interface IUser {
    id?: string;
    email?: string;
    firstName?:string;
    lastName?: string;
    phoneNumber?: string;
    assignedRoles?: string[];
}