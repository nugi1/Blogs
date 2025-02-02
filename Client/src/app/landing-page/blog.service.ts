import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Blog} from "../blog-card/models/Blog";

@Injectable({
  providedIn: 'root'
})
export class BlogService {
  private apiUrl = 'http://localhost:5277/api/blogs';

  constructor(private http: HttpClient) { }

  getBlogsByPublishDate(): Observable<Blog[]> {
    return this.http.get<Blog[]>(`${this.apiUrl}`);
  }

  getBlogs(): Observable<Blog[]> {
    return this.http.get<Blog[]>(this.apiUrl);
  }

  getBlogById(blogId: string) {
    return this.http.get<Blog>(`${this.apiUrl}/${blogId}`);
  }
}
