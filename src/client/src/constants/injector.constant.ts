import { InjectionToken } from '@angular/core';
import { IQuizService } from '../services/interfaces/quiz-service.inteface';

export const QUIZ_SERVICE_INJECTOR = new InjectionToken<IQuizService>(
  'QUIZ_SERVICE_INJECTOR'
);
