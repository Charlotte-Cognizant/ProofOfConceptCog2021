import {Component, OnChanges, OnInit, SimpleChanges} from '@angular/core'
import { HttpClient } from '@angular/common/http';


@Component({ templateUrl: 'gallery.component.html' })
export class GalleryComponent implements OnInit, OnChanges {
  constructor(private http: HttpClient) {}

  ngOnChanges(changes: SimpleChanges): void {
        
  }

ngOnInit(): void {
    
 }

  /*getSpatial() {
      //From our database we subscribe in order to ask for some data, we ask for users => is a Typescript function I dont understand super well.
      //Basically tho is says the response to our http request api/user as seen when running the backend http request is used in order to get the results. This.users = 
      //output from API call.
      this.http.get('https://localhost:5001/api/gallery').subscribe(response => {
        //this.address = response;
        //console.log(this.address);

      }, error => {
        console.log(error);
      })
  }
*/
}
