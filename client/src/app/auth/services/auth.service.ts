import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  loggedInUser = new Subject();
  constructor(private http: HttpClient) {}

  logIn(user) {
    const data = {
      Mail: user.mail,
      Password: user.password,
    };
    const subject = new Subject();
    const url = environment.url + '/auth/login';
    this.http.post(url, data).subscribe((res) => {
      subject.next(res);
    });
    return subject.asObservable();
  }

  getUserInfoById(userId) {
    const subject = new Subject();
    const url = environment.url + '/auth/getUserById/' + userId;
    this.http.get(url).subscribe((res) => {
      subject.next(res);
    });
    return subject.asObservable();
  }
}
