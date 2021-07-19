import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GalleryComponent } from './gallery/gallery.component';
import { addressPagecomponent } from './addressPage/addressPage.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';


const routes: Routes = [ 
  {path:'', component:HomeComponent},
  {path:'home', component:HomeComponent},
  {path: 'gallery',component:GalleryComponent},
  {path: 'addressPage/:id', component:addressPagecomponent},
  //{path : '**', redirectTo: ''}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
