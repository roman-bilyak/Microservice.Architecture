import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { merge } from "rxjs";
import { tap } from 'rxjs/operators';
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MovieAPIService } from '../api-services/api-services';
import { MoviesDataSource } from './movies.datasource';


@Component({
  selector: 'movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit, AfterViewInit {
  displayedColumns = ["title"];

  dataSource: MoviesDataSource;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  constructor(private movieAPIService: MovieAPIService) {

  }

  ngOnInit() {
    this.dataSource = new MoviesDataSource(this.movieAPIService);
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
