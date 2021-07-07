import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import {Map} from 'mapbox-gl';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

//I am less familiar with the frontend at the moment but this links up and is displayed by the information in the template url.
//
export class AppComponent implements OnInit{
  
 // or "const mapboxgl = require('mapbox-gl');"


  title = 'Corperate Template';
  users:any;
  addressControl = new FormControl();
  address = ' ';
  style = 'mapbox://styles/mapbox/streets-v11';
  lat = 37.75;
  lng = -122.41;
  zip='';

  
  //Null function to set address value, pretty sure this is from an old form
  setNullValue(){
    this.addressControl.setValue("")
  }

  setAddressValue(){
    
  //Here is where a data operation occurs where we make an api msg to our geocoding tool.


  }

  constructor(private http: HttpClient) {}
  ngOnInit(){     
   var map = new Map({
      accessToken : 'pk.eyJ1IjoiY2hhcmxvdHRlLWNvZ25pemFudCIsImEiOiJja3FzcXlsdzcxb2F0MnZwNTNuZGprNjZ3In0.j3qiyKwcyQn-k2LH89tZ-w',
      container:'map',
      style: 'mapbox://styles/mapbox/streets-v11'


   });

    this.getUsers()
  }


getUsers(){
  //From our database we subscribe in order to ask for some data, we ask for users => is a Typescript function I dont understand super well.
  //Basically tho is says the response to our http request api/user as seen when running the backend http request is used in order to get the results. This.users = 
  //output from API call.
  this.http.get('https://localhost:5001/api/user').subscribe(response => {
    this.users = response;
    console.log("ping");
  },error => {
    console.log(error);
  })
}
submitAddress(){
  this.http.post<any>('http://localhost:5001/api/User/AdrPost', {title: 'addressDetailSubmission'}).subscribe(data=>{
    this.address = data.address;
    this.zip = data.zip;
    console.log("attempted Post");
  })
}


}