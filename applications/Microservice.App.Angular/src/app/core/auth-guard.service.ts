import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private authService: AuthService) { }

  async canActivate(): Promise<boolean> {
    try {
      if (!await this.authService.isAuthenticated()) {
        await this.authService.login();
        return true;
      }
      return true;
    } catch {
      return false;
    }
  }
}
