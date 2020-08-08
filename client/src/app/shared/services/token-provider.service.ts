import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenProviderService {

  constructor() { }

  getToken() {
    const token = localStorage.getItem('real_time_chat_token');
    return token;
  }
}
