import { Component, OnInit } from '@angular/core';
import { error } from 'console';
import { BackendService } from 'src/app/Services/backend.service';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {

  moviesList: any=[];
  constructor(private service:BackendService) { }

  ngOnInit(): void {

    this.service.GetMoviesData().subscribe
    ({
      next: ((result:any[])=>
      {
        console.log(result);
        this.moviesList= result;
      }),
      error : (err=>
      {
        console.log(err);
      })
    });
  }

  OnDelete(id:any)
  {
    this.service.DeleteMovieById(id).subscribe
    ({
      next: ((result)=>
      {
        console.log(result);
        alert("Deleted Succesfully");
      }),
      error : (err=>
      {
        console.log(err);
        alert(err.error);
      })
    });

  }

}
