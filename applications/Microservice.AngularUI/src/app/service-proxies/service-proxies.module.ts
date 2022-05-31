import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { JwtBearerInterceptor } from './jwt-bearer-interceptor';
import * as ServiceProxies from './service-proxies';

@NgModule({
  providers: [
    ServiceProxies.MovieServiceProxy,
    ServiceProxies.ReviewServiceProxy,
    ServiceProxies.TestServiceProxy,
    ServiceProxies.UserServiceProxy,
    { provide: HTTP_INTERCEPTORS, useClass: JwtBearerInterceptor, multi: true }
  ]
})
export class ServiceProxyModule { }
