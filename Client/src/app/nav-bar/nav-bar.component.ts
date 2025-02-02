import {Component, OnDestroy, OnInit} from '@angular/core';
import {RouterModule, RouterOutlet} from "@angular/router";
import {AuthService} from "../sign-in/auth.service";
import {NgIf} from "@angular/common";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [RouterOutlet, RouterModule, NgIf],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent implements OnInit, OnDestroy{
  isAuthenticated = false;
  private authStatusSubscription!: Subscription;


  constructor(private authService: AuthService) {
  }

  ngOnInit(): void {
    this.isAuthenticated = this.authService.isAuthenticated();
  }

  ngOnDestroy(): void {
    if (this.authStatusSubscription) {
      this.authStatusSubscription.unsubscribe();
    }
  }

  signOut(): void {
    this.authService.signOut();
  }
}
