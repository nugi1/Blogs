import { Component } from '@angular/core';
import {RouterModule, RouterOutlet} from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { SubmitBlogFormComponent } from './submit-blog-form/submit-blog-form.component';
import {CommonModule} from "@angular/common";
import {HTTP_INTERCEPTORS, provideHttpClient} from "@angular/common/http";
import {NavBarComponent} from "./nav-bar/nav-bar.component";
import {RegisterFormComponent} from "./register-form/register-form.component";
import {routes} from "./app.routes";
import {AuthInterceptor} from "./authinterceptor.interceptor";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, CommonModule, ReactiveFormsModule, RouterOutlet, SubmitBlogFormComponent, NavBarComponent, RegisterFormComponent, RouterOutlet, RouterModule],
  providers: [
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Client';
}
