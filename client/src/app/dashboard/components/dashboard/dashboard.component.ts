import { Component, OnInit } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  hubConnection;
  constructor() { }

  ngOnInit(): void {
    this.startConnection();
  }
  private startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl(`https://localhost:44381/message?userId=12324`)
                            .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

}
