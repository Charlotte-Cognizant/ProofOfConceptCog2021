import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { GalleryComponent } from './gallery/index';
import { addressComponent } from './addressPage/index';
import { HomeComponent } from './home/index';
import { MapItemCardComponent } from './gallery/map-item-card/map-item-card.component';



@NgModule({
  declarations: [
    AppComponent,
    GalleryComponent,
    addressComponent,
    HomeComponent,
    MapItemCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
