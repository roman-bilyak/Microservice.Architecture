import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MovieListComponent } from './movie-list/movie-list.component';
import { MovieDetailComponent } from './movie-detail/movie-detail.component';
import { MovieCreateComponent } from './movie-create/movie-create.component';
import { MovieEditComponent } from './movie-edit/movie-edit.component';
import { AuthGuardService } from '../core/auth-guard.service';

const routes: Routes = [
  { path: '', component: MovieListComponent, canActivate: [AuthGuardService] },
  { path: ':id', component: MovieDetailComponent, canActivate: [AuthGuardService] },
  { path: 'create', component: MovieCreateComponent, canActivate: [AuthGuardService] },
  { path: ':id/edit', component: MovieEditComponent, canActivate: [AuthGuardService] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MovieRoutingModule {
}
