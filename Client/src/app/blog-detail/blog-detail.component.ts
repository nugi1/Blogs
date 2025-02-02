import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Blog } from '../blog-card/models/Blog';
import { Observable } from 'rxjs';
import {BlogService} from "../landing-page/blog.service";
import {AsyncPipe, DatePipe, NgIf} from "@angular/common";
import {NavBarComponent} from "../nav-bar/nav-bar.component";
import {AuthService} from "../sign-in/auth.service";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-blog-detail',
  standalone: true,
  imports: [
    AsyncPipe,
    DatePipe,
    NgIf,
    NavBarComponent,
    FormsModule
  ],
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css']
})
export class BlogDetailComponent implements OnInit {
  blog$!: Observable<Blog>;  // Observable to hold the blog data
  isAuthenticated!: boolean;
  protected newComment: any;

  constructor(
    private blogService: BlogService,
    private route: ActivatedRoute,
    private authService: AuthService
  ) {
    this.isAuthenticated = this.authService.isAuthenticated();
  }

  ngOnInit(): void {
    this.isAuthenticated = this.authService.isAuthenticated();
    const blogId = this.route.snapshot.paramMap.get('id');
    if (blogId) {
      this.blog$ = this.blogService.getBlogById(blogId);
    }
  }
  postComment() {
    if (!this.newComment.trim()) return; // Prevent empty comments

    const comment = {
      text: this.newComment,
      author: 'Current User' // Replace with actual user
    };

    console.log(this.newComment);
  }
}
