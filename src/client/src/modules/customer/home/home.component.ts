import { Component, OnInit } from '@angular/core';
import { QuizViewModel } from '../../../view-models/quiz/quiz.view-model';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  public quizzes: QuizViewModel[] = [];

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {
    this.getData();
  }

  private getData() {
    this.httpClient
      .get<QuizViewModel[]>('http://localhost:5242/api/Quizzes')
      .subscribe((data) => {
        if (data) {
          this.quizzes = data;
        }
      });
  }
}
