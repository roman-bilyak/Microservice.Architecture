import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { from, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { AuthService } from '../core/auth.service';

@Injectable()
export class JwtBearerInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return from(this.authService.getUser())
      .pipe(
        switchMap(user => {

          request = request.clone({
            setHeaders: {
              Authorization: `Bearer ${user.access_token}`
            }
          });

          return next.handle(request);
        })
      );
  }
}
