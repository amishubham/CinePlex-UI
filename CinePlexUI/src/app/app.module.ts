import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MoviesComponent } from './AdminsComponent/movies/movies.component';
import { TheatresComponent } from './AdminsComponent/theatres/theatres.component';
import { ShowsComponent } from './AdminsComponent/shows/shows.component';
import { AddMovieComponent } from './AdminsComponent/add-movie/add-movie.component';
import { AddTheatreComponent } from './AdminsComponent/add-theatre/add-theatre.component';
import { AddShowComponent } from './AdminsComponent/add-show/add-show.component';
import { AdminNavbarComponent } from './AdminsComponent/admin-navbar/admin-navbar.component';
import { UserNavbarComponent } from './UserComponent/user-navbar/user-navbar.component';
import { HomeComponent } from './UserComponent/home/home.component';
import { BookTicketComponent } from './UserComponent/book-ticket/book-ticket.component';
import { TicketsComponent } from './UserComponent/tickets/tickets.component';
import { ProfileComponent } from './UserComponent/profile/profile.component';
import { LoginComponent } from './SharedComponent/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TokenInterceptorService } from './Services/token-interceptor.service';
import { AuthGuard } from './Guards/auth.guard';
import { RoleGuard } from './Guards/role.guard';
import { SignupComponent } from './SharedComponent/signup/signup.component';


@NgModule({
  declarations: [
    AppComponent,
    MoviesComponent,
    TheatresComponent,
    ShowsComponent,
    AddMovieComponent,
    AddTheatreComponent,
    AddShowComponent,
    AdminNavbarComponent,
    UserNavbarComponent,
    HomeComponent,
    BookTicketComponent,
    TicketsComponent,
    ProfileComponent,
    LoginComponent,
    SignupComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [AuthGuard,RoleGuard,{provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
