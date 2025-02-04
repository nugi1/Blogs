import { Component } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {Router, RouterModule} from '@angular/router';
import {NgIf} from "@angular/common";
import {AuthService} from "./auth.service";
import {NavBarComponent} from "../nav-bar/nav-bar.component";

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, NgIf, NavBarComponent],
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {
  signInForm!: FormGroup;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.signInForm = this.formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit() {
    this.submitted = true;

    if (this.signInForm.valid) {
      const { username, password } = this.signInForm.value;
      this.authService.signIn(username, password).subscribe((success) => {
        if (success) {
          console.log('User Signed In');
          this.router.navigate(['/home']).then();
        } else {
          console.log('Invalid credentials');
        }
      });
    }
  }
}
