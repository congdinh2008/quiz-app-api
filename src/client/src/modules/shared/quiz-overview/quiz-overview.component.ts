import { Component, Input } from '@angular/core';
import { QuizViewModel } from '../../../view-models/quiz/quiz.view-model';

@Component({
  selector: 'app-quiz-overview',
  standalone: true,
  imports: [],
  templateUrl: './quiz-overview.component.html',
  styleUrl: './quiz-overview.component.scss'
})
export class QuizOverviewComponent {
  @Input('quiz-model') quiz!: QuizViewModel;
}
