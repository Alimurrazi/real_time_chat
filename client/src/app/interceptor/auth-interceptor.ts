import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenProviderService } from '../shared/services/token-provider.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private tokenProviderService: TokenProviderService){
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // const token = localStorage.getItem('real_time_chat_token');
    const token = this.tokenProviderService.getToken();

    if (!token) {
      return next.handle(req);
    }

    const headersObject = new HttpHeaders({
      'Content-Type': 'application/json',
       Authorization: `Bearer ${token}`
    });

    const req1 = req.clone({
      headers: headersObject
    });

    return next.handle(req1);
  }

}
