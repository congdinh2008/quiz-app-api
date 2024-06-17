import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Inject,
  Input,
  input,
  OnInit,
  Output,
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCancel, faSave } from '@fortawesome/free-solid-svg-icons';
import { QUIZ_SERVICE_INJECTOR } from '../../../../constants/injector.constant';
import { IQuizService } from '../../../../services/interfaces/quiz-service.inteface';
import { QuizViewModel } from '../../../../view-models/quiz/quiz.view-model';

@Component({
  selector: 'app-quiz-detail',
  standalone: true,
  imports: [FontAwesomeModule, CommonModule, ReactiveFormsModule],
  templateUrl: './quiz-detail.component.html',
  styleUrl: './quiz-detail.component.scss',
})
export class QuizDetailComponent implements OnInit {
  public faSave = faSave;
  public faCancel = faCancel;
  @Input('selected-id') selectedId!: string;
  @Output('cancel') cancelForm = new EventEmitter();

  public isEdit: boolean = false;

  public form!: FormGroup;
  public model!: QuizViewModel;

  constructor(
    @Inject(QUIZ_SERVICE_INJECTOR) private quizService: IQuizService
  ) {}

  ngOnInit(): void {
    this.createForm();
    if (this.selectedId) {
      this.isEdit = true;
      this.getRecord();
    }
  }

  private updateFormValue() {
    this.form.patchValue({
      title: this.model.title,
      description: this.model.description,
      duration: this.model.duration,
      thumbnailUrl: this.model.thubmnailUrl,
    });
  }

  private getRecord() {
    this.quizService.getById(this.selectedId).subscribe((data) => {
      if (data) {
        this.model = data;
        this.updateFormValue();
      }
    });
  }

  private createForm(): void {
    this.form = new FormGroup({
      title: new FormControl('', [Validators.required]),
      description: new FormControl(''),
      duration: new FormControl(0, [Validators.required]),
      thumbnailUrl: new FormControl(''),
    });
  }

  public cancel(): void {
    this.cancelForm.emit();
  }

  public save(): void {
    const data = this.form.value as QuizViewModel;
    this.quizService.create(data).subscribe((data) => {
      if (data) {
        this.cancel();
      }
    });
  }
}
