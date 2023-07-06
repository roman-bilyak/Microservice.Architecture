import { Injectable } from '@angular/core';
import { User, UserManager } from 'oidc-client-ts';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  userManager: UserManager;

  constructor() {
    const settings = {
      authority: environment.authUrl,
      client_id: environment.authClientId,
      redirect_uri: `${environment.baseUrl}/signin`,
      silent_redirect_uri: `${environment.baseUrl}/silent-callback.html`,
      post_logout_redirect_uri: `${environment.baseUrl}`,
      response_type: 'code',
      scope: environment.authClientScope
    };
    this.userManager = new UserManager(settings);
  }

  public isAuthenticated(): Promise<boolean> {
    return this.getUser().then(user => {
      return user && !user.expired;
    });
  }

  public getUser(): Promise<User> {
    return this.userManager.getUser();
  }

  public login(): Promise<void> {
    return this.userManager.signinRedirect();
  }

  public renewToken(): Promise<User> {
    return this.userManager.signinSilent();
  }

  public logout(): Promise<void> {
    return this.userManager.signoutRedirect();
  }
}
