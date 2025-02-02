import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));

import { provideRouter } from '@angular/router';
import {routes} from "./app/app.routes";

provideRouter(routes)  // Register the routes here
