import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, pipe } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { LoginRequest } from '../../features/login/models/login-request.model';
import { LoginResponse } from '../../features/login/models/login-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  private apiUrl = environment.apiUrl;

  login(credential: LoginRequest): Observable<LoginResponse> 
  {
    return this.httpClient.post<LoginResponse>(`${this.apiUrl}/login`, credential).
    pipe(map(response => {

      localStorage.setItem('accessToken', response.accessToken);
      document.cookie = `refreshToken=${response.accessToken}`;
      return response;
    }))
  }

  register(credential: LoginRequest): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(`${this.apiUrl}/register`, credential).pipe(
      map(response => {
        console.log('Register response:', response);
        return response;
      })
    );
  }

  refreshToken(): Observable<LoginResponse> {
    const refreshToken = this.getRefreshTokenFromCookie();
    return this.httpClient.post<LoginResponse>(`${this.apiUrl}/refresh`, refreshToken)
      .pipe(map(response => {
        localStorage.setItem('accessToken', response.accessToken);
        document.cookie = `refreshToken=${response.refreshToken}`;
        return response;
      }))
  }

private getRefreshTokenFromCookie(): string | null {
  const cookieString = document.cookie;
  const cookieArray = cookieString.split(';');
  for (const cookie of cookieArray) {
    const [name, value] = cookie.split('=');
    if (name.trim() === 'refreshToken') {
      return value;
    }
  }
  return null;
}

logout(): void {
  localStorage.removeItem('accessToken');
}

isLoggedIn(): boolean {
  return localStorage.getItem('accessToken') !== null;
}

}