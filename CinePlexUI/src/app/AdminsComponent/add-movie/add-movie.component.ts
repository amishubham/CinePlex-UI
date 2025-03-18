import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { BackendService } from 'src/app/Services/backend.service';

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.css']
})
export class AddMovieComponent implements OnInit {

  movieForm:any;
  constructor(private formBuilder:FormBuilder, private service: BackendService, private router: Router) { }

  ngOnInit(): void {
    this.movieForm= this.formBuilder.group({
      name: [''],
      releaseDate: ['']
    })
  }

  OnAdd()
  {
    console.log(this.movieForm.value);
    this.service.AddMovie(this.movieForm.value).subscribe({
      next: ((result:any)=>
      {
        console.log(result);
        alert("Added successfully");
      }),
      error:(err=>
      {
        console.log(err);
        alert(err.error);
      })
    });
    this.router.navigate(['/addmovie']);
  }

  OnCancel()
  {
    this.router.navigate(['/movies']);
  }

}
