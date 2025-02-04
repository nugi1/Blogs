import {Component, OnInit} from '@angular/core';
import {Blog} from "../blog-card/models/Blog";
import {AuthService} from "../sign-in/auth.service";
import {BlogService} from "../landing-page/blog.service";
import {Router, RouterModule, RouterOutlet} from "@angular/router";
import {AsyncPipe, DatePipe, NgForOf, NgIf} from "@angular/common";
import {BlogCardComponent} from "../blog-card/blog-card.component";
import {NavBarComponent} from "../nav-bar/nav-bar.component";

@Component({
  selector: 'app-blogs-page',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterModule,
    NgIf,
    NgForOf,
    DatePipe,
    AsyncPipe,
    BlogCardComponent,
    NavBarComponent
  ],
  templateUrl: './blogs-page.component.html',
  styleUrl: './blogs-page.component.css'
})
export class BlogsPageComponent implements OnInit{
  blogs: Blog[] = [];
  isAuthenticated = false;

  constructor(private authService: AuthService, private blogService: BlogService, private router: Router) {}

  ngOnInit() {
    this.isAuthenticated = this.authService.isAuthenticated();
    console.log(this.isAuthenticated);
    this.loadBlogs();
  }

  loadBlogs() {
    this.blogService.getBlogsByPublishDate().subscribe({
      next: (data) => {
        this.blogs = data;
        console.log('Fetched Blogs:', this.blogs);
      },
      error: (err) => console.error('Error fetching blogs:', err)
    });
  }

  navigateToCreateBlog() {
    this.router.navigate(['/post-blog']);
  }
}
