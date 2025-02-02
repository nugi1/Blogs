import { Component } from '@angular/core';
import {Post} from "./models/Post";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {PostService} from "./post.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-submit-blog-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './submit-blog-form.component.html',
  styleUrl: './submit-blog-form.component.css'
})
export class SubmitBlogFormComponent {
  postForm: FormGroup;

  constructor(private fb: FormBuilder, private postService: PostService, private router: Router) {
    this.postForm = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.postForm.valid) {
      const newPost: Post = this.postForm.value;
      this.postService.createPost(newPost).subscribe(response => {
        console.log('Post created successfully', response);
      });
      this.router.navigate(['/blogs']);
    }
  }
}
