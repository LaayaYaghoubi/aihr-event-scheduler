import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MatToolbar } from '@angular/material/toolbar';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ScheduledEvent } from '../../../domain/models/ScheduledEvent.model';
import { MatIcon } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatLabel } from '@angular/material/form-field';
import { MatFormField } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { NgIf } from '@angular/common';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { PaginatedResponse } from '../../../domain/models/PaginatedResponse.model';
import { ScheduledEventService } from '../../../domain/services/scheduled-event.service';

@Component({
  selector: 'app-events',
  imports: [
    RouterOutlet,
    MatToolbar,
    MatTableModule,
    MatIcon,
    MatTooltipModule,
    MatLabel,
    MatFormField,
    MatInputModule,
    MatPaginatorModule,
    MatSortModule,
    NgIf,
    MatSnackBarModule,
    MatButtonModule,
    RouterLink],
  templateUrl: './events.component.html',
  styleUrl: './events.component.scss'
})

export class EventsComponent implements AfterViewInit, OnInit {
  announceSortChange(sortState: Sort) {
    this.currentSortOrder = sortState.direction === 'asc' ? 1 : 2;
    this.getScheduledEvents();
  }

  displayedColumns: string[] = ['title', 'description', 'start', 'end', 'actions'];
  dataSource = new MatTableDataSource<ScheduledEvent>();
  totalCount = 0;
  pageSize = 5;
  currentPage = 0;
  currentSortOrder: number = 1;

  constructor(
    private scheduledEventService: ScheduledEventService,
    private _snackBar: MatSnackBar) { }

  ngOnInit() {
    this.getScheduledEvents();
  }

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getScheduledEvents(): void {
    this.scheduledEventService.getScheduledEvents(
      this.currentPage + 1,
      this.pageSize,
      this.currentSortOrder
    ).subscribe({
      next: (data: PaginatedResponse<ScheduledEvent>) => {
        this.dataSource.data = data.items; 
        this.totalCount = data.totalCount; 
      },
      error: (err) => {
        console.error('Error fetching scheduled events', err);
      },
      complete: () => {
        console.log('Fetching scheduled events completed successfully.');
      }
    });
  }

  onPageChange(event: any): void {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.getScheduledEvents();
  }

  deleteScheduledEvent(id: number) {
    this.scheduledEventService.deleteScheduledEvent(id).subscribe({
      next: () => {
      this.getScheduledEvents();
      this._snackBar.open('The scheduled event was deleted successfully', '', {
        horizontalPosition: 'center',
        verticalPosition: 'bottom',
        duration: 1500,
      });
      },
      error: (err) => {
      console.error('Error deleting scheduled event', err);
      this._snackBar.open('Error deleting scheduled event', '', {
        horizontalPosition: 'center',
        verticalPosition: 'bottom',
        duration: 1500,
      });
      }
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
