import { Injectable } from '@angular/core';
import {Blog} from "../blog-card/models/Blog";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {map} from "rxjs/operators";
import {tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class FeedService {
  private apiUrl = 'http://localhost:5277/feed';

  constructor(private http: HttpClient) { }

  getFollowingBlogs() {
    const token = localStorage.getItem('authToken'); // Get JWT from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<any[]>(`${this.apiUrl}`, {headers}).pipe(
      tap(response => console.log("Raw API Response:", response)), // Debug
      map(blogs => blogs.map(blog => ({
        id: blog.id,
        title: blog.title,
        content: blog.content,
        publishedAt: new Date(blog.publishedAt),
        username: blog.username,
        comments: blog.comments ?? [] // Ensure it’s always an array
      })))
    );
  }
}
