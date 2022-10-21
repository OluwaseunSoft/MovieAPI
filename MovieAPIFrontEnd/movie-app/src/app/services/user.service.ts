import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginRequest } from '../requests/login-request';
import { RegisterRequest } from '../requests/register-request';
import { TokenResponse } from '../responses/token-response';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  login(loginRequest: LoginRequest): Observable<TokenResponse> {

    return this.httpClient.post<TokenResponse>(`${environment.apiUrl}/Authenticate/login`, loginRequest);
  }

  signup(RegisterRequest: RegisterRequest) {
    return this.httpClient.post(`${environment.apiUrl}/Authenticate/register`, RegisterRequest);   }

    saveSession(tokenResponse: TokenResponse) {


      window.localStorage.setItem('AT', tokenResponse.token);  
  
      window.localStorage.setItem('RT', tokenResponse.expiration);  
  
    }

    getSession(): TokenResponse | null {

      if (window.localStorage.getItem('AT')) {  
  
        const tokenResponse: TokenResponse = {  
  
          token: window.localStorage.getItem('AT') || '',  
  
          expiration: window.localStorage.getItem('RT') || ''    
        }; 
  
        return tokenResponse;  
      }
  
      return null;  
    }

    logout() {
      window.localStorage.clear();  
    }

    isLoggedIn(): boolean {

      let session = this.getSession(); 
  
      if (!session) {
        return false;
      }

    const jwtToken = JSON.parse(window.atob(session.token.split('.')[1]));

    const tokenExpired = Date.now() > (jwtToken.exp * 1000);
    return !tokenExpired;
    }

  }
