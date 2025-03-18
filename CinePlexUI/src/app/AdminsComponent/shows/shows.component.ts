import { Component, OnInit } from '@angular/core';
import { BackendService } from 'src/app/Services/backend.service';

@Component({
  selector: 'app-shows',
  templateUrl: './shows.component.html',
  styleUrls: ['./shows.component.css']
})
export class ShowsComponent implements OnInit {

  showsList: any=[];
  constructor(private service:BackendService) { }

  ngOnInit(): void {

    this.service.GetShowsData().subscribe
    ({
      next: ((result:any[])=>
      {
        console.log(result);
        this.showsList= result;
      }),
      error : (err=>
      {
        console.log(err);
      })
    });
  }

  OnDelete(id:any)
  {
    this.service.DeleteShowById(id).subscribe
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
