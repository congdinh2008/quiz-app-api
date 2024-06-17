import { Component, Inject, OnInit } from '@angular/core';
import { QuizViewModel } from '../../../view-models/quiz/quiz.view-model';
import { CommonModule } from '@angular/common';
import { QuizOverviewComponent } from '../../shared/quiz-overview/quiz-overview.component';
import { QUIZ_SERVICE_INJECTOR } from '../../../constants/injector.constant';
import { IQuizService } from '../../../services/interfaces/quiz-service.inteface';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, QuizOverviewComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  public quizzes: QuizViewModel[] = [];

  constructor(
    @Inject(QUIZ_SERVICE_INJECTOR)
    protected quizService: IQuizService
  ) {}

  ngOnInit(): void {
    this.getData();
  }

  private getData() {
    this.quizService.getAll().subscribe((data) => {
      if (data) {
        this.quizzes = data;
      }
    });
  }
}
