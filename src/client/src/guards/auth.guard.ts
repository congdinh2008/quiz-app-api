import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  CanLoad,
  Route,
  UrlSegment,
  Router,
} from '@angular/router';
import { Observable } from 'rxjs';
import { AUTH_SERVICE_INJECTOR } from '../constants/injector.constant';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate, CanLoad {
  private localStorage: Storage | undefined;

  public constructor(
    @Inject(DOCUMENT) private document: Document,
    protected router: Router
  ) {
    this.localStorage = document.defaultView?.localStorage;
  }

  /*
   * Whether user can access to authenticated modules or not.
   * */
  public canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    // if authenticated
    if (this.hasUserAuthenticated()) {
      if (this.isManager()) {
        return true;
      }

      // if not admin authenticated
      this.router.navigate(['error/403']);
      return false;
    }

    // if not authenticated
    this.router.navigate(['auth/login'], {
      queryParams: { returnUrl: state.url },
    });
    return false;
  }

  /*
   * Whether module can be loaded or not.
   * */
  public canLoad(
    route: Route,
    segments: UrlSegment[]
  ): Observable<boolean> | Promise<boolean> | boolean {
    return this.hasUserAuthenticated();
  }

  /*
   * Check whether user has authenticated into system or not.
   * */
  protected hasUserAuthenticated(): boolean {
    const loginResult = this.localStorage?.getItem('loginResult');
    return loginResult ? true : false;
  }

  private isManager(): boolean {
    const userInformation = this.localStorage?.getItem('userInformation');
    if (userInformation) {
      const user = JSON.parse(userInformation);
      var result =
        user.roles.includes('Administrator') || user.roles.includes('Manager');
      return result;
    }

    return false;
  }
}
