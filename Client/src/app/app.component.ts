import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { SubmitBlogFormComponent } from './submit-blog-form/submit-blog-form.component';
import {CommonModule} from "@angular/common";
import {provideHttpClient} from "@angular/common/http";
import {NavBarComponent} from "./nav-bar/nav-bar.component";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, ReactiveFormsModule, RouterOutlet, SubmitBlogFormComponent, NavBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Client';
}
