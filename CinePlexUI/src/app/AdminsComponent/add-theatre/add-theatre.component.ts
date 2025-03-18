import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { BackendService } from 'src/app/Services/backend.service';

@Component({
  selector: 'app-add-theatre',
  templateUrl: './add-theatre.component.html',
  styleUrls: ['./add-theatre.component.css']
})
export class AddTheatreComponent implements OnInit {

  theatreForm:any;
  constructor(private formBuilder:FormBuilder, private service: BackendService, private router: Router) { }

  ngOnInit(): void {
    this.theatreForm= this.formBuilder.group({
      name: [''],
      city: [''],
      seatCapacity: [0]
    });
  }

  OnAdd()
  {
    console.log(this.theatreForm.value);
    this.service.AddTheatre(this.theatreForm.value).subscribe({
      next: ((result:any)=>
      {
        console.log(result);
        alert("Added successfully");
      }),
      error:(err=>
      {
        alert(err.error);
      })
    });
    this.router.navigate(['/addtheatre']);
  }

  OnCancel()
  {
    this.router.navigate(['/theatres']);
  }

}
