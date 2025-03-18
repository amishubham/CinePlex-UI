import { Component, OnInit } from '@angular/core';
import { BackendService } from 'src/app/Services/backend.service';

@Component({
  selector: 'app-theatres',
  templateUrl: './theatres.component.html',
  styleUrls: ['./theatres.component.css']
})
export class TheatresComponent implements OnInit {

  theatresList: any=[];
  constructor(private service:BackendService) { }

  ngOnInit(): void {

    this.service.GetTheatresData().subscribe
    ({
      next: ((result:any[])=>
      {
        console.log(result);
        this.theatresList= result;
      }),
      error : (err=>
      {
        console.log(err);
      })
    });
  }

  OnDelete(id:any)
  {
    this.service.DeleteTheatreById(id).subscribe
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
