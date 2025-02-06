import {Component, Input} from '@angular/core';
import {Blog} from "../blog-card/models/Blog";
import {AsyncPipe, DatePipe, NgForOf, NgIf, SlicePipe} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {NavBarComponent} from "../nav-bar/nav-bar.component";
import {HttpClient} from "@angular/common/http";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-feed-blog',
  standalone: true,
  imports: [
    AsyncPipe,
    DatePipe,
    FormsModule,
    NavBarComponent,
    NgForOf,
    NgIf,
    RouterLink,
    SlicePipe
  ],
  templateUrl: './feed-blog.component.html',
  styleUrl: './feed-blog.component.css'
})
export class FeedBlogComponent {
  @Input() blog!: Blog;
  protected newComment: any;
  private apiUrl: string = "http://localhost:5277";

  constructor(private http: HttpClient) {
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
    this.http.post(`${this.apiUrl}/comment/${this.blog.id}`, comment, {
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
