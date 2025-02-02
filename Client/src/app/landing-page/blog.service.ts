import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Blog} from "../blog-card/models/Blog";

@Injectable({
  providedIn: 'root'
})
export class BlogService {
  private apiUrl = 'https://your-api-url.com/api/blogs';

  constructor(private http: HttpClient) { }

  getBlogs(): Observable<Blog[]> {
    return this.http.get<Blog[]>(this.apiUrl);
  }

  getBlogById(blogId: string) {
    return this.http.get<Blog>(`${this.apiUrl}/${blogId}`);
  }
}
