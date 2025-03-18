import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BackendService } from 'src/app/Services/backend.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm:any;
  id:any;
  constructor(private formBuilder:FormBuilder, private service: BackendService, private router: Router) { }

  ngOnInit(): void {
    this.loginForm= this.formBuilder.group({
      userName: ['',Validators.required],
      password: ['',Validators.required],
      userType: ['',Validators.required]    
    });
  }

  OnLogin()
  {
    console.log(this.loginForm.value);
    this.service.Login(this.loginForm.value).subscribe({
      next: ((result:any)=>
      {
        //console.log("token =");
        //console.log(result);
        localStorage.setItem('token',result);

          //Get User details from username
          this.service.GetUserByUserName(this.loginForm.value.userName).subscribe({
            next: ((result:any)=>
            {
              console.log(result);
              this.id= result.id;
              if(result.userType== "Admin")
              {
                this.router.navigate(['/movies']);
              }
              else
              {
                this.router.navigate(['/home',this.id]);
              }
              
            }),
            error:(err=>
            {
              console.log(err.error);           
              this.router.navigate(['/login']);
              alert(err.error);
            }
            )
          });
          
        this.router.navigate(['/home']);
        console.log(result);
      }),
      error:(err=>
      {
        console.log(err.error);
        this.router.navigate(['/login']);
        alert(err.error);
      })
    });
    
  }

  OnSignup()
  {
    this.router.navigate(['/signup']);
  }

}
