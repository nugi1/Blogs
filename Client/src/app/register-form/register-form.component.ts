import { Component } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router, RouterModule} from "@angular/router";
import {NgIf} from "@angular/common";
import {RegisterService} from "./register.service";
import {NavBarComponent} from "../nav-bar/nav-bar.component";

@Component({
  selector: 'app-register-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterModule,
    NgIf,
    NavBarComponent
  ],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})
export class RegisterFormComponent {
  registerForm: FormGroup;
  submitted = false;

  constructor(private fb: FormBuilder,
              private registerService: RegisterService,
              private router: Router) {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]],
    }, {validators: this.passwordMatchValidator});
  }

  passwordMatchValidator(formGroup: FormGroup) {
    return formGroup.get('password')?.value === formGroup.get('confirmPassword')?.value
      ? null
      : {mismatch: true};
  }

  onSubmit() {
    this.submitted = true;
    if (this.registerForm.valid) {
      const userData = this.registerForm.value;
      this.registerService.registerUser(userData).subscribe(
        (response) => {
          console.log('Registration Successful:', response);
          this.router.navigate(['/home']).then(r => r.valueOf());
        },
        (error) => {
          console.error('Registration Error:', error);
        }
      );
    }
  }
}
