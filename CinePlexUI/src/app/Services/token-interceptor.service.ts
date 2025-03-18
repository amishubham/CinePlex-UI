import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor {

  constructor(private authservice: AuthService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    var token = this.authservice.GetToken();
    console.log("token retrived");
    var tokenHeader= req.clone({
      setHeaders:{
        Authorization : 'bearer ' + token
      }
    });
    return next.handle(tokenHeader);
  }
  
}
