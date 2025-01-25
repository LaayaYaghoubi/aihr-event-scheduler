import { Component, ChangeDetectionStrategy, signal, OnInit } from '@angular/core';
import { MatToolbar } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTimepickerModule } from '@angular/material/timepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatButtonModule } from '@angular/material/button';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ScheduledEventService } from '../../core/services/scheduled-event.service';
import { ScheduledEvent } from '../../shared/models/scheduled-event.model';

@Component({
  selector: 'app-create-edit-event',
  imports: [
    MatToolbar,
    MatCardModule,
    MatGridListModule,
    MatFormFieldModule,
    MatInputModule,
    MatTimepickerModule,
    MatDatepickerModule,
    FormsModule,
    MatButtonModule,
    RouterLink,
    ReactiveFormsModule],

  providers: [provideNativeDateAdapter()],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './create-edit-event.component.html',
  styleUrl: './create-edit-event.component.scss'
})
export class CreateEditEventComponent implements OnInit {
  idUser: number | undefined;
  action: string = 'Add';
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private scheduledEventService: ScheduledEventService,
    private router: Router,
    private _snackBar: MatSnackBar,
    private aRoute: ActivatedRoute) {

    this.form = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      startDate: ['', Validators.required],
      startTime: ['', Validators.required],
      endDate: ['', Validators.required],
      endTime: ['', Validators.required],
    })
    this.idUser = this.aRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    if (this.idUser == undefined) {

    }
    else {
      this.action = 'Edit';
      this.getScheduledEvent(this.idUser);
    }
  }


  saveScheduledEvent() {
    const scheduledEvent: ScheduledEvent = {
      id: this.idUser,
      title: this.form.value.title,
      description: this.form.value.description,
      start: new Date(Date.UTC(
        this.form.value.startDate!.getFullYear(),
        this.form.value.startDate!.getMonth(),
        this.form.value.startDate!.getDate(),
        this.form.value.startTime.getHours(),
        this.form.value.startTime.getMinutes(),
        0
      )),
      end: new Date(Date.UTC(
        this.form.value.endDate!.getFullYear(),
        this.form.value.endDate!.getMonth(),
        this.form.value.endDate!.getDate(),
        this.form.value.endTime.getHours(),
        this.form.value.endTime.getMinutes(),
        0
      )),
    };

    if (this.idUser == undefined) {
      this.addScheduledEvent(scheduledEvent);
    }
    else {
      this.editScheduledEvent(scheduledEvent);
    }
  }

  addScheduledEvent(scheduledEvent: ScheduledEvent) {

    this.scheduledEventService.AddScheduledEvent(scheduledEvent).subscribe({
      next: () => {
        this._snackBar.open('The scheduled event was added successfully', '', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 1500,
        });
        this.router.navigate(['/dashboard/events'], { state: { refreshed: true } });
      },
      error: (err) => {
        console.error('Error adding scheduled event', err.message);
        this._snackBar.open(err.message, '', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 2000,
        });
      }
    });;
  }

  editScheduledEvent(scheduledEvent: ScheduledEvent) {

    this.scheduledEventService.editScheduledEvent(scheduledEvent, this.idUser!).subscribe({
      next: () => {
        this._snackBar.open('The scheduled event was edited successfully', '', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 1500,
        });
        this.router.navigate(['/dashboard/events'], { state: { refreshed: true } });
      },
      error: (err) => {
        console.error('Error editing scheduled event', err);
        this._snackBar.open('Error deleting scheduled event', '', {
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          duration: 1500,
        });
      }
    });;
  }

  getScheduledEvent(idUser: number) {
    this.scheduledEventService.getScheduledEvent(idUser).subscribe((scheduledEvent: ScheduledEvent) => {
      this.form.patchValue({
        title: scheduledEvent.title,
        description: scheduledEvent.description,
        startDate: new Date(scheduledEvent.start),
        startTime: new Date(scheduledEvent.start),
        endDate: new Date(scheduledEvent.end),
        endTime: new Date(scheduledEvent.end)
      });
    });
  }
}
