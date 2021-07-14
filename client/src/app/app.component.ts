import { url } from '@angular-devkit/schematics';
import { HttpClient } from '@angular/common/http';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { EventEmitter, OnInit, Output } from '@angular/core';
import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import * as mapboxgl from 'mapbox-gl';
import {Map} from 'mapbox-gl';
//import * as L from 'leaflet';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

//I am less familiar with the frontend at the moment but this links up and is displayed by the information in the template url.
//https://developer.mozilla.org/en-US/docs/Web/CSS/CSS_Grid_Layout
export class AppComponent implements OnInit{
  
 // or "const mapboxgl = require('mapbox-gl');"
 //https://stackoverflow.com/questions/56826066/how-to-send-formgroup-object-as-output-to-parent-component-from-child-component 
  //Null function to set address value, pretty sure this is from an old form
  

//Coming from angular documentaiton
 


  constructor(private http: HttpClient) {}
  ngOnInit(){ 



  //  map.addControl(
  //   new mapboxgl.MapboxGeocoder({
  //   accessToken: 'pk.eyJ1IjoiY2hhcmxvdHRlLWNvZ25pemFudCIsImEiOiJja3FzcXlsdzcxb2F0MnZwNTNuZGprNjZ3In0.j3qiyKwcyQn-k2LH89tZ-w',
  //   mapboxgl: mapboxgl
  //   });

  }



getUsers(){
  //From our database we subscribe in order to ask for some data, we ask for users => is a Typescript function I dont understand super well.
  //Basically tho is says the response to our http request api/user as seen when running the backend http request is used in order to get the results. This.users = 
  //output from API call.

}
}
