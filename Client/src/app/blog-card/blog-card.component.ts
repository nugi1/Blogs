import {Component, Input} from '@angular/core';
import {Blog} from "./models/Blog";
import {DatePipe, SlicePipe} from "@angular/common";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-blog-card',
  standalone: true,
  imports: [
    SlicePipe,
    DatePipe,
    RouterLink
  ],
  templateUrl: './blog-card.component.html',
  styleUrl: './blog-card.component.css'
})
export class BlogCardComponent {
  @Input() blog!: Blog;
}
