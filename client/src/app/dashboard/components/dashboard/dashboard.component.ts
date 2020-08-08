import { Component, OnInit } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { TokenProviderService } from '../../../shared/services/token-provider.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  hubConnection;
  constructor(private tokenProviderService: TokenProviderService) { }

  ngOnInit(): void {
    this.startConnection();
  }
  private startConnection() {
    const token = this.tokenProviderService.getToken();
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl(`https://localhost:44381/hub/message`, {
                              accessTokenFactory: () => token})
                            .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

}
