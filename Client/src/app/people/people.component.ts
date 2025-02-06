import {Component, OnInit} from '@angular/core';
import {NavBarComponent} from "../nav-bar/nav-bar.component";
import {Person, ProfileCardComponent} from "../profile-card/profile-card.component";
import {NgForOf, NgIf} from "@angular/common";
import {PeopleService} from "../services/people.service";

@Component({
  selector: 'app-people',
  standalone: true,
  imports: [
    NavBarComponent,
    ProfileCardComponent,
    NgForOf,
    NgIf
  ],
  templateUrl: './people.component.html',
  styleUrl: './people.component.css'
})
export class PeopleComponent implements OnInit {
  people!: Person[];

  constructor(private peopleService: PeopleService) {
  }

  ngOnInit() {
    this.getUserData();
  }

  private getUserData() {

    this.peopleService.getUsers().subscribe(
      (data) => {
        this.people = data;
        console.log(data);
      },
      (error) => {
        console.log("Error");
      }
    );
  }

  subscribe(userId: string) {
    console.log(userId);
    this.peopleService.addFollower(userId).subscribe({
      next: () => alert('Subscribed successfully!'),
      error: () => console.error('Subscription failed')
    });
  }
}
