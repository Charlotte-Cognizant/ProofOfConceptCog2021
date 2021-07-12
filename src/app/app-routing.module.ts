import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GalleryComponent } from './gallery/gallery.component';
import { addressComponent } from './addressPage/addressPage.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';


const routes: Routes = [ 
  {path:'', component:AppComponent},
  {path:'home', component:HomeComponent},
  {path: 'gallery',component:GalleryComponent},
  {path: 'addressPage', component:addressComponent},
  {path : '**', redirectTo: ''}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
