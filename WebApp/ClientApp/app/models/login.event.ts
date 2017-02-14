import { ICredentials } from './credentials';

export class LoginEvent {
    credentials: ICredentials;
    private _isDefaultPrevented: boolean;

    public isDefaultPrevented(): boolean {
        return (this._isDefaultPrevented);
    }

    public preventDefault(): void {
        this._isDefaultPrevented = true;
    }
}