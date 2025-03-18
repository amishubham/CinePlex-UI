import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { BackendService } from 'src/app/Services/backend.service';

@Component({
  selector: 'app-add-show',
  templateUrl: './add-show.component.html',
  styleUrls: ['./add-show.component.css']
})
export class AddShowComponent implements OnInit {

  showForm:any;
  movieName = '';
  theatreName = '';
  ticketsRemaining= 0;
  constructor(private formBuilder:FormBuilder, private service: BackendService, private router: Router) { }

  ngOnInit(): void {
    this.showForm= this.formBuilder.group({
      movieId: [0],
      movieName: [''],
      theatreId: [0],
      theatreName: [''],
      price: [0],
      ticketsRemaining: [0],
      showTiming: ['']
    });
  }

  OnAdd(dt:string, tm:string)
  {
    this.showForm.value.movieName= this.movieName;
    this.showForm.value.theatreName = this.theatreName;
    this.showForm.value.ticketsRemaining= this.ticketsRemaining;
    this.showForm.value.showTiming= dt+ "T" + tm;
    console.log(this.showForm.value);
    this.service.AddShow(this.showForm.value).subscribe({
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
    this.router.navigate(['/addshow']);
  }

  OnCancel()
  {
    this.router.navigate(['/shows']);
  }

  onMovieIdChange()
  {
    const movieId= this.showForm.get('movieId').value;
    this.service.GetMovieById(movieId).subscribe({
      next: ((result:any)=>
      {
        console.log(result);
        this.movieName= result.name;
      }),
      error:(err=>
      {
        this.movieName= '';
      })
    });
  }

  searchTheatre()
  {
    const theatreId= this.showForm.get('theatreId').value;
    this.service.GetTheatreById(theatreId).subscribe({
      next: ((result:any)=>
      {
        console.log(result);
        this.theatreName= result.name;
        this.ticketsRemaining = result.seatCapacity;
      }),
      error:(err=>
      {
        console.log(err);
        this.theatreName= '';
        this.ticketsRemaining=0;
      })
    });
  }


}
