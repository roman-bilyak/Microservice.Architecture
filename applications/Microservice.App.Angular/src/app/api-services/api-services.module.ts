import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { JwtBearerInterceptor } from './jwt-bearer-interceptor';
import * as APIServices from './api-services';

@NgModule({
  providers: [
    APIServices.MoviesAPIService,
    APIServices.TestsAPIService,
    APIServices.RolesAPIService,
    APIServices.UsersAPIService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtBearerInterceptor, multi: true }
  ]
})
export class APIServicesModule { }
