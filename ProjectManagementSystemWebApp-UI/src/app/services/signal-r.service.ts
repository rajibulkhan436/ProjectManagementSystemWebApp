import { Injectable } from '@angular/core';
import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
} from '@microsoft/signalr';
import { Environment } from '../../environments/environment';
import { SessionHelperService } from '../core/session-helper.service';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private hubConnection!: HubConnection;

  constructor(private readonly _sessionService: SessionHelperService) {
    this.initiateConnection();
  }

  initiateConnection(): void {
    const token: string | null = this._sessionService.get('token');

    if (!token) {
      console.error('No token found. Cannot establish SignalR connection.');
      return;
    }

    // Avoid reinitializing if connection already exists
    if (this.hubConnection && this.hubConnection.state !== 'Disconnected') {
      console.warn('SignalR connection already exists or is in progress.');
      return;
    }

    this.hubConnection = new HubConnectionBuilder()
      .withUrl(Environment.WebSocketUrl, {
        accessTokenFactory: () => token,
        transport: HttpTransportType.WebSockets,
        skipNegotiation: true,
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('SignalR Connected:', this.hubConnection.connectionId);
        this.registerMessageHandlers();
      })
      .catch((error: Error) => {
        console.error('SignalR Connection Error:', error);
      });
  }

  private registerMessageHandlers(): void {
    if (!this.hubConnection) return;

    this.hubConnection.on('ReceiveMessage', (message: string): void => {
      console.log('Notification received:', message);
      this.handleCallback(message);
    });

    this.hubConnection.on(
      'ReceivedMessage',
      (user: string, message: string): void => {
        console.log(`User: ${user} | Message: ${message}`);
      }
    );
  }

  private handleCallback: (message: string) => void = () => {};

  broadcastMessage(callback: (message: string) => void): void {
    this.handleCallback = callback;
  }

  sendMessage(message: string): void {
    if (!this.hubConnection || this.hubConnection.state !== 'Connected') {
      console.warn('Cannot send message. SignalR is not connected.');
      return;
    }

    this.hubConnection
      .invoke('BroadcastNotification', message)
      .catch((err) => console.error('Send Message Error:', err));
  }

  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop().then(() => {
        console.log('SignalR Disconnected.');
      });
    }
  }
}
