import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { JwtBearerInterceptor } from './jwt-bearer-interceptor';
import { HttpClientModule } from "@angular/common/http";
import { environment } from '../../environments/environment';
import * as APIServices from './api-services';

@NgModule({
  providers: [
    { provide: APIServices.API_BASE_URL, useValue: environment.baseUrl },
    { provide: HTTP_INTERCEPTORS, useClass: JwtBearerInterceptor, multi: true },
    APIServices.MoviesAPIService,
    APIServices.TestsAPIService,
    APIServices.RolesAPIService,
    APIServices.UsersAPIService,
  ],
  imports: [
    HttpClientModule
  ],
})
export class APIServicesModule { }
