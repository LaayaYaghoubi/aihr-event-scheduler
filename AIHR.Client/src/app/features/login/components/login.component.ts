import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { LoginRequest } from '../models/login-request.model';
import { Router } from '@angular/router';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgIf } from '@angular/common';
import { SignalRService } from '../../../core/services/signalr.service';

@Component({
  selector: 'app-login',
  imports: [MatFormFieldModule, MatInputModule, MatButtonModule,ReactiveFormsModule, MatSnackBarModule, MatProgressSpinnerModule, NgIf],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})

export class LoginComponent implements OnInit {
    form: FormGroup;
    loading: boolean = false;

    constructor(private fb: FormBuilder, private _snackBar: MatSnackBar, private router: Router, private authService: AuthService, private signalRService: SignalRService) {
      this.form = this.fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required]
      })
    }

    ngOnInit(): void {
      this.authService.isLoggedIn();
    }

    credential: LoginRequest ={
      email: '',
      password: ''
    }
  
    login() {
      this.credential.email = this.form.get('email')?.value;
      this.credential.password = this.form.get('password')?.value;
      this.authService.login(this.credential).subscribe({
        next: () => {
          this.fakeLoading();
          this._snackBar.open('Logged in successfully', '', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 1500,
        });
        },
        error: () => this.error(),
        complete: () => {
          this.signalRService.startConnection();
          this.signalRService.addMessageListener();
        }
      });
      
    }

    register() {
      this.credential.email = this.form.get('email')?.value;
      this.credential.password = this.form.get('password')?.value;
      this.authService.register(this.credential).subscribe({
        next: (response) => {
          console.log('Registration successful:', response);
          this.fakeLoading();
          this.login();
          this._snackBar.open('Registered successfully', '', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 1500,
        });
        },
        error: (err) => {
          console.error('Registration error:', err);
          this.error();
        }
      });
    }
  
    error() {
      this._snackBar.open('Incorrect username or password', '', {
        horizontalPosition: 'center',
        verticalPosition: 'bottom',
        duration: 3000,
      });
    }
  
    fakeLoading() {
      this.loading = true;
      setTimeout(() => {
        this.loading = false;
        this.router.navigate(['/dashboard'])
      }, 1000);
    }
  
  }