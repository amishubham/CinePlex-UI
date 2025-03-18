import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MoviesComponent } from './AdminsComponent/movies/movies.component';
import { TheatresComponent } from './AdminsComponent/theatres/theatres.component';
import { ShowsComponent } from './AdminsComponent/shows/shows.component';
import { AddShowComponent } from './AdminsComponent/add-show/add-show.component';
import { AddMovieComponent } from './AdminsComponent/add-movie/add-movie.component';
import { AddTheatreComponent } from './AdminsComponent/add-theatre/add-theatre.component';
import { HomeComponent } from './UserComponent/home/home.component';
import { TicketsComponent } from './UserComponent/tickets/tickets.component';
import { BookTicketComponent } from './UserComponent/book-ticket/book-ticket.component';
import { SignupComponent } from './SharedComponent/signup/signup.component';
import { LoginComponent } from './SharedComponent/login/login.component';
import { ProfileComponent } from './UserComponent/profile/profile.component';
import { AuthGuard } from './Guards/auth.guard';
import { RoleGuard } from './Guards/role.guard';




const routes: Routes = [
  {path: 'movies', component:MoviesComponent,canActivate:[AuthGuard,RoleGuard]},
  {path: 'theatres', component:TheatresComponent,canActivate:[AuthGuard,RoleGuard]},
  {path: 'shows', component:ShowsComponent,canActivate:[AuthGuard,RoleGuard]},
  {path: 'addmovie', component:AddMovieComponent,canActivate:[AuthGuard,RoleGuard]},
  {path: 'addtheatre', component:AddTheatreComponent,canActivate:[AuthGuard,RoleGuard]},
  {path: 'addshow', component:AddShowComponent,canActivate:[AuthGuard,RoleGuard]},
  {path: 'home/:id', component:HomeComponent,canActivate:[AuthGuard]},
  {path: 'tickets/:id', component:TicketsComponent,canActivate:[AuthGuard]},
  {path: 'book/:id/:showid', component:BookTicketComponent,canActivate:[AuthGuard]},
  {path: 'profile/:id', component:ProfileComponent,canActivate:[AuthGuard]},
  {path: 'signup', component:SignupComponent},
  {path: 'login', component:LoginComponent},

  {path: '', component:LoginComponent}  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
