import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BackendService } from 'src/app/Services/backend.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  userForm:any;
  id:any;
  user:any;
  flag=0;
  constructor(private formBuilder:FormBuilder, private service: BackendService, private router: Router,private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.id= this.activeRoute.snapshot.paramMap.get('id');
    this.service.GetUserByUserId(this.id).subscribe
    ({
      next: ((result:any)=>
      {
        console.log("aa");
        console.log(result);
        this.user= result;
      }),
      error : (err=>
      {
        console.log(err);
      })
    });
  }

  OnModify()
  {
    this.flag=1;
    this.userForm= this.formBuilder.group({
      id: [this.id],
      userName: [this.user.userName],
      password: [this.user.password],
      userType: [this.user.userType],
      name: [this.user.name],
      email: [this.user.email]     
    });
  }

  OnUpdate()
  {
    console.log(this.userForm.value);
    this.service.UpdateUser(this.userForm.value).subscribe({
      next: ((result:any)=>
      {
        console.log(result);
      }),
      error:(err=>
      {
        alert(err.error);
      })
    });
    this.flag=0;    
    this.router.navigate(['/profile',this.id]);
  }

  OnCancel()
  {
    this.flag=0;
    this.router.navigate(['/profile',this.id]);
  } 

}
