import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Blog } from '../blog-card/models/Blog';
import { Observable } from 'rxjs';
import {BlogService} from "../landing-page/blog.service";
import {AsyncPipe, DatePipe} from "@angular/common";

@Component({
  selector: 'app-blog-detail',
  standalone: true,
  imports: [
    AsyncPipe,
    DatePipe
  ],
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css']
})
export class BlogDetailComponent implements OnInit {
  blog$!: Observable<Blog>;  // Observable to hold the blog data

  constructor(
    private blogService: BlogService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const blogId = this.route.snapshot.paramMap.get('id'); // Get the blog ID from the route parameter
    if (blogId) {
      this.blog$ = this.blogService.getBlogById(blogId); // Fetch the blog data by ID
    }
  }
}
