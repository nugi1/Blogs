import { Routes } from '@angular/router';
import {SubmitBlogFormComponent} from "./submit-blog-form/submit-blog-form.component";
import {LandingPageComponent} from "./landing-page/landing-page.component";
import {BlogDetailComponent} from "./blog-detail/blog-detail.component";
import {RegisterFormComponent} from "./register-form/register-form.component";
import {SignInComponent} from "./sign-in/sign-in.component";
import {BlogsPageComponent} from "./blogs-page/blogs-page.component";

export const routes: Routes = [
  {path:"home", component: LandingPageComponent},
  {path:'sign-in', component: SignInComponent},
  {path:'blogs', component: BlogsPageComponent},
  {path: 'sign-up', component: RegisterFormComponent},
  {path: "post-blog", component: SubmitBlogFormComponent},
  {path: 'blog/:id', component: BlogDetailComponent}
];
