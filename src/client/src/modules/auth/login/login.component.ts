import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { IAuthService } from '../../../services/interfaces/auth-service.interface';
import { AUTH_SERVICE_INJECTOR } from '../../../constants/injector.constant';
import { ActivatedRoute, Route, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  public loginForm!: FormGroup;
  public isAuthenticated: boolean = false;

  /**
   *
   */
  constructor(
    @Inject(AUTH_SERVICE_INJECTOR) private authService: IAuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.isAuthenticated = this.authService.isAuthenticated();
    
    if (this.isAuthenticated) {
      this.router.navigate(['/']);
    }

    this.createForm();
  }

  private createForm(): void {
    this.loginForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
      ]),
    });
  }

  public onSubmit(): void {
    const loginData = this.loginForm.value;

    this.authService.login('/', loginData).then((data) => {
      if (data) {
        this.router.navigate(['/']);
      } else {
        console.log('Login failed');
      }
    });
  }
}
