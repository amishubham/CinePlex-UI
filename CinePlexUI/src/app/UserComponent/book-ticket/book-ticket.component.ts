import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BackendService } from 'src/app/Services/backend.service';

 // Declare Razorpay
 declare var Razorpay: any;

@Component({
  selector: 'app-book-ticket',
  templateUrl: './book-ticket.component.html',
  styleUrls: ['./book-ticket.component.css']
})
export class BookTicketComponent implements OnInit {

  ticketForm:any;
  id:any;
  showid:any;
  showDetails:any;
  razorpayKey = 'rzp_test_4t194vL1iWYnXi';
  constructor(private formBuilder:FormBuilder, private service: BackendService, private router: Router,private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.id= this.activeRoute.snapshot.paramMap.get('id');
    this.showid= this.activeRoute.snapshot.paramMap.get('showid');

    this.service.GetShowById(this.showid).subscribe
    ({
      next: ((result:any)=>
      {
        console.log(result);
        this.showDetails= result;  
        
        this.ticketForm= this.formBuilder.group({
          userId: [this.id],
          movieName: [this.showDetails.movieName],
          showId: [this.showid],
          theatreName: [this.showDetails.theatreName],
          seatNos: [''],
          noOfTickets: [0],
          showTiming: [this.showDetails.showTiming],
          price: [this.showDetails.price] 
        });
        
      }),
      error : (err=>
      {
        console.log(err.error);
      })
    });
    
  }

  OnAdd()
  {
    const ticketData = this.ticketForm.value;
    this.service.BookTicket(ticketData).subscribe({
      next: (result: any) => {
        this.openRazorpay(result.id, ticketData.price);
        //console.log(result);
        //this.router.navigate(['/payment', { ticketId: result.id, amount: this.ticketForm.value.amount }]);
      },
      error:(err=>
      {
        alert(err.error);
      })
    });
    //this.router.navigate(['/tickets',this.id]);
  }
  openRazorpay(ticketId: string, amount: number) {
    const options = {
      key: this.razorpayKey,
      amount: amount * 100, // Razorpay amount is in paise
      currency: 'INR',
      name: 'CinePlex',
      description: 'Movie Ticket Booking',
      handler: (response: any) => {
        this.router.navigate(['/tickets', this.id]);
      },
      prefill: {
        name: 'Shubham Negi',
        email: 'shubhamnegi96399@gmail.com',
        contact: '999999999'
      },
      notes: {
        ticketId: ticketId
      },
      theme: {
        color: '#3399cc'
      }
    };
    const rzp = new Razorpay(options);
    rzp.open();
  }

  OnCancel()
  {
    this.router.navigate(['/home',this.id]);
  }

}
