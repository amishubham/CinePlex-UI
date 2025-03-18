import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private router:Router) { }

  GetToken()
  {
    return localStorage.getItem('token') || '';
  }

  LogoutClear()
  {
    localStorage.clear();
    this.router.navigate(['login']);
  }

  IsLoggedin()
  {
    return localStorage.getItem('token') !=null;
  }

  HasAccess()
  {
    var token= localStorage.getItem('token') || '';
    var extractedToken = token.split('.')[1];
    var atobtoken = atob(extractedToken);
    var parse= JSON.parse(atobtoken);
    const roleclaim= "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    if(parse[roleclaim] == 'Admin')
      return true;
    else{
      console.log("Does not have access");
      return true;
    }
  }
  
}
