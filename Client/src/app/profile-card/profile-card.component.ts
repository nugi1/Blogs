import {Component, Input} from '@angular/core';
import {Blog} from "../blog-card/models/Blog";
import {DatePipe, NgForOf, NgOptimizedImage} from "@angular/common";
import {RouterLink} from "@angular/router";
import {NavBarComponent} from "../nav-bar/nav-bar.component";
import {PeopleService} from "../services/people.service";

export interface Person{
  id: string;
  avatarUrl: string;
  username: string;
  age: number;
  email: string;
  isSubscribed: boolean;
  blogs: Blog[];
}

@Component({
  selector: 'app-profile-card',
  standalone: true,
  imports: [
    NgOptimizedImage,
    NgForOf,
    DatePipe,
    RouterLink,
    NavBarComponent
  ],
  templateUrl: './profile-card.component.html',
  styleUrl: './profile-card.component.css'
})
export class ProfileCardComponent {
  @Input() person!: Person;

  constructor(private peopleService: PeopleService) {
  }
  subscribe(userId: string) {
    this.peopleService.addFollower(userId).subscribe({
      next: () => {
        this.person.isSubscribed = true;
      },
      error: () => console.error('Subscription failed')
    });
  }
}
