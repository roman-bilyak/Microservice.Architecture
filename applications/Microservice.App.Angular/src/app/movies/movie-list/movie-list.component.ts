import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { merge } from "rxjs";
import { tap } from 'rxjs/operators';
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MoviesAPIService } from '../../api-services/api-services';
import { MovieListDataSource } from './movie-list.datasource';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit, AfterViewInit {

  displayedColumns = ["title"];

  dataSource: MovieListDataSource;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  constructor(private moviesAPIService: MoviesAPIService) {

  }

  ngOnInit() {
    this.dataSource = new MovieListDataSource(this.moviesAPIService);
    this.dataSource.load(0, 5);
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(tap(() => this.load()))
      .subscribe();
  }

  load() {
    this.dataSource.load(this.paginator.pageIndex, this.paginator.pageSize);
  }
}
