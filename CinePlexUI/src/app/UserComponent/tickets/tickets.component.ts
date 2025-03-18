import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BackendService } from 'src/app/Services/backend.service';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.css']
})
export class TicketsComponent implements OnInit {

  ticketsList: any=[];
  id:any;
  isEmpty = false;
  constructor(private service:BackendService,private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.id= this.activeRoute.snapshot.paramMap.get('id');
    
    this.service.GetTicketsDataByUserId(this.id).subscribe
    ({
      next: ((result:any[])=>
      {
        console.log(result);
        this.ticketsList= result;
        if(this.ticketsList.length==0)
        {
          this.isEmpty= true;
        }
      }),
      error : (err=>
      {
        console.log(err);
      })
    });
  }

  OnDelete(id:any)
  {
    this.service.DeleteTicketById(id).subscribe
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
