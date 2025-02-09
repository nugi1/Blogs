import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';
import {SignInUserDto} from "./dto";

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  private apiUrl = 'http://20.82.111.66:5277/api/users/sign-in';

  constructor(private http: HttpClient) {
    this.checkAuthentication();
  }

  private checkAuthentication() {
    const token = localStorage.getItem('authToken');
    if (token) {
      this.isAuthenticatedSubject.next(true);
    } else {
      this.isAuthenticatedSubject.next(false);
    }
  }

  signIn(username: string, password: string) {
    const signInPayload: SignInUserDto = { username: username, password };

    console.log("Sending sign-in request with:", signInPayload);
    return this.http.post<{ success: boolean, token?: string }>(this.apiUrl, signInPayload).pipe(
      map((response) => {
        console.log("Backend Response: ", response);
        if (response.success && response.token) {
          localStorage.setItem("authToken", response.token);
          this.isAuthenticatedSubject.next(true);
          return true;
        }
        this.isAuthenticatedSubject.next(false);
        return false;
      }),
      catchError((error) => {
        console.error('Sign-in failed', error);
        this.isAuthenticatedSubject.next(false);
        return of(false);
      })
    );
  }

  signOut(): void {
    localStorage.removeItem('authToken');
    this.isAuthenticatedSubject.next(false);
  }

  getAuthStatus(): boolean {
    return this.isAuthenticatedSubject.value;
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('authToken');
    return !!token;
  }

  storeToken(token: string) {
    localStorage.setItem('authToken', token);
  }

  getToken(): string | null {
    return localStorage.getItem('authToken');
  }
}
