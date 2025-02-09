import { Injectable } from '@angular/core';
import {Post} from "./models/Post";
import {Observable} from "rxjs";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private apiUrl = 'http://20.82.111.66:5277/api/blogs';

  constructor(private http: HttpClient) {}

  createPost(post: Post): Observable<Post> {
    const token = localStorage.getItem('authToken'); // Get JWT from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.post<Post>(this.apiUrl, post, {headers});
  }
}
