import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  public loginForm!: FormGroup;

  /**
   *
   */
  constructor(private httpClient: HttpClient) {}
  ngOnInit(): void {
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

    this.httpClient.post('http://localhost:5242/api/auth/login', loginData).subscribe((data:any) => {
      if(data) {
        console.log(data);
        // Store token in local storage
        localStorage.setItem('token', data.accessToken);
        const user =  JSON.parse(data?.userInformation);
        localStorage.setItem('userInformation', JSON.stringify(user));
      }
    });
  }
}
