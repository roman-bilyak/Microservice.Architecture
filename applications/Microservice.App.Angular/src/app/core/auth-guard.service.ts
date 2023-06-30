import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private authService: AuthService) { }

  canActivate(): Promise<boolean> {
    return this.authService.isAuthenticated().then((isAuthenticated) => {
      if (isAuthenticated) {
        return true;
      }
      return this.authService.login().then(() => {
        return true;
      }).catch(() => {
        return false;
      });
    }).catch(() => {
      return false;
    });
  }
}
