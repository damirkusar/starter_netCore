export interface IErrorMessage {
    status: number;
    message: string;
}

export class ErrorMessage implements IErrorMessage {
    status: number;
    message: string;

    constructor(status: number, message: string) {
        this.status = status;
        this.message = message;
    }
}