import {Component, OnInit} from '@angular/core';
import {BlogDetailComponent} from "../blog-detail/blog-detail.component";
import {NavBarComponent} from "../nav-bar/nav-bar.component";
import {AsyncPipe, DatePipe, NgForOf, NgIf, SlicePipe} from "@angular/common";
import {AuthService} from "../sign-in/auth.service";
import {Observable} from "rxjs";
import {FeedService} from "../services/feed.service";
import {FeedBlogComponent} from "../feed-blog/feed-blog.component";
import {RouterLink} from "@angular/router";

export interface Comment {
  author: string;
  text: string;
}

export interface Blog {
  id: string;
  title: string;
  content: string;
  publishedAt: Date;
  username: string;
  comments: Comment[]; // Ensure comments array is included
}

@Component({
  selector: 'app-feed',
  standalone: true,
  imports: [
    BlogDetailComponent,
    NavBarComponent,
    NgIf,
    NgForOf,
    AsyncPipe,
    FeedBlogComponent,
    RouterLink,
    SlicePipe,
    DatePipe
  ],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css'
})
export class FeedComponent implements OnInit{
  blogs$!: Observable<Blog[]>;
  blogs: Blog[] = [];

  constructor(
    private feedService: FeedService,
  ) {
    console.log(this.blogs)
  }

  ngOnInit(): void {
    this.blogs$ = this.feedService.getFollowingBlogs();

    this.feedService.getFollowingBlogs().subscribe(blogs => {
      this.blogs = blogs.map(blog => ({
        ...blog,
        comments: blog.comments ?? [] // Ensure comments is always an array
      }));
      console.log(this.blogs)
    });
  }
}
