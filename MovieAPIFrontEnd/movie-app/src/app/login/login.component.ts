import { Component, NgModule, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginRequest } from '../requests/login-request';
import { ErrorResponse } from '../responses/error-response';
import { UserService } from '../services/user.service';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})


export class LoginComponent implements OnInit {

  constructor(private userService: UserService, private router: Router) { }

  loginRequest: LoginRequest = {
    username: "",
    password: ""
  };

  isLoggedIn = false;
  isLoginFailed = false;
  error: ErrorResponse = { error: '', errorCode: '' };

  ngOnInit(): void {
    let isLoggedIn = this.userService.isLoggedIn();


    console.log(`isLoggedIn: ${isLoggedIn}`);
    if (isLoggedIn) {
      this.isLoggedIn = true;
      this.router.navigate(['tasks']);
  }
}

onSubmit(): void {
  this.userService.login(this.loginRequest).subscribe({
    next: (data => {
      console.debug(`logged in successfully ${data}`);

      this.userService.saveSession(data);
      this.isLoggedIn = true;
      this.isLoginFailed = false;
      this.reloadPage();

    }),
    error: ((error: ErrorResponse) => {
      this.error = error;
      this.isLoggedIn = false;
      this.isLoginFailed = true;
    })
  });
}
reloadPage(): void {
  window.location.reload();
}

}


