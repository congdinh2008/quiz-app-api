import {
    HttpEvent,
    HttpHandlerFn,
    HttpInterceptorFn,
    HttpRequest,
  } from '@angular/common/http';
  import { inject } from '@angular/core';
  import { Observable } from 'rxjs';
import { AUTH_SERVICE_INJECTOR } from '../constants/injector.constant';
  
  export const authInterceptor: HttpInterceptorFn = (
    req: HttpRequest<any>,
    next: HttpHandlerFn
  ): Observable<HttpEvent<any>> => {
    const token = inject(AUTH_SERVICE_INJECTOR).getAccessToken();
    if (token) {
      const cloned = req.clone({
        setHeaders: {
          authorization: 'Bearer ' + token,
        },
      });
      return next(cloned);
    } else {
      return next(req);
    }
  };