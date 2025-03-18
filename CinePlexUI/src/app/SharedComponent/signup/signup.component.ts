import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BackendService } from 'src/app/Services/backend.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  signupForm:any;
    constructor(private formBuilder:FormBuilder, private service: BackendService, private router: Router) { }
  
    ngOnInit(): void {
      this.signupForm= this.formBuilder.group({
        userName: ['',Validators.required],
        password: ['',Validators.required],
        userType: ['',Validators.required],
        name: [''],
        email: ['']        
      })
    }
  
    OnSignup()
    {
      console.log(this.signupForm.value);
      this.service.Signup(this.signupForm.value).subscribe({
        next: ((result:any)=>
        {
          console.log(result);
          alert("Signup Successful");
          this.router.navigate(['/login']);
        }),
        error:(err=>
        {
          alert(err.error);
          this.router.navigate(['/signup']);
        })
      });
    }
  
    OnCancel()
    {
      this.router.navigate(['/login']);
    }
  
}
