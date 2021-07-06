import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent implements OnInit{
  
  title = 'Corperate Template';
  users:any;
  addressControl = new FormControl();
  address = ' ';

  setNullValue(){
    this.addressControl.setValue("")
  }

  setAddressValue(){
    
  //Here is where a data operation occurs I think I need to make an api call.


  }

  constructor(private http: HttpClient) {}
  ngOnInit(){
    this.getUsers()
  }


getUsers(){
  this.http.get('https://localhost:5001/api/user').subscribe(response => {
    this.users = response;
  },error => {
    console.log(error);
  })
}

}