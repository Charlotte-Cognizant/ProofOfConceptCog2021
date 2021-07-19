import { Component } from "@angular/core";
import { EventEmitter, OnInit, Output } from '@angular/core';
import { ChangeDetectionStrategy } from "@angular/compiler/src/core";
import { url } from '@angular-devkit/schematics';
import { HttpClient } from '@angular/common/http';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { FormControl, FormGroup } from '@angular/forms';
import * as mapboxgl from 'mapbox-gl';
import {Map} from 'mapbox-gl';
//import * as L from 'leaflet';
// sources : 
//https://stackoverflow.com/questions/13204002/align-form-elements-in-css https://stackoverflow.com/questions/4221263/center-form-submit-buttons-html-css
//
//
//
//
//
//
@Component(

    {selector: 'home-route',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit{
    @Output() private onFormGroupChange = new EventEmitter<any>();
    constructor(private http: HttpClient) {}
    addressFormGroup = new FormGroup({
     address:new FormControl(''),
     zip : new FormControl(''),
     city : new FormControl(''),
     state: new FormControl('')
     })
     title = 'Building Footprint Search';
     users:any;
   
     addressControl = new FormControl();
     address = ' ';
     style = 'mapbox://styles/mapbox/streets-v11';
     lat = 37.75;
     lng = -122.41;
     zip='';
     city = '';
     state ='';
     queryString:any;
     geocodeJson:any;
     private mapQuestKey = 'FCG8OAH64qxVqVG1MQRqWnDiBY0kwkzA';
     //Null function to set address value, pretty sure this is from an old form
     setNullValue(){
       this.addressControl.setValue("")
     }
   
   //Coming from angular documentaiton
     onSubmit(){
       //updates the values in our application eventually should also reset the form and display a message
       const addrStrng = this.addressFormGroup.get('address')
       const zipStrng = this.addressFormGroup.get('zip')
       const cityStrng = this.addressFormGroup.get('city')
       const stateStrng = this.addressFormGroup.get('state')
       this.address=addrStrng?.value
       this.zip=zipStrng?.value
       this.city =cityStrng?.value
       this.state = stateStrng?.value
       this.submitAddress()
     }
   
    
     setAddressValue(){
       console.error("ping")
     //Here is where a data operation occurs where we make an api msg to our geocoding tool.
     }
   
    
     ngOnInit(){ 
       
     /*var map = new Map({
       accessToken : 'pk.eyJ1IjoiY2hhcmxvdHRlLWNvZ25pemFudCIsImEiOiJja3FzcXlsdzcxb2F0MnZwNTNuZGprNjZ3In0.j3qiyKwcyQn-k2LH89tZ-w',
       container:'map',
       style: 'mapbox://styles/mapbox/streets-v11'
      })*/
   
   
   
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
     this.http.get('https://localhost:5001/api/user').subscribe(response => {
       this.users = response;
       console.log("ping");
     
     },error => {
       console.log(error);
     })
  }

  load_bar() {
    (document.getElementById("barContainer") as HTMLFormElement).style.display = "block";
    let fill = 0;
    window.setInterval(function () {
      fill += 10;
      if (fill === 100) {
        window.location.href = 'http://localhost:4200/gallery';
      } else {
        (document.getElementById("loading_bar") as HTMLFormElement).style.width = fill +"%";
      }
    }, 1000);
  }
   
   addressDataPost(){
     //fetch implementation
     //Sends stuff to the backend now as a JSON package
     const url = 'https://localhost:5001/api/user/AdrPost';

     this.load_bar();

     const data = {
       "Address":this.address,
       "City":this.city,
       "State":this.state,
       "Zip":this.zip.toString()
     }
     //request option json object
     const options = {
       method:"POST",
       body:JSON.stringify(data),
       headers:{
         "Accept": "application/json",
         "Content-type": "application/json; charset=utf-8"
       }
     }
     fetch(url,options).then(res=>res.json()).then(res=>console.log(res));
   }
   
   
   
   submitAddress(){
     var dataPackage = {
       "Address":this.address,
       "City":this.city,
       "State":this.state,
       "Zip":this.zip
     }
     console.error('dataPackage created')
     this.addressDataPost()
     // this.http.post<any>('http://localhost:5001/api/User/AdrPost', {title: 'addressDetailSubmission'}).subscribe(data=>{
     //   data.address = this.address,
     //   data.city = this.city,
     //   data.zip = this.zip,
     //   data.state = this.state
     // })
   
     //this.getUsers();
   }
   mapUpdate(){
     //Set up information for our initial api call to mapbox 
     const addressString =encodeURIComponent(this.address)
     const zipString = encodeURIComponent(this.zip)
     const cityString = encodeURIComponent(this.city)
     const stateString = encodeURIComponent(this.state)
     
     //Below is mapbox api call from documentaiton
     //"https://api.mapbox.com/geocoding/v5/mapbox.places/Los%20Angeles.json?access_token=pk.eyJ1IjoiY2hhcmxvdHRlLWNvZ25pemFudCIsImEiOiJja3Fzb3liaGExcThxMnVyMTc0ZXp2dXcwIn0.HaBkNfsZNhtKxr806kBfjg"
     
     //This is the important object for mapdata and coding. Database could store only this ang generate map
     //http://www.mapquestapi.com/geocoding/v1/address?key=KEY&location=Washington,DC
     // const locationOBJ = this.http.get<any>("http://mapquest.com/geocoding/v1/address?key=FCG8OAH64qxVqVG1MQRqWnDiBY0kwkzA&location=Washington,DC").subscribe(data=>
     //   this.geocodeJson =data
     //   )
     const locationOBJ = this.http.get<any>("http://www.mapquestapi.com/geocoding/v1/address?key=FCG8OAH64qxVqVG1MQRqWnDiBY0kwkzA&location=Washington,DC").subscribe(
       data => this.geocodeJson = data
     ) //+cityString+','+stateString)//addressString)//+zipString+cityString+stateString)
   //+cityString+','+stateString)//addressString)//+zipString+cityString+stateString)
     console.error('http://www.mapquestapi.com/geocoding/v1/address?key=FCG8OAH64qxVqVG1MQRqWnDiBY0kwkzA&location='+addressString+zipString+cityString+stateString)
   
     //To get a human readable value I need to parse the Json
     console.error(this.geocodeJson.LatLng)
     //Forward geocodes the inputs and generates a map
     //This is going to make a call to the mapbox api not our own. That api will then find the location details.
   
   }
}

