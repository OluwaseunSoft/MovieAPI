import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'movie-app';
  isLoggedIn = false;
  constructor(private tokenService: UserService, private router: Router) { }

  ngOnInit() {
    this.isLoggedIn = this.tokenService.isLoggedIn();
  }

  logout(): void {

    this.tokenService.logout();
    this.isLoggedIn = false;
    this.router.navigate(['login']);
    return;
  }

}
