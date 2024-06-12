import { Routes } from '@angular/router';
import { AuthentictedLayoutComponent } from './shared/authenticted-layout/authenticted-layout.component';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { ContactComponent } from './contact/contact.component';
import { QuizComponent } from './quiz/quiz.component';
import { QuestionComponent } from './question/question.component';
import { ManagerLayoutComponent } from './shared/manager-layout/manager-layout.component';

export const routes: Routes = [
    {
        path: 'manager',
        component: ManagerLayoutComponent,
        children: [
            {
                path: 'quiz',
                component: QuizComponent
            },
            {
                path: 'question',
                component: QuestionComponent
            }
        ]
    },
    {
        path: '',
        component: AuthentictedLayoutComponent,
        children: [
            {
                path: 'home',
                component: HomeComponent
            },
            {
                path: 'about',
                component: AboutComponent
            },
            {
                path: 'contact',
                component: ContactComponent
            },
            {
                path: '**',
                redirectTo: 'home',
                pathMatch: 'full'
            }
        ]
    }
];
