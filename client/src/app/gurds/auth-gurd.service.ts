import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthGurdService implements CanActivate {

  constructor(private jwtHelper: JwtHelperService, private router: Router) { }

  canActivate() {
    const token = localStorage.getItem('real_time_chat_token');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    this.router.navigate(['/']);
    return false;
  }
}
