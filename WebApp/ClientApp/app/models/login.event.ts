import { ICredentials } from './credentials';

export class LoginEvent {
    credentials: ICredentials;
    private defaultPrevented: boolean;

    public isDefaultPrevented(): boolean {
        return (this.defaultPrevented);
    }

    public preventDefault(): void {
        this.defaultPrevented = true;
    }
}