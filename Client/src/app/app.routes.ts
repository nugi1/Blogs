import { Routes } from '@angular/router';
import {SubmitBlogFormComponent} from "./submit-blog-form/submit-blog-form.component";
import {LandingPageComponent} from "./landing-page/landing-page.component";
import {BlogDetailComponent} from "./blog-detail/blog-detail.component";

export const routes: Routes = [
  {path:"home", component: LandingPageComponent},
  {path: "post-blog", component: SubmitBlogFormComponent},
  {path: 'blog/:id', component: BlogDetailComponent}
];
