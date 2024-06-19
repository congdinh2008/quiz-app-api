import { DOCUMENT } from '@angular/common';
import { IAuthService } from '../interfaces/auth-service.interface';
import { Inject, Injectable } from '@angular/core';
import { LoginResponseViewModel } from '../../view-models/auth/login-response.view-model';
import { LoginViewModel } from '../../view-models/auth/login.view-model';
import { UserViewModel } from '../../view-models/user/user.view-model';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AuthService implements IAuthService {
  private _localStorage: Storage | undefined;
  public _response: any;
  public apiUrl: string = 'http://localhost:5242/api/Auth/login';
  public _user: UserViewModel | undefined;

  /**
   *
   */
  constructor(
    @Inject(DOCUMENT) private document: Document,
    private httpClient: HttpClient
  ) {
    this._localStorage = document.defaultView?.localStorage;

    if (!this._response) {
      const loginResult: any = this._localStorage?.getItem('loginResult');
      if (loginResult) {
        this._response = JSON.parse(loginResult);
      }
    }
  }

  private isLoggedIn(): boolean {
    return (
      this._response != null &&
      this._response.accessToken != null &&
      this._response.expiresAt != null
    );
  }

  public isAuthenticated(): boolean {
    return this.isLoggedIn();
  }

  public getAccessToken(): string {
    return this._response ? this._response.accessToken : '';
  }

  public getCurrentUser(): UserViewModel | undefined {
    const userJSON = this._localStorage?.getItem('userInformation');
    const user: any = userJSON ? JSON.parse(userJSON) : null;
    return user ? user : null;
  }

  public login(
    returnUrl: string,
    model: LoginViewModel
  ): Promise<LoginResponseViewModel | undefined> {
    // Luu duong dan noi nguoi dung da di toi trang login
    this._localStorage?.setItem('returnUrl', returnUrl);

    return this.httpClient
      .post<LoginResponseViewModel>(this.apiUrl, model)
      .toPromise()
      .then((response) => {
        this._response = response;
        this._localStorage?.setItem('loginResult', JSON.stringify(response));
        this._user = JSON.parse(this._response?.userInformation);
        this._localStorage?.setItem(
          'userInformation',
          JSON.stringify(this._user)
        );
        return response;
      })
      .catch((error) => {
        return error;
      });
  }

  logout(): boolean {
    this._localStorage?.removeItem('loginResult');
    this._localStorage?.removeItem('returnUrl');
    this._localStorage?.removeItem('userInformation');
    return true;
  }

  public isManager(): boolean {
    const userJSON = this._localStorage?.getItem('userInformation');
    const user: any = userJSON ? JSON.parse(userJSON) : null;
    var result =
      user?.roles.includes('Administrator') || user?.roles.includes('Manager');

    return result ? true : false;
  }
}
