import {Component, OnInit} from '@angular/core';
import {NgForOf, NgOptimizedImage, SlicePipe} from "@angular/common";
import {ActivatedRoute, RouterLink} from "@angular/router";
import {NavBarComponent} from "../nav-bar/nav-bar.component";
import {HttpClient, HttpHeaders} from "@angular/common/http";

export interface Profile{
  id: string,
  avatar: string,
  name: string,
  age: number,
  email: string
}

export interface Friend{
  id: string,
  avatar: string,
  name: string,
  email: string
}

export interface Blog{
  id: string,
  title: string,
  content: string
}

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    NgForOf,
    RouterLink,
    NgOptimizedImage,
    SlicePipe,
    NavBarComponent
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit{
  userProfile!: Profile;
  friends: Friend[] = [];
  blogs: Blog[] = []

  constructor(private http: HttpClient, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const userId = params['id'];

      if (userId) {
        this.getUserProfile(userId);
      } else {
        this.getCurrentUserProfile();
      }
    });
  }

  getUserProfile(userId: string) {
    let url = "http://localhost:5277/profile";
    this.http.get<Profile>(`${url}/${userId}`).subscribe(profile => {
      this.userProfile = profile;
      console.log(this.userProfile);
      this.loadFriendsAndBlogs(userId);
    });
  }

  getCurrentUserProfile() {
    const token = localStorage.getItem('authToken'); // Get JWT from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    let url = "http://localhost:5277/profile"
    this.http.get<Profile>(url, {headers}).subscribe(profile => {
      this.userProfile = profile;
      this.loadFriendsAndBlogs(profile.id);
    });
  }

  loadFriendsAndBlogs(userId: string) {
    let url = "http://localhost:5277/profile";
    this.http.get<Friend[]>(`${url}/friends/${userId}`).subscribe(friends => {
      this.friends = friends;
    });
    this.http.get<Blog[]>(`${url}/blogs/${userId}`).subscribe(blogs => {
      this.blogs = blogs;
    });
  }
}
