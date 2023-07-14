import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../core/core.module';

import { APIServicesModule } from '../api-services/api-services.module';
import { AngularMaterialModule } from '../angular-material.module';
import { MovieRoutingModule } from './movie-routing.module';

import { MovieListComponent } from './movie-list/movie-list.component';
import { MovieDetailComponent } from './movie-detail/movie-detail.component';
import { MovieCreateComponent } from './movie-create/movie-create.component';
import { MovieEditComponent } from './movie-edit/movie-edit.component';

@NgModule({
  declarations: [
    MovieListComponent,
    MovieDetailComponent,
    MovieCreateComponent,
    MovieEditComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    APIServicesModule,
    AngularMaterialModule,
    MovieRoutingModule
  ]
})
export class MovieModule { }
