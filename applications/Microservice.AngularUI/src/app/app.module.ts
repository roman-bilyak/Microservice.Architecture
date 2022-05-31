import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';

import { API_BASE_URL } from './service-proxies/service-proxies';
import { environment } from '../environments/environment';
import { ServiceProxyModule } from './service-proxies/service-proxies.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    ServiceProxyModule
  ],
  providers: [
    { provide: API_BASE_URL, useValue: environment.baseUrl}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
