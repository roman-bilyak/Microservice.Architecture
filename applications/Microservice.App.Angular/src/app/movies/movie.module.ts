import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { APIServicesModule } from '../api-services/api-services.module';
import { AngularMaterialModule } from '../angular-material.module';

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
    APIServicesModule,
    AngularMaterialModule
  ]
})
export class MovieModule { }
