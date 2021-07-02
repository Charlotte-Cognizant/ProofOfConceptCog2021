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

  addressVal = new FormControl();
  setNameValue(){
    this.addressVal.setValue("300 Broadway")
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