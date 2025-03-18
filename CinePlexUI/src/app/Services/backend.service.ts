import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BackendService {

  baseUrl = "https://localhost:7101/api/";
  constructor(private http: HttpClient) { }

  GetMoviesData()
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams();
    return this.http.get<any[]>(`${this.baseUrl}Movie/GetAllMovies`,{headers:headers, params:params});
  }

  BookTicket(ticket: any) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post<any>(`${this.baseUrl}Ticket/AddTicket`, ticket, { headers });
  }

  GetTheatresData()
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams();
    return this.http.get<any[]>(`${this.baseUrl}Theatre/GetAllTheatres`,{headers:headers, params:params});
  }

  GetShowsData()
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams();
    return this.http.get<any[]>(`${this.baseUrl}Show/GetAllShows`,{headers:headers, params:params});
  }

  GetTicketsDataByUserId(id:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams().set('userid',id);
    return this.http.get<any[]>(`${this.baseUrl}Ticket/GetTicketsByUserid`,{headers:headers, params:params});
  }

  AddMovie(movie:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams();
    return this.http.post<any>(`${this.baseUrl}Movie/AddMovie`,movie,{headers:headers, params:params});
  }

  AddTheatre(theatre:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams();
    return this.http.post<any>(`${this.baseUrl}Theatre/AddTheatre`,theatre,{headers:headers, params:params});
  }

  AddShow(show:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams();
    return this.http.post<any>(`${this.baseUrl}Show/AddShow`,show,{headers:headers, params:params});
  }


  Signup(user:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams();
    return this.http.post<any>(`${this.baseUrl}User/AddUser`,user,{headers:headers, params:params});
  }
  
  Login(user:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams();
    return this.http.post<any>(`${this.baseUrl}User/Login`,user,{headers:headers, params:params, responseType: 'text' as 'json'});
  }

  GetUserByUserId(id:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams().set('id',id);
    return this.http.get<any>(`${this.baseUrl}User/GetUserById`,{headers:headers, params:params});
  }

  GetUserByUserName(username:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams().set('username',username);
    return this.http.get<any>(`${this.baseUrl}User/GetUserByUserName`,{headers:headers, params:params});
  }

  UpdateUser(user:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams();
    return this.http.put<any>(`${this.baseUrl}User/UpdateUser`,user,{headers:headers, params:params});
  }

  GetShowById(id:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams().set('id',id);
    return this.http.get<any>(`${this.baseUrl}Show/GetShowById`,{headers:headers, params:params});
  }

  GetMovieById(id:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams().set('id',id);
    return this.http.get<any>(`${this.baseUrl}Movie/GetMovieById`,{headers:headers, params:params});
  }

  GetTheatreById(id:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams().set('id',id);
    return this.http.get<any>(`${this.baseUrl}Theatre/GetTheatreById`,{headers:headers, params:params});
  }

  DeleteShowById(id:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams().set('id',id);
    return this.http.delete<any>(`${this.baseUrl}Show/DeleteShow`,{headers:headers, params:params});
  }

  DeleteMovieById(id:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams().set('id',id);
    return this.http.delete<any>(`${this.baseUrl}Movie/DeleteMovie`,{headers:headers, params:params});
  }

  DeleteTheatreById(id:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams().set('id',id);
    return this.http.delete<any>(`${this.baseUrl}Theatre/DeleteTheatre`,{headers:headers, params:params});
  }

  DeleteTicketById(id:any)
  {
    var headers= new HttpHeaders().set("Content-Type", "Application/json");
    var params= new HttpParams().set('id',id);
    return this.http.delete<any>(`${this.baseUrl}Ticket/DeleteTicket`,{headers:headers, params:params});
  }

 
}
