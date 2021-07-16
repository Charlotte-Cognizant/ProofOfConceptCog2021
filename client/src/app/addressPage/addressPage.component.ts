import { HttpClient } from '@angular/common/http';
import {Component, Input, OnChanges, OnInit} from '@angular/core'
import { ActivatedRoute } from '@angular/router';
import {Map} from 'mapbox-gl';
import { image } from '../_models/image';
@Component(

    {selector: 'addressPage',
    templateUrl: './addressPage.component.html',
    styleUrls: ['./addressPage.component.css']
})

export class addressPagecomponent implements OnInit,OnChanges{
  // @Input()
  // imagedata!: image;

  constructor(private http:HttpClient, private route: ActivatedRoute){}
    private mapQuestKey = 'FCG8OAH64qxVqVG1MQRqWnDiBY0kwkzA';
    style = 'mapbox://styles/mapbox/streets-v11';
    lat = 37.75;
    lng = -122.41;
    spatial: any;
    address:any;
    imageData:any;
    imagedata:any;
    infostring:any;
    sourceString:any;
    
    ngOnChanges(){
     
  }

    ngOnInit(){
    this.getSpatial()  
    
    }







      //gets specific map information and then loads it all up for the viewer.
  getAllSpatial() {
    //From our database we subscribe in order to ask for some data, we ask for users => is a Typescript function I dont understand super well.
    //Basically tho is says the response to our http request api/user as seen when running the backend http request is used in order to get the results. This.users = 
    //output from API call.
    this.http.get('https://localhost:5001/api/addressPage').subscribe(response => {
      this.address = response;
      console.log(this.address);

    }, error => {
      console.log(error);
    })
  }
  getSpatial(){

this.http.get('https://localhost:5001/api/addressPage/'+this.route.snapshot.paramMap.get("id")).subscribe(data =>{
  this.spatial = data;
  this.sourceString=this.spatial.imagebyte;
  this.imageData= "data:image/png;base64," + this.sourceString;
  })
  
}

//Going to want to get information from the database as a photo.


//containts detailed information about any individual data
}