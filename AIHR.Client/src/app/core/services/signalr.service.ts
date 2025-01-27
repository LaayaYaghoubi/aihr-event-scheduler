import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { NotificationDialogComponent } from '../../shared/components/notifications/notification-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;
  constructor(private dialog: MatDialog){}

notificationHubUrl = environment.notificationHubUrl;

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.notificationHubUrl,{
      logger: signalR.LogLevel.Debug,
      accessTokenFactory: () => localStorage.getItem('accessToken') || ''})
      .withAutomaticReconnect([0, 5000, 10000, 60000, 300000])
      .build();

    this.hubConnection
      .start()
          .then(() => {
        console.log('SignalR Connection started');
      })
      .catch(err => console.log('Error establishing SignalR connection: ' + err));
  }

  public addMessageListener = () => {
    this.hubConnection.on('ReceiveNotification', (message: string, eventId: number) => {
      this.dialog.open(NotificationDialogComponent, {
        data: { message, eventId },
        width: '400px',
      });
      this.acknowledgeNotification(eventId);
    });
  }

  public stopConnection = () => {
    this.hubConnection.stop();
  }

  public acknowledgeNotification( eventId: number) {
    this.hubConnection.invoke('AcknowledgeNotification', eventId)
      .then(() => console.log(`Acknowledgment sent for event ${eventId}`))
      .catch(err => console.error(`Error sending acknowledgment for event ${eventId}: ${err}`));
  }
}
