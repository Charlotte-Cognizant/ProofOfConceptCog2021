import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GalleryComponent } from './gallery';
import { addressComponent } from './addressPage';
import { AppComponent } from './app.component';


const routes: Routes = [
  {path:'', component: AppComponent},
  {path: 'gallery', component : GalleryComponent},
  {path: 'addressPage', component : addressComponent},
  {path : '**', redirectTo: ''}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
