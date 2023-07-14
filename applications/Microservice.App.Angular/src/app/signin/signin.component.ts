import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../core/auth.service';

@Component({
  template: `<p>Processing signin callback</p>`,
})
export class SigninComponent implements OnInit {

  constructor(private readonly _router: Router, private readonly _authService: AuthService) { }

  async ngOnInit() {
    await this._authService.userManager.signinCallback();
    this._router.navigate(['']);
  }
}
