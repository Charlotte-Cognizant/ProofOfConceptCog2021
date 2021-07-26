import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { GalleryComponent } from './gallery/index';
import { addressPagecomponent } from './addressPage/index';
import { HomeComponent } from './home/index';
import { MapItemCardComponent } from './gallery/map-item-card/map-item-card.component';
import { filter } from '@angular-devkit/schematics';
import { FilterPipe } from './gallery/filter.pipe';



@NgModule({
  declarations: [
    AppComponent,
    GalleryComponent,
    addressPagecomponent,
    HomeComponent,
    MapItemCardComponent,
    FilterPipe
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
