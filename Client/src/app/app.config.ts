import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import {HTTP_INTERCEPTORS, provideHttpClient} from '@angular/common/http'; // ✅ Import provideHttpClient

import { routes } from './app.routes';
import {AuthInterceptor} from "./authinterceptor.interceptor";

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(), // Enable HttpClient service
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor, // Use the custom interceptor for auth tokens
      multi: true, // Ensures you can add multiple interceptors
    }
  ]
};
