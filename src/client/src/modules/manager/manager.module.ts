import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuizComponent } from './quiz/quiz.component';
import { QuestionComponent } from './question/question.component';
import { RouterModule, Routes } from '@angular/router';
import { QUIZ_SERVICE_INJECTOR } from '../../constants/injector.constant';
import { QuizService } from '../../services/implementation/quiz.service';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'quiz-manager',
        component: QuizComponent,
      },
      {
        path: 'question-manager',
        component: QuestionComponent,
      },
      {
        path: '**',
        redirectTo: 'quiz-manager',
        pathMatch: 'full',
      },
    ],
  },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  providers:[
    { provide: QUIZ_SERVICE_INJECTOR, useClass: QuizService}
  ]
})
export class ManagerModule { }
