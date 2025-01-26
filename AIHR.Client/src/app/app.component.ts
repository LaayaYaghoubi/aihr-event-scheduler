import { Component, OnInit } from '@angular/core';
import { SignalRService } from './core/services/signalr.service';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {

  constructor(public signalRService: SignalRService) {}

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addMessageListener();
  }
}
