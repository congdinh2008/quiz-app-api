import { InjectionToken } from '@angular/core';
import { IQuizService } from '../services/interfaces/quiz-service.inteface';
import { IAuthService } from '../services/interfaces/auth-service.interface';

export const AUTH_SERVICE_INJECTOR = new InjectionToken<IAuthService>(
  'AUTH_SERVICE_INJECTOR'
);

export const QUIZ_SERVICE_INJECTOR = new InjectionToken<IQuizService>(
  'QUIZ_SERVICE_INJECTOR'
);
