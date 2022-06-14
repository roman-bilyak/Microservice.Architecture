import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { JwtBearerInterceptor } from './jwt-bearer-interceptor';
import * as APIServices from './api-services';

@NgModule({
  providers: [
    APIServices.MovieAPIService,
    APIServices.ReviewAPIService,
    APIServices.TestAPIService,
    APIServices.UserAPIService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtBearerInterceptor, multi: true }
  ]
})
export class APIServicesModule { }
