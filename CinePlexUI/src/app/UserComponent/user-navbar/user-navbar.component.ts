import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-user-navbar',
  templateUrl: './user-navbar.component.html',
  styleUrls: ['./user-navbar.component.css']
})
export class UserNavbarComponent implements OnInit {

  id:any;
  constructor(private activeRoute: ActivatedRoute,private authservice: AuthService) { }

  ngOnInit(): void {
    this.id= this.activeRoute.snapshot.paramMap.get('id');    
  }

  OnLogout()
  {
    this.authservice.LogoutClear();
  }

}
