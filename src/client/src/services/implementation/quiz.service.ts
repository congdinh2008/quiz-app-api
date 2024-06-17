import { HttpClient } from '@angular/common/http';
import {
  IQuizService,
} from '../interfaces/quiz-service.inteface';
import { Observable } from 'rxjs';
import { QuizViewModel } from '../../view-models/quiz/quiz.view-model';
import { Injectable } from '@angular/core';
import { FilterViewModel } from '../../view-models/filter.view-model';
import { PaginatedResult } from '../../view-models/paginated-result.view-model';

@Injectable()
export class QuizService implements IQuizService {
  private baseUrl: string = 'http://localhost:5242/api/Quizzes';
  /**
   *
   */
  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<QuizViewModel[]> {
    return this.httpClient.get<QuizViewModel[]>(this.baseUrl);
  }
  
  getById(id: string): Observable<QuizViewModel> {
    return this.httpClient.get<QuizViewModel>(`${this.baseUrl}/${id}`);
  }
  
  search(filter: FilterViewModel): Observable<PaginatedResult<QuizViewModel>> {
    return this.httpClient.post<PaginatedResult<QuizViewModel>>(
      `${this.baseUrl}/search`,
      filter
    );
  }
  
  create(quiz: QuizViewModel): Observable<boolean> {
    return this.httpClient.post<boolean>(this.baseUrl, quiz);
  }
  
  update(quiz: QuizViewModel): Observable<boolean> {
    return this.httpClient.put<boolean>(this.baseUrl, quiz);
  }
  
  delete(id: string): Observable<boolean> {
    return this.httpClient.delete<boolean>(`${this.baseUrl}/${id}`);
  }
}
