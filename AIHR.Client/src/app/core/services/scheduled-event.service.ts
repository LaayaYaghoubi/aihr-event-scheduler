import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import { PaginatedResponse } from '../../shared/models/paginated-response.model';
import { ScheduledEvent } from '../../shared/models/scheduled-event.model';

@Injectable({
  providedIn: 'root'
})
export class ScheduledEventService {

  constructor(
    private httpClient: HttpClient) { }
  private apiUrl = environment.apiUrl + '/api/v1/scheduled-event';


  getScheduledEvents(
    page: number,
     pageSize: number,
     sortOrder: number): Observable<PaginatedResponse<ScheduledEvent>> {
    const params = { Page: page.toString(), PageSize: pageSize.toString(),sortOrder : sortOrder.toString()  };
    return this.httpClient.get<PaginatedResponse<ScheduledEvent>>(this.apiUrl, { params });
  }
  deleteScheduledEvent(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiUrl}/${id}`);
  }

  AddScheduledEvent(event: ScheduledEvent): Observable<ScheduledEvent> {
   return this.httpClient.post<ScheduledEvent>(this.apiUrl, event);
  }

  getScheduledEvent(id: number): Observable<ScheduledEvent> {
    return this.httpClient.get<ScheduledEvent>(`${this.apiUrl}/${id}`);
  }

  editScheduledEvent(event: ScheduledEvent, id: number): Observable<void> {
    return this.httpClient.put<void>(`${this.apiUrl}/${id}`, event);
  }
}