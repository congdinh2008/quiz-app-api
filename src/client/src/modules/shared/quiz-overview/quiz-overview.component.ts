import { Component, Input, Output, EventEmitter } from '@angular/core';
import { QuizViewModel } from '../../../view-models/quiz/quiz.view-model';

@Component({
  selector: 'app-quiz-overview',
  standalone: true,
  imports: [],
  templateUrl: './quiz-overview.component.html',
  styleUrl: './quiz-overview.component.scss',
})
export class QuizOverviewComponent {
  @Input('quiz-model') quiz!: QuizViewModel;
  @Output('clickQuiz') clickQuiz: EventEmitter<QuizViewModel> =
    new EventEmitter();

  public takeQuiz(): void {
    this.clickQuiz.emit(this.quiz);
  }
}
