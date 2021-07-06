import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

//I am less familiar with the frontend at the moment but this links up and is displayed by the information in the template url.
//
export class AppComponent implements OnInit{
  
  title = 'Corperate Template';
  users:any;
  addressControl = new FormControl();
  address = ' ';

  //Null function to set address value, pretty sure this is from an old form
  setNullValue(){
    this.addressControl.setValue("")
  }

  setAddressValue(){
    
  //Here is where a data operation occurs where we make an api msg to our geocoding tool.


  }

  constructor(private http: HttpClient) {}
  ngOnInit(){
    //On frontend startup this runs and executes getUsers.
    this.getUsers()
  }


getUsers(){
  //From our database we subscribe in order to ask for some data, we ask for users => is a Typescript function I dont understand super well.
  //Basically tho is says the response to our http request api/user as seen when running the backend http request is used in order to get the results. This.users = 
  //output from API call.
  this.http.get('https://localhost:5001/api/user').subscribe(response => {
    this.users = response;
  },error => {
    console.log(error);
  })
}

}