import { Observable } from 'rxjs';
import { QuizViewModel } from '../../view-models/quiz/quiz.view-model';
import { FilterViewModel } from '../../view-models/filter.view-model';
import { PaginatedResult } from '../../view-models/paginated-result.view-model';

export interface IQuizService {
  getAll(): Observable<QuizViewModel[]>;
  getById(id: string): Observable<QuizViewModel>;
  search(filter: FilterViewModel): Observable<PaginatedResult<QuizViewModel>>;
  create(quiz: QuizViewModel): Observable<boolean>;
  update(quiz: QuizViewModel): Observable<boolean>;
  delete(id: string): Observable<boolean>;
}
