import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  private apiUrl = 'http://20.82.111.66:5277/api/users'

  constructor(private http: HttpClient) {}

  registerUser(userData: { email: string; password: string; username: string }): Observable<any> {
    return this.http.post<any>(this.apiUrl, userData);
  }
}
