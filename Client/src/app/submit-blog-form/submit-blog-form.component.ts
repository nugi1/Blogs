import { Component } from '@angular/core';
import {Post} from "./models/Post";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {PostService} from "./post.service";

@Component({
  selector: 'app-submit-blog-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './submit-blog-form.component.html',
  styleUrl: './submit-blog-form.component.css'
})
export class SubmitBlogFormComponent {
  postForm: FormGroup;

  constructor(private fb: FormBuilder, private postService: PostService) {
    this.postForm = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required],
      publishedAt: [new Date(), Validators.required]
    });
  }

  onSubmit() {
    if (this.postForm.valid) {
      const newPost: Post = this.postForm.value;
      this.postService.createPost(newPost).subscribe(response => {
        console.log('Post created successfully', response);
      });
    }
  }
}
