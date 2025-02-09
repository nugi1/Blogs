import { Injectable } from '@angular/core';
import {Person} from "../profile-card/profile-card.component";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PeopleService {
  privateUrl : string = "http://20.82.111.66:5277/"
  constructor(private http: HttpClient) {
  }

  getUsers() {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.get<Person[]>(`${this.privateUrl}api/users`, {headers});
  }

  addFollower(userId: string) {
    const token = localStorage.getItem('authToken'); // Get JWT from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post(`${this.privateUrl}follow/${userId}`, {}, {headers});
  }
}
