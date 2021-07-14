import { HttpClient } from '@angular/common/http';
import {Component, OnChanges, OnInit} from '@angular/core'
import {Map} from 'mapbox-gl';
@Component(

    {selector: 'addressPage-route',
    templateUrl: './addressPage.component.html',
    styleUrls: ['./addressPage.component.css']
})

export class addressComponent implements OnInit,OnChanges{
    constructor(private http:HttpClient){}
    private mapQuestKey = 'FCG8OAH64qxVqVG1MQRqWnDiBY0kwkzA';
    style = 'mapbox://styles/mapbox/streets-v11';
    lat = 37.75;
    lng = -122.41;
    address:any;
    ngOnChanges(){
      this.getAddresss();
    }
    ngOnInit(){
       this.getAddresss();
      var map = new Map({
          accessToken : 'pk.eyJ1IjoiY2hhcmxvdHRlLWNvZ25pemFudCIsImEiOiJja3FzcXlsdzcxb2F0MnZwNTNuZGprNjZ3In0.j3qiyKwcyQn-k2LH89tZ-w',
          container:'map',
          style: 'mapbox://styles/mapbox/streets-v11'
      })
    }
    //gets specific map information and then loads it all up for the viewer.
getAddresss(){
     //From our database we subscribe in order to ask for some data, we ask for users => is a Typescript function I dont understand super well.
     //Basically tho is says the response to our http request api/user as seen when running the backend http request is used in order to get the results. This.users = 
     //output from API call.
     this.http.get('https://localhost:5001/api/addressPage').subscribe(response => {
      this.address = response;
       console.log(this.address);
     
     },error => {
       console.log(error);
     })
   }

}

//Going to want to get information from the database as a photo.


//containts detailed information about any individual data
