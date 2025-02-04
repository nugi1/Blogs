import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Blog } from '../blog-card/models/Blog';
import { Observable } from 'rxjs';
import {BlogService} from "../landing-page/blog.service";
import {AsyncPipe, DatePipe, NgForOf, NgIf} from "@angular/common";
import {NavBarComponent} from "../nav-bar/nav-bar.component";
import {AuthService} from "../sign-in/auth.service";
import {FormsModule} from "@angular/forms";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-blog-detail',
  standalone: true,
  imports: [
    AsyncPipe,
    DatePipe,
    NgIf,
    NavBarComponent,
    FormsModule,
    NgForOf
  ],
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css']
})
export class BlogDetailComponent implements OnInit {
  blog$!: Observable<Blog>;  // Observable to hold the blog data
  isAuthenticated!: boolean;
  protected newComment: any;
  blogId: string | null = ' ';
  comments: {author: string, text: string}[] = [];
  private apiUrl: string = "http://localhost:5277";

  constructor(
    private blogService: BlogService,
    private route: ActivatedRoute,
    private authService: AuthService,
    private http: HttpClient
  ) {
    this.isAuthenticated = this.authService.isAuthenticated();
  }

  ngOnInit(): void {
    this.isAuthenticated = this.authService.isAuthenticated();
    this.blogId = this.route.snapshot.paramMap.get('id');
    if (this.blogId) {
      this.blog$ = this.blogService.getBlogById(this.blogId);
    }
    this.loadComments();
  }
  loadComments(): void {
    this.http.get<{author: string, text: string}[]>(`${this.apiUrl}/comment/${this.blogId}`).subscribe({
      next: (data) => {
        this.comments = data;
        console.log('Fetched comments:', this.comments);
      },
      error: (err) => {
        console.error('Error fetching comments:', err);
      }
    });
  }
  postComment() {
    if (!this.newComment.trim()) return; // Prevent empty comments

    const token = localStorage.getItem('authToken');
    if (!token) {
      console.error('No token found');
      return;
    }

    const comment = {
      text: this.newComment,
      author: 'Current User' // Replace with actual user data
    };

    // Make API call to post comment
    this.http.post(`${this.apiUrl}/comment/${this.blogId}`, comment, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).subscribe({
      next: (data) => {
        console.log('Comment added:', data);
      },
      error: (err) => {
        console.error('Error adding comment:', err);
      }
    });

    console.log(this.newComment);
  }
}
