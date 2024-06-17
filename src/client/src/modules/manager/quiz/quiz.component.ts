import { Component, Inject, OnInit } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEdit, faTrashCan, faPlus } from '@fortawesome/free-solid-svg-icons';
import { QUIZ_SERVICE_INJECTOR } from '../../../constants/injector.constant';
import { IQuizService } from '../../../services/interfaces/quiz-service.inteface';
import { QuizViewModel } from '../../../view-models/quiz/quiz.view-model';
import { CommonModule } from '@angular/common';
import { QuizDetailComponent } from './quiz-detail/quiz-detail.component';

@Component({
  selector: 'app-quiz',
  standalone: true,
  imports: [FontAwesomeModule, CommonModule, QuizDetailComponent],
  templateUrl: './quiz.component.html',
  styleUrl: './quiz.component.scss',
})
export class QuizComponent implements OnInit {
  public faEdit = faEdit;
  public faTrashCan = faTrashCan;
  public faPlus = faPlus;

  public quizzes: QuizViewModel[] = [];
  public isShowDetail: boolean = false;
  public selectedId!: string;

  /**
   *
   */
  constructor(
    @Inject(QUIZ_SERVICE_INJECTOR) private quizService: IQuizService
  ) {}

  ngOnInit(): void {
    this.getData();
  }

  getData(): void {
    this.quizService.getAll().subscribe((data) => {
      if (data) {
        this.quizzes = data;
      }
    });
  }

  public add(): void {
    this.isShowDetail = true;
  }

  public edit(id: string): void {
    this.isShowDetail = true;
    this.selectedId = id;
  }

  public delete(id: string): void {
    this.quizService.delete(id).subscribe((data) => {
      if (data) {
        this.getData();
      }
    });
  }

  public cancelForm(): void {
    this.isShowDetail = false;
    this.selectedId = '';
    this.getData();
  }
}
