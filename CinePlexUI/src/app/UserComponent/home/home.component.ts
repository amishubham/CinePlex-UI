import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BackendService } from 'src/app/Services/backend.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  showsList: any=[];
  id:any;
  constructor(private service:BackendService,private activeRoute: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.id= this.activeRoute.snapshot.paramMap.get('id');
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

  OnBook(showid:any)
  {
    this.router.navigate(['book',this.id,showid]);
  }

  setDefault(event: Event) : void {
    (event.target as HTMLImageElement).src = 'assets/movie.jpg'
  }

}
