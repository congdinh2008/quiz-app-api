import { LoginViewModel } from '../../view-models/auth/login.view-model';
import { LoginResponseViewModel } from '../../view-models/auth/login-response.view-model';
import { UserViewModel } from '../../view-models/user/user.view-model';

export interface IAuthService {
  login(
    returnUrl: string,
    model: LoginViewModel
  ): Promise<LoginResponseViewModel | undefined>;

  isAuthenticated(): boolean;

  getAccessToken(): string;

  logout(): boolean;

  getCurrentUser(): UserViewModel | undefined;

    isManager(): boolean;
}
