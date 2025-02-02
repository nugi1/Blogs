import { Component } from '@angular/core';
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {NavBarComponent} from "../nav-bar/nav-bar.component";
import {BlogCardComponent} from "../blog-card/blog-card.component";
import {Blog} from "../blog-card/models/Blog";
import {BlogService} from "./blog.service";
import {Observable} from "rxjs";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [
    NgOptimizedImage,
    NavBarComponent,
    BlogCardComponent,
    CommonModule,
    RouterLink
  ],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.css'
})
export class LandingPageComponent {
  blogs$!: Observable<Blog[]>;
  constructor(private blogService: BlogService) {
  }

  ngOnInit(): void {
    this.blogs$ = this.blogService.getBlogs();
  }
}
